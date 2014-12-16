using SimpleParser.Expressions;

namespace SimpleParser
{
    public interface IParser<TTokenType,TCHAR>
    {
        ISimpleExpression<TCHAR> ParseExpression(Precedence precedence = Precedence.ZERO);
        bool IsMatch(TTokenType expected);
        IToken<TTokenType> Consume(TTokenType expected);
        IToken<TTokenType> Consume();
    }
}