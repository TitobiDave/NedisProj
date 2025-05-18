using Misc;
using System;
using System.Collections;


namespace DatabaseLib
{
    public abstract class DbCentral
    {

        protected string DbName;

        
        public abstract ResponseModel DbRemoveValue(string value, string key = default);
        public abstract ResponseModel DbSetValue(IEnumerable value, string key = default, TimeSpan ttl = default);

        public abstract ResponseModel GetItemByKey(string key, out DateTime? expiryValue);
       


      
    }
}
