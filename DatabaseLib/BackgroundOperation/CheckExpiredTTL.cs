using DatabaseLib;
using DatabaseLib.DataStruct;
using Microsoft.Extensions.Hosting;
using Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscLib.BackgroundOperation
{
    //Periodic check to see if key has expired
    public class CheckExpiredTTL : IHostedLifecycleService
    {
        public readonly DictDb _db;
        public CheckExpiredTTL(DictDb db)
        {
            _db = db;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
          
        }

        public async Task StartedAsync(CancellationToken cancellationToken)
        {

            var val = Console.ReadLine();

            await Task.Run(() =>
            {
                while (val != "Exit")
                {
                    val = Console.ReadLine();
                }

            });
        }

        public async Task StartingAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(10000);
                foreach (KeyValuePair<string, ValueContainer> item in _db.NedisDb)
                {
                    if (DateTime.UtcNow > item.Value.expireTime && item.Value.expireTime != null)
                    {
                        _db.NedisDb.Remove(item.Key, out _);
                    }
                }
                Console.WriteLine("tito");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
           
        }

        public async Task StoppedAsync(CancellationToken cancellationToken)
        {
        }

        public async Task StoppingAsync(CancellationToken cancellationToken)
        {
        }


    }
}
