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
    public class CheckExpiredTTL : BackgroundService
    {
        public readonly DictDb _db;
        public CheckExpiredTTL(DictDb db)
        {
            _db = db;
        }

    

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                foreach (KeyValuePair<string, ValueContainer> item in _db.NedisDb)
                {
                    if (DateTime.UtcNow > item.Value.expireTime && item.Value.expireTime != null)
                    {
                        _db.NedisDb.Remove(item.Key, out _);
                    }

                }
            }
        }
    }
}
