using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc.Tokens
{

    public enum TokenType
    {
        Identifier, 
        Number,    
        Operator,  
        Symbol,     
        Index,
        Unknown    
    }
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString() => $"{Value}";
    }
}
