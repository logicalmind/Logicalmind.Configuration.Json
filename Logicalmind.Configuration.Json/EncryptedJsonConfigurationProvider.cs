using Logicalmind.Cryption;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Logicalmind.Configuration.Json
{
    public class EncryptedJsonConfigurationProvider : JsonConfigurationProvider
    {
        private readonly ICryptor cryptor;
        private readonly Dictionary<string, string> decrypted = new Dictionary<string, string>();

        public EncryptedJsonConfigurationProvider(string key,
                                                  EncryptedJsonConfigurationSource encryptedConfigurationSource)
                                                    : base(encryptedConfigurationSource)
        {
            cryptor = new Cryptor(key);
        }

        public override bool TryGet(string key, out string value)
        {
            if (!decrypted.TryGetValue(key, out value))
            {
                return base.TryGet(key, out value);
            }
            return true;
        }

        public override void Load(Stream stream)
        {
            base.Load(stream);

            var encryptedItems = Data.Where(x => x.Key.EndsWith(".Encrypted",
                                                                StringComparison.CurrentCultureIgnoreCase));

            foreach (var item in encryptedItems)
            {
                decrypted.Add(item.Key.Replace(".Encrypted", ""), cryptor.Decrypt(item.Value));
            }
        }
    }
}
