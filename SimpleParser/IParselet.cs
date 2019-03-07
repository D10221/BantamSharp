
using System;

namespace SimpleParser
{
    public interface IParselet<TTokenType> 
    {
        ISimpleExpression Parse(IParser<TTokenType> parser, IToken<TTokenType> token);
    }
}