using Misc;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.DataStruct
{
    public class DictDb: DbCentral 
    {
        public ConcurrentDictionary<string, ValueContainer> NedisDb { get; set; }
        public DictDb()
        {
            DbName = "Dictionary";
            NedisDb = new ConcurrentDictionary<string, ValueContainer>();
        }


        public override ResponseModel DbRemoveValue(string value, string? key = null)
        {
            try
            { 
                var checkKey = NedisDb.TryGetValue(key, out ValueContainer? result);
                if (!checkKey)
                {
                    return new ResponseModel
                    {
                        ErrorMessage = "value does not exist"
                    };
                }

                NedisDb.Remove(key, out _);
                return new ResponseModel
                {
                    ErrorMessage = "Successful"
                };
            }
            catch (Exception ex)
            {

                return new ResponseModel
                {
                    ErrorMessage = ex.Message,
                };
            }
        }

        public override ResponseModel DbSetValue(IEnumerable value, string? key = null, TimeSpan ttl = default)
        {
            var checkKey = NedisDb.TryGetValue(key, out ValueContainer? result);
            NedisDb[key] = new ValueContainer
            {
                data = value,
                expireTime = ttl != default ? DateTime.UtcNow.Add(ttl) : default(DateTime),
            };

            if (checkKey)
            {

                return new ResponseModel
                {
                    data = value,
                    ErrorMessage = "value overwritten"
                };
            }
            return new ResponseModel
            {
                data = value,
                ErrorMessage = "Added successfully"
            };

        }

        public override ResponseModel GetItemByKey(string key, out DateTime? expiryValue)
        {
            try
            {
                var value = NedisDb.TryGetValue(key, out ValueContainer? result);
                expiryValue = null;
                if (!value)
                {
                    return new ResponseModel
                    {
                        ErrorMessage = "key doesn't exist"
                    };
                }
                if (result != null && result.expireTime != null)
                {
                    expiryValue = result.expireTime;
                    //if (DateTime.UtcNow > result.expireTime)
                    //{
                    //    NedisDb.Remove(key, out _);
                    //    return new ResponseModel
                    //    {
                    //        data = "Expired!!!!",
                    //        ErrorMessage = "Key has expired",
                    //    };

                    //}
                }
                return new ResponseModel
                {
                    data = result.data,
                };
            }
            catch (Exception ex)
            {
                expiryValue = null;
                return new ResponseModel
                {
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}

