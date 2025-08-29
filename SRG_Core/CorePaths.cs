using System;
using System.IO;

namespace Serger.SRG_Core;

public static class CorePaths
{
    private static readonly object _lock = new();
    private static string? _baseDir;

    public static string AppDataBase
    {
        get
        {
            if (_baseDir != null) return _baseDir;
            lock (_lock)
            {
                if (_baseDir != null) return _baseDir;
                var baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                // macOS -> ~/Library/Application Support
                // Windows -> %AppData%
                // Linux -> ~/.local/share
                _baseDir = Path.Combine(baseDir, "SergerGUI");
                Directory.CreateDirectory(_baseDir);
                return _baseDir;
            }
        }
    }

    public static string EnsureDirectory(string relative)
    {
        var full = Path.Combine(AppDataBase, relative);
        Directory.CreateDirectory(full);
        return full;
    }

    public static string GetConfigFilePath()
    {
        var dir = EnsureDirectory("Config");
        return Path.Combine(dir, "Config.json");
    }

    public static string GetLangsDirectory()
    {
        return EnsureDirectory("Langs");
    }
}
