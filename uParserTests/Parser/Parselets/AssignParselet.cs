
namespace uParserTests
{
    public class AssignParselet : InfixParselet
    {
        public int Precedence => (int)uParserTests.Precedence.ASSIGNMENT;

        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(
                Parser parser,
                Lexer lexer,
                Token token,
                ISimpleExpression left)
        {
            //Why -1
            Right = parser.Parse(Precedence - 1);
            Left = left;
            if (left as NameExpression == null)
                throw new ParseException($"Expected {nameof(NameExpression)} but found {left}.");

            return new AssignExpression(left, Right);
        }
    }
}
