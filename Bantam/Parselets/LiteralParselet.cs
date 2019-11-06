

using SimpleParser;

namespace Bantam
{
    public class LiteralParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get; }
        public ParseletType ParseletType { get; } = ParseletType.Prefix;
        public int Precedence { get; }

        public LiteralParselet(TokenType tokenType)
        {
            TokenType = tokenType;
        }

        public ISimpleExpression<TokenType> Parse(
            IParser<TokenType> parser, 
            ILexer<IToken<TokenType>> lexer,
            IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            return new LiteralExpression(token);
        }

    }
}