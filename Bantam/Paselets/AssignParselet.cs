using Bantam.Expressions;
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleParser.TokenType>;
using IPrefixParselet = SimpleParser.IPrefixParselet<SimpleParser.TokenType>;
using IToken = SimpleParser.IToken<SimpleParser.TokenType>;
using InfixParselet = SimpleParser.InfixParselet<SimpleParser.TokenType>;

namespace Bantam.Paselets
{
    public class AssignParselet : InfixParselet
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
