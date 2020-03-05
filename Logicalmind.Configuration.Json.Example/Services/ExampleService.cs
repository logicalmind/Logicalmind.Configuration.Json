using Logicalmind.Configuration.Json.Example.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Logicalmind.Configuration.Json.Example.Services
{
    public class ExampleService : IExampleService
    {
        private readonly ILogger<ExampleService> logger;
        private readonly ExampleSettings exampleSettings;

        public ExampleService(ILogger<ExampleService> logger, IOptions<ExampleSettings> exampleSettings)
        {
            this.logger = logger;
            this.exampleSettings = exampleSettings.Value;
        }

        public void ShowSettings()
        {
            logger.LogInformation($"{nameof(exampleSettings.Username)}: {exampleSettings.Username}");
            logger.LogInformation($"{nameof(exampleSettings.Password)}: {exampleSettings.Password}");
        }
    }
}
