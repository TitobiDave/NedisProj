

using DatabaseLib;
using DatabaseLib.DataStruct;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiscLib.BackgroundOperation;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<DictDb>();
builder.Services.AddHostedService<CheckExpiredTTL>();



using IHost host = builder.Build();

var db = host.Services.GetService<DictDb>();
db.DbSetValue("titobi", "name", TimeSpan.FromSeconds(20));



await host.StartAsync();
