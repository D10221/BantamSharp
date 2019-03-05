using Bantam.Expressions;
using SimpleParser;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;


namespace Bantam.Paselets
{
    public class AssignParselet : InfixParselet<TokenType, char>
    {
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            //Why -1
            Right = parser.ParseExpression(Precedence - 1);
            Left = left;
            //if (left as NameExpression == null)throw new ParseException("The left-hand side of an assignment must be a name.");

            var name = ((NameExpression)left).Name;
            return new AssignExpression(name, Right);
        }

        public int Precedence { get; } = (int)Bantam.Precedence.ASSIGNMENT;
    }
}
