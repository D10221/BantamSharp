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
using InfixParselet= SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType,string>;
namespace SimpleMaths
{
    public class AssignParselet : InfixParselet<TokenType, string>
    {
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            //Why -1
            Right = parser.ParseExpression(Precedence-1);
            Left = left;      
            //if (left as NameExpression == null)throw new ParseException("The left-hand side of an assignment must be a name.");

            var name = ((NameExpression) left).GetName();
            return new AssignExpression(name, Right);
        }

        public Precedence Precedence
        {
            get { return Precedence.ASSIGNMENT; }
        }
    }
}
