
using System;

namespace SimpleParser
{
    public interface IParselet<TTokenType> where TTokenType : Enum
    {
        ISimpleExpression Parse(IParser<TTokenType> parser, IToken<TTokenType> token);
    }
}