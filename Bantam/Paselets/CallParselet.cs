using System.Collections.Generic;
using Bantam.Expressions;

namespace Bantam.Paselets
{
   
/**
 * Parselet to parse a function call like "a(b, c, d)".
 */
public class CallParselet : InfixParselet {

  public Expression Parse(Parser parser, Expression left, Token token) {
    // Parse the comma-separated arguments until we hit, ")".
    var args = new List<Expression>();
    
    // There may be no arguments at all.
      if (parser.IsMatch(TokenType.RIGHT_PAREN))
          return new CallExpression(left, args);
      do {
          args.Add(parser.ParseExpression());
      } while (parser.IsMatch(TokenType.COMMA));

      parser.Consume(TokenType.RIGHT_PAREN);

      return new CallExpression(left, args);
  }

  public int GetPrecedence() {
    return Precedence.CALL;
  }
}
}
