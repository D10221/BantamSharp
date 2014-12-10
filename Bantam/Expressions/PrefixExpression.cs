using System.Text;

namespace Bantam.Expressions
{
   
/**
 * A prefix unary arithmetic expression like "!a" or "-b".
 */
public class PrefixExpression : Expression {
  public PrefixExpression(TokenType @operator, Expression right) {
    mOperator = @operator;
    mRight = right;
  }
  
  public void Print(IBuilder builder) {
    builder.Append("(").Append(mOperator.Punctuator());
    mRight.Print(builder);
    builder.Append(")");
  }

  private readonly TokenType  mOperator;
  private readonly Expression mRight;
}

}
