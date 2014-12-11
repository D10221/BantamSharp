using Bantam.Expressions;

namespace Bantam.Paselets
{
 
  /**
 * Generic prefix parselet for an unary arithmetic operator. Parses prefix
 * unary "-", "+", "~", and "!" expressions.
 */
public class PrefixOperatorParselet : IPrefixParselet {
    public PrefixOperatorParselet(Precedence precedence)
    {
    mPrecedence = precedence;
  }
  
  public ISimpleExpression Parse(IParser parser, IToken token) {
    // To handle right-associative operators like "^", we allow a slightly
    // lower precedence when parsing the right-hand side. This will let a
    // parselet with the same precedence appear on the right, which will then
    // take *this* parselet's result as its left-hand argument.
    ISimpleExpression right = parser.ParseExpression();
    
    return new PrefixSimpleExpression(token.GetTokenType(), right);
  }

  public int getPrecedence() {
    return (int) mPrecedence;
  }
  
  private readonly Precedence mPrecedence;
}
}
