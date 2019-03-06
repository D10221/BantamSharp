
using System;
using System.Collections.Generic;

namespace SimpleParser
{
    public abstract class PrefixExpressionBase<TTokenType, TTokenBase> : ISimpleExpression<TTokenBase>
    {
        protected PrefixExpressionBase(
            IDictionary<TTokenType, TTokenBase> tokenTypes,
            TTokenType tokenType, 
            ISimpleExpression<TTokenBase> right)
        {
            Right = right;
            if(!tokenTypes.TryGetValue(tokenType, out var type))
            {
                throw new Exception($"Invalid tokenTypes:'{tokenType}'");
            }
            Punctuator = type;
        }

        protected TTokenBase Punctuator { get; }

        protected ISimpleExpression<TTokenBase> Right { get; }

        public abstract void Print(IBuilder<TTokenBase> builder);
    }
}
