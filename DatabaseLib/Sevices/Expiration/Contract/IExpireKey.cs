using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Sevices.Expiration.Contract
{
    public interface IExpireKey
    {
        public ResponseModel SetKeyExpirationSec(string Key, TimeSpan span);

        public ResponseModel DelKeyExpiration(string Key);

        public ResponseModel SetKeyExpirationMilliSec(string Key, TimeSpan span);


    }
}
