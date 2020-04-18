using System;
using System.Collections.Generic;

namespace SimpleCompiler
{
    public class FunctionCallExpression : Expression
    {
        public string FunctionName { get; private set; }
        public List<Expression> Args { get; private set; }

        public override void Parse(TokensStack sTokens)
        {
            Token token = sTokens.Peek();
                 if (!(token is Identifier))
                 {
                    throw new SyntaxErrorException("Expected function name, received " + token, token);
                 }
                else
               {
                token=  sTokens.Pop();
            FunctionName = ((Identifier)token).Name;
               }

            //(
            Args = new List<Expression>();
            token = sTokens.Pop();
            if (sTokens.Count != 0 && ((Parentheses)token).Name != '(' && (token is Parentheses))
            {
                throw new SyntaxErrorException("Expected ( received: " + token, token);
            }
            else
            {
                Expression exp = Expression.Create(sTokens);
                exp.Parse(sTokens);
                Args.Add(exp);
            }

            //,
            //token = sTokens.Peek();
            if (sTokens.Count != 0 && sTokens.Peek() is Separator && (((Separator)sTokens.Peek()).Name == ','))
            {
                token = sTokens.Pop();
                Expression exp = Expression.Create(sTokens);
                exp.Parse(sTokens);
                Args.Add(exp);
                //sTokens.Pop();
                //token = sTokens.Peek();
            }

           token = sTokens.Pop();
        }
        public override string ToString()
        {
            string sFunction = FunctionName + "(";
            for (int i = 0; i < Args.Count - 1; i++)
                sFunction += Args[i] + ",";
            if (Args.Count > 0)
                sFunction += Args[Args.Count - 1];
            sFunction += ")";
            return sFunction;
        }
    }
}