using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc.Tokens
{
    public class Tokenizer: ITokenizer
    {
        private readonly string _input;
        private int _position;

        public Tokenizer(string input)
        {
            _input = input;
            _position = 0;
        }

        private char CurrentChar => _position < _input.Length ? _input[_position] : '\0';
        private void Advance() => _position++;

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            while (_position < _input.Trim().Length)
            {
                char c = CurrentChar;

                //if (char.IsWhiteSpace(c))
                //{
                //    tokens.Add(new Token(TokenType.Whitespace, c.ToString()));
                //    Advance();
                //}
                if (char.IsLetter(c))
                {
                    tokens.Add(ReadIdentifier());
                }
                else if (char.IsDigit(c))
                {
                    tokens.Add(ReadNumber());
                }
                else if ("{}[]".Contains(c))
                {
                    Advance();
                    tokens.Add(ReadOperator());
                    Advance();
                }
                else     
                {
                    Advance();
                }
            }

            return tokens;
        }

        private Token ReadIdentifier()
        {
            StringBuilder sb = new StringBuilder();
            while (_position < _input.Length && char.IsLetterOrDigit(CurrentChar))
            {
                sb.Append(CurrentChar);
                Advance();
            }
            return new Token(TokenType.Identifier, sb.ToString());
        }

        private Token ReadNumber()
        {
            StringBuilder sb = new StringBuilder();
            while (_position < _input.Length && char.IsDigit(CurrentChar))
            {
                sb.Append(CurrentChar);
                Advance();
            }
            return new Token(TokenType.Number, sb.ToString());
        }

        private Token ReadOperator()
        {
            StringBuilder sb = new StringBuilder();
            char current = CurrentChar;
            while (_position < _input.Length-1)
            {
                sb.Append(CurrentChar);
                Advance();
            }
            if (current.Equals('{'))
                return new Token(TokenType.Identifier, sb.ToString());
            else
                return new Token(TokenType.Index, sb.ToString());

        }
    }
}
