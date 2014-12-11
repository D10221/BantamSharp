using System.Text;

namespace Bantam.Expressions
{
   
/**
 * A binary arithmetic expression like "a + b" or "c ^ d".
 */
public class OperatorSimpleExpression : ISimpleExpression {
  public OperatorSimpleExpression(ISimpleExpression left, TokenType @operator, ISimpleExpression right) {
    mLeft = left;
    mOperator = @operator;
    mRight = right;
  }
  
  public void Print(IBuilder builder) {
    builder.Append("(");
    mLeft.Print(builder);
    builder.Append(" ").Append(mOperator.Punctuator()).Append(" ");
    mRight.Print(builder);
    builder.Append(")");
  }

  private readonly ISimpleExpression mLeft;
  private readonly TokenType  mOperator;
  private readonly ISimpleExpression mRight;
}
}
