using Logicalmind.Configuration.Json.Example.Services;
using Logicalmind.Configuration.Json.Example.Settings;
using Logicalmind.Cryption;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Logicalmind.Configuration.Json.Example
{
    public class Program
    {
        const string SettingsFileName = "appsettings.json";
        public static void Main(string[] args)
        {
            var key = "example key";

            WriteExampleSettingsFile(key);

            using (var host = BuildHost(key))
            {
                host.Services.GetRequiredService<IExampleService>().ShowSettings();
            }
        }

        public static IHost BuildHost(string key)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddEncryptedJsonFile(SettingsFileName, key);
                })
                .ConfigureServices((hostContext, config) =>
                {
                    config.Configure<ExampleSettings>(hostContext.Configuration.GetSection(nameof(ExampleSettings)));
                    config.AddTransient<IExampleService, ExampleService>();
                })
                .ConfigureLogging(config =>
                {
                    config.AddConsole();
                });

            return hostBuilder.Build();
        }

        /// <summary>
        /// This method creates a fake appsettings.json file where we have an encrypted key/value. In a real scenario,
        /// you would generate this appsettings.json file via another process using the encryption key that your app
        /// would have a runtime.
        /// </summary>
        /// <param name="key">The key to use to encrypt the data.</param>
        public static void WriteExampleSettingsFile(string key)
        {
            var c = new Cryptor(key);
            if (File.Exists(SettingsFileName)) File.Delete(SettingsFileName);

            var settingsData =
                new JObject(
                    new JProperty(nameof(ExampleSettings),
                    new JObject(new JProperty(nameof(ExampleSettings.Username),
                                              "ExampleUsername"),
                                new JProperty($"{nameof(ExampleSettings.Password)}.Encrypted",
                                              c.Encrypt("ExamplePassword")))));

            var json = settingsData.ToString(Formatting.Indented);

            // Display the generated appsettings.json file if you like
            //Console.WriteLine(json);

            File.WriteAllText(SettingsFileName, json);

        }
    }
}
