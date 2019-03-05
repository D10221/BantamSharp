using Bantam.Expressions;
using SimpleParser;

using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;

namespace Bantam.Paselets
{

    /// <summary>
    /// Generic infix parselet for a binary arithmetic operator. The only
    /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
    /// associativity, so we can use a single parselet class for all of those.
    /// </summary>
    public class BinaryOperatorParselet : InfixParselet<TokenType, char>
    {
        public int Precedence { get; }

        private readonly bool _isRight;
        private readonly ITokenConfig _tokenConfig;

        public BinaryOperatorParselet(int precedence, InfixType infixType, ITokenConfig tokenConfig)
        {
            Precedence = precedence;
            _tokenConfig = tokenConfig;
            _isRight = infixType == InfixType.Right;
        }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression(Precedence - (_isRight ? 1 : 0));

            return new OperatorExpression(_tokenConfig, left, token.TokenType, right);
        }


    }
}
