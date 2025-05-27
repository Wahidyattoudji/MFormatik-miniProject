namespace MFormatik.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

public class AppSettingsManager
{
    private readonly string _filePath;
    private readonly IConfigurationRoot _configuration;

    public AppSettingsManager(string filePath = "appsettings.json")
    {
        _filePath = filePath;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_filePath, optional: false, reloadOnChange: true);
        _configuration = builder.Build();
    }


    public string GetSetting(string section, string key)
    {
        return _configuration[$"{section}:{key}"];
    }


    public void SetSetting(string section, string key, string value)
    {
        var json = File.ReadAllText(_filePath);
        dynamic jsonObj = JsonConvert.DeserializeObject(json);

        if (jsonObj[section] == null)
            jsonObj[section] = new Newtonsoft.Json.Linq.JObject();

        jsonObj[section][key] = value;

        string output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, output);
    }
}

