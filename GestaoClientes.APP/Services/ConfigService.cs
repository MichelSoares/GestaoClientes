using IniParser;
using IniParser.Model;
using System.IO;

namespace GestaoClientes.APP.Services;

public class ConfigService
{
    private readonly string _cfgFilePath;
    private readonly string _baseUrlApi = "BaseUrlApi";

    public ConfigService()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _cfgFilePath = Path.Combine(basePath, "Config", "config.ini");
    }

    public string GetApiBaseUrl()
    {
        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile(_cfgFilePath);
        return data["API"][_baseUrlApi];
    }
}
