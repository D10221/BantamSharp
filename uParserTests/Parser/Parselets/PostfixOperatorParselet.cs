


namespace uParserTests
{
    /// <summary>
    /// Generic infix parselet for an unary arithmetic operator. Parses postfix
    /// unary "?" expressions.
    /// </summary>
    public class PostfixOperatorParselet : InfixParselet
    {
        public int Precedence { get; }

        public PostfixOperatorParselet(int precedence)
        {
            Precedence = precedence;
        }

        public ISimpleExpression Parse(
            Parser parser, 
            Lexer lexer,
            Token token, ISimpleExpression left)
        {
            return new PostfixExpression(token, left);
        }
    }
}
