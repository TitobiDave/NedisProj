using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.DataStruct
{
    public class DictDb : DbCentral<Dictionary<string, ValueContainer>>
    {
        public DictDb()
        {
            DbName = "Dictionary";
            NedisDb = new Dictionary<string, ValueContainer>();
        }


        public override ResponseModel DbRemoveValue(string value, string key = null)
        {
            try
            {
                var checkKey = NedisDb.TryGetValue(key, out ValueContainer result);
                if (!checkKey)
                {
                    return new ResponseModel
                    {
                        ErrorMessage = "value does not exist"
                    };
                }

                NedisDb.Remove(key);
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

        public override ResponseModel DbSetValue(string value, string? key = null, double expiryTime = 0)
        {
            var checkKey = NedisDb.TryGetValue(key, out ValueContainer result);
            NedisDb[key] = new ValueContainer
            {
                data = value,
                expireTime = DateTime.Now.AddSeconds(expiryTime),
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
                var value = NedisDb.TryGetValue(key, out ValueContainer result);
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
                    if (DateTime.Now > result.expireTime)
                    {
                        NedisDb.Remove(key);
                        return new ResponseModel
                        {
                            ErrorMessage = "Key has expired",
                        };

                    }
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

