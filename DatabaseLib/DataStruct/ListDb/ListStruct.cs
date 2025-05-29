using DatabaseLib.DataStruct.ListDb.Contract;
using Misc;
using Misc.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.DataStruct.ListDb
{
    public class ListStruct: DictDb, IDbStruct
    {
        private readonly DictDb _db;
        public ListStruct(DictDb db)
        {
            _db = db;
        }
        public override ResponseModel DbSetValue(string key, object value , TimeSpan ttl = default)
        {
            try
            {
                ITokenizer _tokenizer;
                if(value == null)
                {
                    throw new InvalidOperationException("Value cannot be null");
                }
                var strVal = value as string;
                _tokenizer =  new Tokenizer(strVal);
                List<Token> result = _tokenizer.Tokenize();
                return _db.DbSetValue(key, result, ttl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("An error occured: {0}", ex.Message));
                throw;
            }
        }
    }
}
