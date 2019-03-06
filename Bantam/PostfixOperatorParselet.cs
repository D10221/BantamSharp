
using SimpleParser;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;

namespace Bantam
{
    /// <summary>
    /// Generic infix parselet for an unary arithmetic operator. Parses postfix
    /// unary "?" expressions.
    /// </summary>
    public class PostfixOperatorParselet : InfixParselet<TokenType, char>
    {
        private readonly ITokenConfig _tokenConfig;

        public int Precedence { get; }

        public PostfixOperatorParselet(int precedence, ITokenConfig tokenConfig)
        {
            _tokenConfig = tokenConfig;
            Precedence = precedence;
        }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            return new PostfixExpression(_tokenConfig, left, token.TokenType);
        }

    }
}
