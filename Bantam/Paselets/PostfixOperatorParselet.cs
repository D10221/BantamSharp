using Bantam.Expressions;

namespace Bantam.Paselets
{
    /**
 * Generic infix parselet for an unary arithmetic operator. Parses postfix
 * unary "?" expressions.
 */
public class PostfixOperatorParselet : InfixParselet {
  public PostfixOperatorParselet(int precedence) {
    mPrecedence = precedence;
  }
  
  public Expression Parse(Parser parser, Expression left, Token token) {
    return new PostfixExpression(left, token.GetTokenType());
  }

  public int GetPrecedence() {
    return mPrecedence;
  }
  
  private readonly int mPrecedence;
}
}
