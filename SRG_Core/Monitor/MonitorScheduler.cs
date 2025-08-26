using System.Collections.Concurrent;

namespace Serger.SRG_Core.Config;

public class MonitorScheduler(Lang lang) : IDisposable
{
    
    private readonly ConcurrentDictionary<Monitor, MonitorTask> _monitorTasks = new();
    private readonly Lock _lockObject = new();
    private bool _disposed;

    public event EventHandler<MonitorResultEventArgs>? MonitorResult;

    public void UpdateMonitors(List<Monitor> monitors)
    {
        lock (_lockObject)
        {
            if (_disposed) return;

            // Stop monitors that are no longer in the config
            var monitorsToRemove = _monitorTasks.Keys
                .Where(existingMonitor => !monitors.Contains(existingMonitor))
                .ToList();

            foreach (var monitor in monitorsToRemove)
            {
                RemoveMonitor(monitor);
            }

            // Add or update monitors
            foreach (var monitor in monitors)
            {
                if (_monitorTasks.TryGetValue(monitor, out var existingTask))
                {
                    // Check if interval changed
                    if (existingTask.CheckInterval == monitor.CheckInterval) continue;
                    
                    RemoveMonitor(monitor);
                    AddMonitor(monitor);
                }
                else
                {
                    AddMonitor(monitor);
                }
            }
        }
    }

    private void AddMonitor(Monitor monitor)
    {
        lock (_lockObject)
        {
            if (_disposed) return;

            if (_monitorTasks.ContainsKey(monitor))
                return;

            var monitorTask = new MonitorTask(monitor, OnMonitorExecuted, lang);
            _monitorTasks.TryAdd(monitor, monitorTask);
            monitorTask.Start();
        }
    }

    public void RemoveMonitor(Monitor monitor)
    {
        lock (_lockObject)
        {
            if (!_monitorTasks.TryRemove(monitor, out var monitorTask)) return;
            
            monitorTask.Stop();
            monitorTask.Dispose();
        }
    }

    private void OnMonitorExecuted(Monitor monitor, bool isUp, DateTime timestamp)
    {
        var status = isUp ? lang.MonitorUp : lang.MonitorDown;
        var monitorType = monitor switch
        {
            PingMonitor => lang.PingMonitor,
            HttpMonitor => lang.HttpMonitor,
            SocketMonitor => lang.SocketMonitor,
            _ => monitor.GetType().Name
        };
        
        Log.PrintLog($"[{timestamp:yyyy-MM-dd HH:mm:ss}] {monitorType} {monitor.GetDisplayName()}: {status}");
        MonitorResult?.Invoke(this, new MonitorResultEventArgs(monitor, isUp, timestamp));
    }

    public void Dispose()
    {
        if (_disposed) return;

        lock (_lockObject)
        {
            _disposed = true;
            
            foreach (var monitorTask in _monitorTasks.Values)
            {
                monitorTask.Stop();
                monitorTask.Dispose();
            }
            
            _monitorTasks.Clear();
        }
        
        GC.SuppressFinalize(this);
    }
    
}

public class MonitorResultEventArgs(Monitor monitor, bool isUp, DateTime timestamp) : EventArgs
{
    
    public Monitor Monitor { get; } = monitor;
    public bool IsUp { get; } = isUp;
    public DateTime Timestamp { get; } = timestamp;
    
}

internal class MonitorTask(Monitor monitor, Action<Monitor, bool, DateTime> onResult, Lang lang)
    : IDisposable
{
    
    private Timer? _timer;
    private bool _disposed;

    public int CheckInterval => monitor.CheckInterval;

    public void Start()
    {
        if (_disposed) return;

        _timer = new Timer(ExecuteMonitor, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(monitor.CheckInterval));
    }

    public void Stop()
    {
        _timer?.Dispose();
        _timer = null;
    }

    private void ExecuteMonitor(object? state)
    {
        if (_disposed) return;

        try
        {
            var isUp = monitor.IsUp();
            var timestamp = DateTime.UtcNow;
            onResult(monitor, isUp, timestamp);
        }
        catch (Exception ex)
        {
            Log.WriteLog($"{lang.MonitorExecuteError} {monitor.GetType().Name}: {ex.Message}");
            onResult(monitor, false, DateTime.UtcNow);
        }
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
        Stop();
    }
    
}