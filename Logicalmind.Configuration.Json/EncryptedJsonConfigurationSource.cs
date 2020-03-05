using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Logicalmind.Configuration.Json
{
    public class EncryptedJsonConfigurationSource : JsonConfigurationSource
    {
        public string Key { get; set; }

        public EncryptedJsonConfigurationSource() { }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new EncryptedJsonConfigurationProvider(Key, this);
        }
    }
}
