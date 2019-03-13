using System.Collections.Generic;

namespace SimpleParser
{
    public interface IParser<TTokenType>
    {
        IEnumerable<IToken<TTokenType>> Tokens { get; }
        ISimpleExpression<TTokenType> ParseExpression(int precedence = 0, object caller = null);
        bool IsMatch(TTokenType expected);
        IToken<TTokenType> Consume(TTokenType expected);
        IToken<TTokenType> Consume();
    }
}