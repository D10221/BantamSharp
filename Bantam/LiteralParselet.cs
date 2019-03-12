

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

        public ISimpleExpression<TokenType> Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            return new LiteralExpression(token);
        }

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


            public void Print(IBuilder builder)
            {
                builder.Append(Token);
            }

            public override string ToString()
            {
                return $"{nameof(LiteralExpression)}:\"{Token}\"";
            }
        }
    }
}