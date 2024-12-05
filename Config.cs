public class Config                                     // Class for configuration variables and values
{                                                       // X = ignore Y = check for differences
    public string JSON_Version { get; set; }            // JSON config file version (X.X.y)
    public string URL { get; set; }                     // URL from JSON config file
    public string JSON_VER { get; set; }                // JSON config file version (X.y.X)
    public int PING_DELAY { get; set; }                 // Pause lenght between pings (in ms)
    public string LANG { get; set; }                    // Language (currently: en/cz)
    public string CS_Version = "0.0.5";                 // CS Program file version (X.X.y)
    public string CS_VER = "0.0";                       // CS Program file version (X.y.X)
}