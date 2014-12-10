using Bantam.Expressions;

namespace Bantam.Paselets
{
    /**
 * Generic prefix parselet for an unary arithmetic operator. Parses prefix
 * unary "-", "+", "~", and "!" expressions.
 */
public class PrefixOperatorParselet : PrefixParselet {
  public PrefixOperatorParselet(int precedence) {
    mPrecedence = precedence;
  }
  
  public Expression Parse(Parser parser, Token token) {
    // To handle right-associative operators like "^", we allow a slightly
    // lower precedence when parsing the right-hand side. This will let a
    // parselet with the same precedence appear on the right, which will then
    // take *this* parselet's result as its left-hand argument.
    Expression right = parser.ParseExpression();
    
    return new PrefixExpression(token.GetTokenType(), right);
  }

  public int getPrecedence() {
    return mPrecedence;
  }
  
  private readonly int mPrecedence;
}
}
