using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Handler
{
    public interface IDatabase
    {
        public ResponseModel addItem();

        public ResponseModel removeItem();

        public ResponseModel GetItem();

        
    }
}
