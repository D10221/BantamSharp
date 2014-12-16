using Bantam.Expressions;
using SimpleParser;
using SimpleParser.Parselets;
using ParseException = SimpleParser.ParseException<Bantam.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>;


namespace Bantam.Paselets
{
    public class AssignParselet : InfixParselet<TokenType, char>
    {
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            //Why -1
            Right = parser.ParseExpression(Precedence-1);
            Left = left;      
            //if (left as NameExpression == null)throw new ParseException("The left-hand side of an assignment must be a name.");

            var name = ((NameExpression) left).Name;
            return new AssignExpression(name, Right);
        }

        public Precedence Precedence
        {
            get { return Precedence.ASSIGNMENT; }
        }
    }
}
