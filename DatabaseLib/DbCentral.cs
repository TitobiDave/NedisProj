using Misc;
using System;
using System.Collections;


namespace DatabaseLib
{
    public abstract class DbCentral
    {

        protected string DbName;

        
        public abstract ResponseModel DbRemoveValue(string key);
        public abstract ResponseModel DbSetValue(string key, IEnumerable value, TimeSpan ttl = default);

        public abstract ResponseModel GetItemByKey(string key, out DateTime? expiryValue);
       


      
    }
}
