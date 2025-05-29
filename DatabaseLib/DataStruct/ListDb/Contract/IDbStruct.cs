using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.DataStruct.ListDb.Contract
{
    public interface IDbStruct
    {

        public ResponseModel DbSetValue(string key, object value, TimeSpan ttl = default);
    }
}
