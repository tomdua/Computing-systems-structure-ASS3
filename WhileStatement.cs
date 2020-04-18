using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class WhileStatement : StatetmentBase
    {
        public Expression Term { get; private set; }
        public List<StatetmentBase> Body { get; private set; }

        public override void Parse(TokensStack sTokens)
        {
            Body = new List<StatetmentBase>();
            //while ( X ) { }
            //Token tWhile, firstBracket, secandBracket, firstBracketLoop, secandBracketLoop;
            Token token = sTokens.Pop();
            StatetmentBase statement;
            //while
                 if (!(token is Statement) || ((Keyword)token).Name != "while" )
                throw new SyntaxErrorException("Expected While received: " + token, token);

            //(
            token = sTokens.Pop();
            
            if (sTokens.Count != 0 && (token is Parentheses) && ((Parentheses)token).Name == ('('))
            {
                Term = Expression.Create(sTokens);
                Term.Parse(sTokens);
            }
            else
            {
                throw new SyntaxErrorException("Expected ( received: " + token, token);
                //token = sTokens.Pop();
                
            }

            //)
            token = sTokens.Peek();
            if (sTokens.Count > 0 && (token is Parentheses) && ((Parentheses)token).Name == ')')
            {
                token = sTokens.Pop();
            }
            else
            {
                throw new SyntaxErrorException("Expected ) received: " + token, token);
            }


            //{
            token = sTokens.Peek();
            if (sTokens.Count != 0 && (token is Parentheses) && ((Parentheses)token).Name == ('{'))
            {
                token = sTokens.Pop();
            
            }
            else
            {
                throw new SyntaxErrorException("Expected { received: " + token, token);
            }

            //body
            //token = sTokens.Peek();
            while (!(sTokens.Peek() is Parentheses) && sTokens.Count !=0)
            {
                statement = StatetmentBase.Create(sTokens.Peek());
                statement.Parse(sTokens);
                Body.Add(statement);
                //token = sTokens.Peek();
            }

            //} finish
            token = sTokens.Peek();
            if (sTokens.Count != 0 && (token is Parentheses) && ((Parentheses)token).Name == ('}'))
            {
                sTokens.Pop();

            }
            else
            {
                throw new SyntaxErrorException("Expected } received: " + token, token);
            }
            //donothing
        }

        public override string ToString()
        {
            string sWhile = "while(" + Term + "){\n";
            foreach (StatetmentBase s in Body)
                sWhile += "\t\t\t" + s + "\n";
            sWhile += "\t\t}";
            return sWhile;
        }

    }

}
