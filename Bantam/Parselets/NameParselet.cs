

using System.Linq;
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get; }
        public ParseletType ParseletType { get; } = ParseletType.Prefix;
        public int Precedence { get; }

        public NameParselet(TokenType tokenType)
        {
            TokenType = tokenType;
        }

        public ISimpleExpression<TokenType> Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {               
            return new NameExpression(token);
        }
    }
}