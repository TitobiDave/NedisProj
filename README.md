# NedisProj

Nedis is a lightweight, Redis-inspired in-memory key-value store library built in C# and .NET. It provides a simple, extensible playground for exploring distributed-cache concepts and building custom caching solutions.

## Features

* 🔑 **Concurrent Dictionary**: a thread safe data structure
* ⏱️ **Per-key TTL**: Optional Time-to-Live with automatic expiry on access.
* ⚙️ **Core commands**:

  * `SET` (add/overwrite)
  * `GET` (with expiry check)
  * `DEL` (delete key)
* 📡 **Client-server support**: TCP-based communication (planned).
* 💬 **Structured responses**: Clear success vs. error handling via `ResponseModel`.
* 🔄 **Extensible design**:

  * Add background TTL sweeper for proactive cleanup.
  * Implement Redis-like commands (`EXPIRE`, `TTL`, etc.).

## Getting Started

### Prerequisites

* .NET 7 SDK (or later)
* C# 10

### Installation

1. Clone the repo:

   ```bash
   git clone https://github.com/TitobiDave/NedisProj
   cd NedisProj
   ```

2. Build the project:

   ```bash
   dotnet build
   ```

## Usage

```csharp
using CommandLib;
using DatabaseLib;
using DatabaseLib.DataStruct;
using DatabaseLib.DataStruct.ListDb.Contract;
using DatabaseLib.Sevices.Expiration.Contract;
using DatabaseLib.Sevices.Expiration.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiscLib.BackgroundOperation;

var host = new HostBuilder().ConfigureHostConfiguration(hconfig=> {}).ConfigureServices((context, services) =>
{
    services.AddSingleton<DictDb>();               //  in‑memory DB
    services.AddSingleton<Command>();
    services.AddScoped<IExpireKey, ExpireKey>();
    services.AddHostedService<CheckExpiredTTL>();  // the background checker
}).UseConsoleLifetime().Build();

await host.StartAsync();

var exec = host.Services.GetService<Command>();

var dictDb = host.Services.GetService<DictDb>();

var query = "";
while (query.ToUpper() != "EXIT")
{
    Console.Write(">>> ");
    query = Console.ReadLine();
    var result = exec.ParseCommand(query);

}
await host.StopAsync();
```

## Extending Nedis

* **Thread Safety**: Change the underlying store to `ConcurrentDictionary<string, ValueContainer>`.
* **Background Cleanup**: Implement a periodic task to scan and remove expired keys.
* **Additional Commands**:

  * `Expire(string key, TimeSpan ttl)`
  * `TimeSpan? TTL(string key)`
  * `Persist(string key)`

## Contributing

Contributions are welcome! Please open an issue or submit a pull request:

1. Fork the repo
2. Create a new branch (`git checkout -b feature/my-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin feature/my-feature`)
5. Open a Pull Request



© 2025 Titobi
