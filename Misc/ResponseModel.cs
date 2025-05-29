using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc
{
    public class ResponseModel
    {
        public object? data { get; set; }

        public string? ErrorMessage {  get; set; }

        public bool IsSuccesful { get; set; }
    }
}
