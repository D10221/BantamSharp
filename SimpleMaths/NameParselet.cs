using ISimpleExpression = SimpleParser.ISimpleExpression<string>;
using IParser = SimpleParser.IParser<SimpleMaths.TokenType, string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;
using SimpleParser;

namespace SimpleMaths
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : IParselet<TokenType, string>
    {
        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            return new NameExpression(token.GetText());
        }
    }
}