﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc.Tokens
{
    public interface ITokenizer
    {
        List<Token> Tokenize();
    }
}
