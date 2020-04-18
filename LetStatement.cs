using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class LetStatement : StatetmentBase
    {
        public string Variable { get; set; }
        public Expression Value { get; set; }

        public override string ToString()
        {
            return "let " + Variable + " = " + Value + ";";
        }

        public override void Parse(TokensStack sTokens)
        {
            //let
            Token token = sTokens.Peek();
            if (sTokens.Count != 0 && !(token is Statement) || ((Statement)token).Name != "let")
            {
                throw new SyntaxErrorException("Expected let received: " + token, token);
            }
            else
            {
                sTokens.Pop();
            }

            //id
            token = sTokens.Peek();
            if (sTokens.Count != 0 && !(token is Identifier))
            {
                throw new SyntaxErrorException("Expected Identifier received: " + token, token);
            }
            else
            {
                sTokens.Pop();
                Variable = ((Identifier)token).Name;
            }

            //=
            token = sTokens.Pop();
            if (sTokens.Count != 0 && (token is Operator) && (((Operator)token).Name == '='))//change
            {
                Value = Expression.Create(sTokens);
                Value.Parse(sTokens);
            }
            else
            {
                throw new SyntaxErrorException("Expected = received: " + token, token);
                //sTokens.Pop();
            }

            //;
            token = sTokens.Peek();
            if (sTokens.Count != 0 && (token is Separator) && (((Separator)token).Name == ';') )//change
            {
                token = sTokens.Pop();
            }
            else
            {
                throw new SyntaxErrorException("Expected ; received: " + token, token);
            }



        }

    }
}
