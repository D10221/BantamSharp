using System.Text;

namespace Bantam.Expressions
{
   
/**
 * A binary arithmetic expression like "a + b" or "c ^ d".
 */
public class OperatorExpression : Expression {
  public OperatorExpression(Expression left, TokenType @operator, Expression right) {
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

  private readonly Expression mLeft;
  private readonly TokenType  mOperator;
  private readonly Expression mRight;
}

    public interface IBuilder
    {
        IBuilder Append(string s);
        IBuilder Append(char c);
    }
}
