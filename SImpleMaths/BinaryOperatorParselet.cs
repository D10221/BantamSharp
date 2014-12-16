
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
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType, string>;
using InfixParselet= SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType,string>;

namespace SimpleMaths
{
  
    /// <summary>
    /// Generic infix parselet for a binary arithmetic operator. The only
    /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
    /// associativity, so we can use a single parselet class for all of those.
    /// </summary>
    public class BinaryOperatorParselet : InfixParselet<TokenType, string>
    {
        public BinaryOperatorParselet(Precedence precedence, InfixType infixType, ITokenConfig tokenConfig)
        {
            _precedence = precedence;
            _tokenConfig = tokenConfig;
            _isRight = infixType== InfixType.Right;
        }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression(_precedence - (_isRight ? 1 : 0));

            return new OperatorExpression(_tokenConfig,left, token.TokenType, right);
        }

        public Precedence Precedence
        {
            get { return  _precedence; }
        }

        private readonly Precedence _precedence;
        private readonly bool _isRight;
        private readonly ITokenConfig _tokenConfig;
    }
}
