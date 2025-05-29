
using DatabaseLib.DataStruct.ListDb.Contract;
using Misc;
using Misc.Tokens;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.DataStruct
{
    public class DictDb
    {
        private protected readonly ConcurrentDictionary<string, ValueContainer> NedisDb;
        public DictDb()
        {
            NedisDb = new ConcurrentDictionary<string, ValueContainer>();
        }

        public ConcurrentDictionary<string, ValueContainer> NEDISDB
        {
            get { return NedisDb; }
        }
        public virtual ResponseModel DbRemoveValue(string? key = null)
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

        public virtual ResponseModel DbSetValue(string key, object value, TimeSpan ttl = default)
        {
            try
            {
                var checkKey = NedisDb.TryGetValue(key, out ValueContainer? result);
                NedisDb[key] = new ValueContainer
                {
                    data = value,
                    expireTime = ttl != default ? DateTime.UtcNow.Add(ttl) : null,
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
            catch (Exception ex)
            {

                throw;
            }
            

        }
        public  ResponseModel DbSetList(string key, object value, TimeSpan ttl = default)
        {
            try
            {
                ITokenizer _tokenizer;
                if (value == null)
                {
                    throw new InvalidOperationException("Value cannot be null");
                }
                var strVal = value as string;
                _tokenizer = new Tokenizer(strVal);
                List<Token> result = _tokenizer.Tokenize();
                return DbSetValue(key, result, ttl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("An error occured: {0}", ex.Message));
                throw;
            }
        }
        public virtual ResponseModel GetItemByKey(string key, out DateTime? expiryValue, string index = default)
        {
            try
            {
                var value = NedisDb.TryGetValue(key, out ValueContainer? result);
                expiryValue = null;
                if (!value)
                {
                    Console.WriteLine("Key doesn't exist");
                    return new ResponseModel
                    {
                        ErrorMessage = "key doesn't exist"
                    };

                }
                if (result != null && result.expireTime != null)
                {
                    expiryValue = result.expireTime;
                    if (DateTime.UtcNow > result.expireTime)
                    {
                        NedisDb.Remove(key, out _);
                        Console.WriteLine("Key has expired");
                        return new ResponseModel
                        {
                            data = "Expired!!!!",
                            ErrorMessage = "Key has expired",
                        };

                    }
                }
                if(index != default && result.data is List<Token>)
                {
                    List<Token> item = (List<Token>)result.data;
                    int numIndex = Convert.ToInt32(index);
                    Console.WriteLine(item[numIndex]);
                    return new ResponseModel
                    {
                        data = result.data,
                    };
                }
                Console.WriteLine(result);
                return new ResponseModel
                {
                    data = result.data,
                };
            }
            catch (Exception ex)
            {
                expiryValue = null;
                Console.WriteLine(ex.Message);
                return new ResponseModel
                {
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}

