using Bantam.Expressions;
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleParser.TokenType>;
using IPrefixParselet = SimpleParser.IPrefixParselet<SimpleParser.TokenType>;
using IToken= SimpleParser.IToken<SimpleParser.TokenType>;
namespace Bantam.Paselets
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : IPrefixParselet
    {
        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            return new NameExpression(token.GetText());
        }
    }
}