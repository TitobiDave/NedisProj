using Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    public abstract class DbCentral<T> where T : ICollection
    {

        protected string DbName;

        public T NedisDb {  get; set; }
        
        public abstract ResponseModel DbRemoveValue(string value, string key = default);
        public abstract ResponseModel DbSetValue(string value, string key = default, double expiryTime = 0);

        public abstract ResponseModel GetItemByKey(string key, out DateTime? expiryValue);
       


      
    }
}
