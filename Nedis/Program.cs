

using CommandLib;
using DatabaseLib;
using DatabaseLib.DataStruct;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiscLib.BackgroundOperation;

var host = new HostBuilder().ConfigureHostConfiguration(hconfig=> {}).ConfigureServices((context, services) =>
{
    services.AddSingleton<DictDb>();               //  in‑memory DB
    services.AddSingleton<Command>();
    services.AddHostedService<CheckExpiredTTL>();  // the background checker
}).UseConsoleLifetime().Build();

await host.StartAsync();


var exec = host.Services.GetService<Command>();

var dictDb = host.Services.GetService<DictDb>();

var query = "";
while (query.ToUpper() != "EXIT")
{
    var result = exec.ParseCommand(query.ToUpper());
    query = Console.ReadLine();
    await host.StopAsync();
}