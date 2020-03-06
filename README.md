# Overview
This is an extension to the .NET Core configuration capabilities that allows for JSON configuration files to have keys with encrypted values. The library uses the [Cryption](https://github.com/logicalmind/Logicalmind.Cryption) library internally and you can use that library to create your encrypted values. 

# Example
An example JSON file:
```json
{
  "SomeKey.Encrypted": "<your_encrypted_value>"
}
```
Startup configuration code to load your encrypted file. Note that any keys that end with ".Encrypted" will be decrypted and available as their real name.
```C#
var host = new HostBuilder()
               .ConfigureAppConfiguration(config =>
               {
                   config.AddEncryptedJsonFile("your_settings_file.json", "your_key");
               }).Build();

var value = host.Services.GetServices<IConfiguration>().GetValue<string>("SomeKey");
```

A full example exists within the repo.
