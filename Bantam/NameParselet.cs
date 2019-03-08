

using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : IParselet<TokenType>
    {
        public TokenType TokenType {get; set;} 
        public ParseletType ParseletType { get; } = ParseletType.Prefix;

        public int Precedence { get; }

        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            return new NameExpression(token);
        }
    }
}