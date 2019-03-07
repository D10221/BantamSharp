

using System;

namespace SimpleParser
{
    public interface IParser<TTokenType> where TTokenType: Enum
    {
        ISimpleExpression ParseExpression(int precedence = 0);
        bool IsMatch(TTokenType expected);
        IToken<TTokenType> Consume(TTokenType expected);
        IToken<TTokenType> Consume();
    }
}