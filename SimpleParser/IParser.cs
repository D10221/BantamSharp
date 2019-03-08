using System.Collections.Generic;

namespace SimpleParser
{
    public interface IParser<TTokenType>
    {
        IEnumerable<IToken<TTokenType>> Tokens { get; }
        ISimpleExpression ParseExpression(int precedence = 0);
        bool IsMatch(TTokenType expected);
        IToken<TTokenType> Consume(TTokenType expected);
        IToken<TTokenType> Consume();
    }
}