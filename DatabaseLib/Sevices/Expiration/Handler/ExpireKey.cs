using DatabaseLib.DataStruct;
using DatabaseLib.Sevices.Expiration.Contract;
using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Sevices.Expiration.Handler
{
    public class ExpireKey : IExpireKey
    {
        public readonly DictDb _db;

        public ExpireKey(DictDb db)
        {
            _db = db;
        }

        public ResponseModel DelKeyExpiration(string Key)
        {
            throw new NotImplementedException();
        }

        public ResponseModel SetKeyExpirationMilliSec(string Key, TimeSpan span)
        {
            throw new NotImplementedException();
        }

        public ResponseModel SetKeyExpirationSec(string Key, TimeSpan span)
        {
            try
            {
                var result = _db.GetItemByKey(Key, out _);

                if (result.data == null)
                {
                    return new ResponseModel
                    {
                        ErrorMessage = "Key doesnt exist",
                        IsSuccesful = false,
                    };
                }
                var response = _db.DbSetValue(Key, result.data, span);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
    }
}
