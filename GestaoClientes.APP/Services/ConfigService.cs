using IniParser;
using IniParser.Model;

namespace GestaoClientes.APP.Services;

public class ConfigService
{
    private readonly string _cfgFilePath = @"C:\Users\miche\source\repos\GestaoClientesCAPTA\GestaoClientes.APP\Config\config.ini";
    private readonly string _baseUrlApi = "BaseUrlApi";

    public string GetApiBaseUrl()
    {
        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile(_cfgFilePath);
        return data["API"][_baseUrlApi];
    }
}
