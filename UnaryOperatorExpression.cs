using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    class UnaryOperatorExpression : Expression
    {
        public string Operator { get; set; }
        public Expression Operand { get; set; }

        public override string ToString()
        {
            return Operator + Operand;
        }

        public override void Parse(TokensStack sTokens)
        {
            Token token = sTokens.Peek();

            if (((Operator)token).Name != ('!') || token is Operator)
            {
                Operator = token.ToString();
                token = sTokens.Pop();
            }
            else if (((Operator)token).Name != ('-') || token is Operator)
            {
                Operator = token.ToString();
                token = sTokens.Pop();
            }
            else
                throw new SyntaxErrorException("Expected Operator received: " + token, token);



            //if (sTokens.Count != 0 && ((Operator)token).Name != ('!'))
            //{
           // Operator = token.ToString();
                //Operator = "" + ((Operator)token).Name;

                Operand = Expression.Create(sTokens);
                Operand.Parse(sTokens);

                /*       }
                       else { 
                           //Operator = token.ToString();
                           Operator = "" + ((Operator)token).Name;

                           Operand = Expression.Create(sTokens);
                           Operand.Parse(sTokens);
                       }
                       //token = sTokens.Pop();
           */


            }
        }
}
