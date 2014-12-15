using Bantam.Expressions;
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleParser.TokenType>;
using IPrefixParselet = SimpleParser.IPrefixParselet<SimpleParser.TokenType>;
using IToken = SimpleParser.IToken<SimpleParser.TokenType>;
using InfixParselet = SimpleParser.InfixParselet<SimpleParser.TokenType>;

namespace Bantam.Paselets
{
    /// <summary>
    ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
    ///     unary "-", "+", "~", and "!" expressions.
    /// </summary>
    public class PrefixOperatorParselet : IPrefixParselet, IParselet
    {
        public PrefixOperatorParselet(Precedence precedence,ITokenConfig<char> tokenConfig)
        {
            _tokenConfig = tokenConfig;
            _precedence = precedence;
        }

        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression(_precedence);

            return new PrefixExpression(_tokenConfig, token.TokenType, right);
        }

        public Precedence Precedence
        {
            get { return  _precedence; }
        }

        private readonly Precedence _precedence;
        private readonly ITokenConfig<char> _tokenConfig;
    }
}
