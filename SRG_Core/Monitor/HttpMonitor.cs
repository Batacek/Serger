using System.Text.Json.Serialization;

namespace Serger.SRG_Core.Config;

public class HttpMonitor : Monitor
{

    public string Host => Uri;

    [JsonPropertyName("uri")]
    public string Uri { get; set; }
    
    [JsonPropertyName("valid_response_codes")]
    public List<int>? ValidResponseCodes { get; set; }
    
    [JsonPropertyName("body_regexp")]
    public string? BodyRegexp { get; set; }
    
    public HttpMonitor()
    {
        Uri = "";
    }
    
    public HttpMonitor(int checkInterval, int timeout, string uri, List<int>? validResponseCodes = null, string? bodyRegexp = null) 
        : base("", checkInterval, timeout)
    {
        Uri = uri;
        ValidResponseCodes = validResponseCodes;
        BodyRegexp = bodyRegexp;
    }
    
    public override bool IsUp()
    {
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromMilliseconds(Timeout);
            // Avoid .Result deadlocks by using GetAwaiter().GetResult on the background timer thread
            var response = client.GetAsync(Uri, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();
            
            // Check response code if ValidResponseCodes is specified
            if (ValidResponseCodes != null && ValidResponseCodes.Count != 0)
            {
                if (!ValidResponseCodes.Contains((int)response.StatusCode))
                    return false;
            }

            // Check body regex if BodyRegexp is specified
            if (string.IsNullOrEmpty(BodyRegexp)) return true;
            
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            
            return System.Text.RegularExpressions.Regex.IsMatch(content, BodyRegexp);

        }
        catch
        {
            return false;
        }
    }
    
}