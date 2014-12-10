using System.Text;

namespace Bantam.Expressions
{
   /**
 * A postfix unary arithmetic expression like "a!".
 */
public class PostfixExpression : Expression {
  public PostfixExpression(Expression left, TokenType @operator) {
    mLeft = left;
    mOperator = @operator;
  }
  
  public void Print(IBuilder builder) {
    builder.Append("(");
    mLeft.Print(builder);
    builder.Append(mOperator.Punctuator()).Append(")");
  }

  private readonly Expression mLeft;
  private readonly TokenType  mOperator;
}
}
