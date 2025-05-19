

using DatabaseLib;
using DatabaseLib.DataStruct;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiscLib.BackgroundOperation;

var host = new HostBuilder().ConfigureHostConfiguration(hconfig=> {}).ConfigureServices((context, services) =>
{
    services.AddSingleton<DictDb>();               //  in‑memory DB
    services.AddHostedService<CheckExpiredTTL>();  // the background checker
}).UseConsoleLifetime().Build();

await host.StartAsync();

Console.ReadLine();
