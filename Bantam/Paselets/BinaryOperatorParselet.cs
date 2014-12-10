using Bantam.Expressions;

namespace Bantam.Paselets
{
    /**
 * Generic infix parselet for a binary arithmetic operator. The only
 * difference when parsing, "+", "-", "*", "/", and "^" is precedence and
 * associativity, so we can use a single parselet class for all of those.
 */
public class BinaryOperatorParselet : InfixParselet {

  public BinaryOperatorParselet(int precedence, bool isRight) {
    mPrecedence = precedence;
    mIsRight = isRight;
  }
  
  public Expression Parse(Parser parser, Expression left, Token token) {
    // To handle right-associative operators like "^", we allow a slightly
    // lower precedence when parsing the right-hand side. This will let a
    // parselet with the same precedence appear on the right, which will then
    // take *this* parselet's result as its left-hand argument.
    Expression right = parser.ParseExpression();
    
    return new OperatorExpression(left, token.GetTokenType(), right);
  }

  public int GetPrecedence() {
    return mPrecedence;
  }
  
  private readonly int mPrecedence;
  private readonly bool mIsRight;
}
}
