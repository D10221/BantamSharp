


using System;
using System.Collections.Generic;

namespace uParserTests
{
    /// <summary>
    ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
    /// </summary>
    public class ConditionalParselet : InfixParselet
    {
      
        public ISimpleExpression Parse(
            Func<int,ISimpleExpression> parse, 
            IList<Token> lexer,
            Token token, 
            ISimpleExpression left)
        {
            var thenArm = parse(0);
            var next  = lexer.Consume();
            if(next.TokenType != TokenType.COLON) 
            throw new ParseException("Expected COLON");
            //WHy  precedence -1 
            var elseArm = parse(Precedence - 1);
            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence => (int) uParserTests.Precedence.CONDITIONAL;
    }
}
