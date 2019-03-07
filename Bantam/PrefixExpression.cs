
using SimpleParser;
using System;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : ISimpleExpression
    {        
        public PrefixExpression(
            IDictionary<TokenType, char> tokenTypes,
            TokenType tokenType,
            ISimpleExpression right)
        {
            Right = right;
            if (!tokenTypes.TryGetValue(tokenType, out var type))
            {
                throw new Exception($"Invalid tokenTypes:'{tokenType}'");
            }
            Punctuator = type;
        }

        protected char Punctuator { get; }

        protected ISimpleExpression Right { get; }

       
        public void Print(IBuilder builder)
        {
            builder.Append("(").Append(Punctuator.ToString());
            Right.Print(builder);
            builder.Append(")");
        }
    }
}
