
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleMaths.TokenType, string>;
using ISimpleExpression = SimpleParser.ISimpleExpression<string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;
namespace SimpleMaths
{
    public class AssignParselet : InfixParselet<TokenType, string>
    {
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            //Why -1
            Right = parser.ParseExpression(Precedence - 1);
            Left = left;
            //if (left as NameExpression == null)throw new ParseException("The left-hand side of an assignment must be a name.");

            var name = ((NameExpression)left).GetName();
            return new AssignExpression(name, Right);
        }

        public int Precedence { get; } = (int)SimpleMaths.Precedence.ASSIGNMENT;
    }
}
