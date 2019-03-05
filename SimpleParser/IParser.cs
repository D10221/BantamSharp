using SimpleParser.Expressions;

namespace SimpleParser
{
    public interface IParser<TTokenType,TCHAR>
    {
        ISimpleExpression<TCHAR> ParseExpression(int precedence = 0);
        bool IsMatch(TTokenType expected);
        IToken<TTokenType> Consume(TTokenType expected);
        IToken<TTokenType> Consume();
    }
}