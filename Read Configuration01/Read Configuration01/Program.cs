// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;


await Task.Run(async () =>
{
    while (true)
    {
        ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);
        IConfigurationRoot configurationRoot = configurationBuilder.Build();
        string name = configurationRoot["name"];
        Console.WriteLine($"name = {name}");
        string proxyAddress = configurationRoot.GetSection("proxy:address").Value;
        Console.WriteLine($"address={proxyAddress}");
        await Task.Delay(2000);
    }
});


