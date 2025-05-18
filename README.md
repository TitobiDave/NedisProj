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
using DatabaseLib.DataStruct;

var db = new DictDb();

// Set a key with a 60-second TTL
var setResult = db.DbSetValue("myKey", "myValue", expiryTime: 60);
Console.WriteLine(setResult.Message);

// Get the key
var getResult = db.GetItemByKey("myKey", out DateTime? expireAt);
Console.WriteLine(getResult.Data);  // "myValue"
Console.WriteLine(expireAt);       // Expiry timestamp

// Delete the key
var delResult = db.DbRemoveValue(value: null, key: "myKey");
Console.WriteLine(delResult.Message);
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
