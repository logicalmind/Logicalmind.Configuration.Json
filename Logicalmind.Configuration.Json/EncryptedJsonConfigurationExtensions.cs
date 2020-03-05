using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;

namespace Logicalmind.Configuration.Json
{
    public static class EncryptedJsonConfigurationExtensions
    {
        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder,
                                                                 string path,
                                                                 string key,
                                                                 IFileProvider fileProvider = null,
                                                                 bool optional = true,
                                                                 bool reloadOnChange = false)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Invalid File Path", nameof(path));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Invalid Key", nameof(key));
            }

            return builder.Add<EncryptedJsonConfigurationSource>(s =>
            {
                s.Key = key;
                s.FileProvider = fileProvider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }
    }
}
