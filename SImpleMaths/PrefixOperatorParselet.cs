using SimpleParser;
using SimpleParser.Parselets;
using ParseException = SimpleParser.ParseException<SimpleMaths.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<SimpleMaths.TokenType, string>;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType, string>>;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType, string>>;
using ParserConfig = SimpleParser.ParserConfig<SimpleMaths.TokenType, string>;
using ParserMap = SimpleParser.ParserMap<SimpleMaths.TokenType, string>;
using IParserMap = SimpleParser.IParserMap<SimpleMaths.TokenType, string>;
using Parser = SimpleParser.Parser<SimpleMaths.TokenType, string>;
using IBuilder = SimpleParser.IBuilder<string>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<string>;
using IParser = SimpleParser.IParser<SimpleMaths.TokenType, string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType,string>;

namespace SimpleMaths
{
    /// <summary>
    ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
    ///     unary "-", "+", "~", and "!" expressions.
    /// </summary>
    public class PrefixOperatorParselet : IPrefixParselet<TokenType, string>, IParselet
    {
        public PrefixOperatorParselet(Precedence precedence,ITokenConfig tokenConfig)
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
        private readonly ITokenConfig _tokenConfig;
    }
}
