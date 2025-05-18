

using DatabaseLib;
using DatabaseLib.DataStruct;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiscLib.BackgroundOperation;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<DictDb>();
builder.Services.AddHostedService<CheckExpiredTTL>();



using IHost host = builder.Build();
await host.RunAsync();
