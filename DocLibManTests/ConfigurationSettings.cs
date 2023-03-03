using Microsoft.Extensions.Configuration;

namespace DocLibManTests;

public static class ConfigurationSettings
{
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables() 
            .Build();
        return config;
    }
}