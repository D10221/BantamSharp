

using IParser = SimpleParser.IParser<Bantam.TokenType>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : IParselet<TokenType>
    {
        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            return new NameExpression(token);
        }
    }
}