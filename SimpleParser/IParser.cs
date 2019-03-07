

using System;

namespace SimpleParser
{
    public interface IParser<TTokenType> 
    {
        ISimpleExpression ParseExpression(int precedence = 0);
        bool IsMatch(TTokenType expected);
        IToken<TTokenType> Consume(TTokenType expected);
        IToken<TTokenType> Consume();
    }
}