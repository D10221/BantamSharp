

using SimpleParser;

namespace Bantam
{
    public class LiteralExpression : ISimpleExpression<TokenType>
    {
        public static LiteralExpression From(string token)
        {
            return new LiteralExpression(
                SimpleParser.Token.From(TokenType.NAME, token)
            );
        }
        public IToken<TokenType> Token { get; }

        public LiteralExpression(IToken<TokenType> token)
        {
            Token = token;
        }

        public override string ToString()
        {
            return $"{nameof(LiteralExpression)}:\"{Token}\"";
        }
    }
}