


namespace uParserTests
{
    /// <summary>
    ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
    /// </summary>
    public class ConditionalParselet : InfixParselet
    {
      
        public ISimpleExpression Parse(
            Parser parser, 
            Lexer lexer,
            Token token, 
            ISimpleExpression left)
        {
            var thenArm = parser.Parse();
            var next  = lexer.Consume();
            if(next.TokenType != TokenType.COLON) 
            throw new ParseException("Expected COLON");
            //WHy  precedence -1 
            var elseArm = parser.Parse(Precedence - 1);
            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence => (int) uParserTests.Precedence.CONDITIONAL;
    }
}
