using SimpleParser.Expressions;

namespace SimpleParser.Parselets
{
    public interface IPrefixParselet<TTokenType,TCHAR>
    {
        ISimpleExpression<TCHAR> Parse(IParser<TTokenType,TCHAR> parser, IToken<TTokenType> token);
    }
}