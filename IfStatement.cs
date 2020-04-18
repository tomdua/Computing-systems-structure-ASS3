using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class IfStatement : StatetmentBase
    {
        public Expression Term { get; private set; }
        public List<StatetmentBase> DoIfTrue { get; private set; }
        public List<StatetmentBase> DoIfFalse { get; private set; }

        public override void Parse(TokensStack sTokens)
        {
            StatetmentBase statement;

            //if
            Token token = sTokens.Pop();
            if (((Statement)token).Name != ("if") || !(token is Statement))
            {
                throw new SyntaxErrorException("Expected if received: " + token, token);
            }                

            //(
            token = sTokens.Peek();
            if (sTokens.Count != 0 && token is Parentheses&& ((Parentheses)token).Name != '(')
            {
               throw new SyntaxErrorException("Expected ( received: " + token, token);
            }
            else
            {
                sTokens.Pop();
                Term = Expression.Create(sTokens);
                Term.Parse(sTokens);
           }
            //)
            token = sTokens.Peek();
            if (sTokens.Count != 0 && (token is Parentheses)&& ((Parentheses)token).Name != ')')
            {
                throw new SyntaxErrorException("Expected ) received: " + token, token);
            }
            else
                sTokens.Pop();

            DoIfTrue = new List<StatetmentBase>();
            //{
            token = sTokens.Pop();
             if (sTokens.Count != 0 &&(token is Parentheses)&& ((Parentheses)token).Name != '{' )
            {
               throw new SyntaxErrorException("Expected { received: " + token, token);
            }

            while (!(sTokens.Peek() is Parentheses) && sTokens.Count != 0 )
                {
                    statement = StatetmentBase.Create(sTokens.Peek()); // maybe pop?
                    statement.Parse(sTokens);
                    DoIfTrue.Add(statement);
                    //token = sTokens.Peek();
                }
            
            //}
            token = sTokens.Peek();
            if (sTokens.Count != 0 && ((Parentheses)token).Name != '}' && (token is Parentheses))
            {
                throw new SyntaxErrorException("Expected } received: " + token, token);
            }
            else
                sTokens.Pop();

            //else
            DoIfFalse = new List<StatetmentBase>();
            token = sTokens.Peek();
            if (sTokens.Count != 0 && ((Statement)token).Name == "else" && token is Statement)
            {
                //{
                token = sTokens.Pop();
                token = sTokens.Peek();
                if (sTokens.Count != 0 && (token is Parentheses) && ((Parentheses)token).Name != '{')
                {
                    throw new SyntaxErrorException("Expected { received: " + token, token);
                }
                else
                {
                    token = sTokens.Pop();
                    while (!(sTokens.Peek() is Parentheses) && sTokens.Count > 0)
                    {
                        statement = StatetmentBase.Create(sTokens.Peek()); // maybe pop?
                        statement.Parse(sTokens);
                        DoIfFalse.Add(statement);
                    }
                }
                if (sTokens.Count > 0 && (sTokens.Peek() is Parentheses) && ((Parentheses)sTokens.Peek()).Name == '}')
                {
                    token = sTokens.Pop();
                }
            }
        }

        public override string ToString()
        {
            string sIf = "if(" + Term + "){\n";
            foreach (StatetmentBase s in DoIfTrue)
                sIf += "\t\t\t" + s + "\n";
            sIf += "\t\t}";
            if (DoIfFalse.Count > 0)
            {
                sIf += "else{";
                foreach (StatetmentBase s in DoIfFalse)
                    sIf += "\t\t\t" + s + "\n";
                sIf += "\t\t}";
            }
            return sIf;
        }

    }
}
