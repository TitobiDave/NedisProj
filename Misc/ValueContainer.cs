using Misc.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc
{
    public class ValueContainer
    {
        public object data { get; set; }

        public DateTime? expireTime { get; set; }

        public override string ToString()
        {
            if(data is List<Token>)
            {
                StringBuilder sb = new StringBuilder();
                var result = (List<Token>)data;
                sb.Append("{");
                foreach (var item in result)
                {
                    sb.Append(item.Value);
                    if (result[result.Count-1] != item)
                    {
                        sb.Append(", ");
                    }
                    
                }
                sb.Append("}");
                return sb.ToString();
            }
            return data.ToString();
        }
    }
}
