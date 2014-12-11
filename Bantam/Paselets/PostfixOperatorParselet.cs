﻿using Bantam.Expressions;

namespace Bantam.Paselets
{
    /**
 * Generic infix parselet for an unary arithmetic operator. Parses postfix
 * unary "?" expressions.
 */
public class PostfixOperatorParselet : InfixParselet {
    public PostfixOperatorParselet(Precedence precedence)
    {
    mPrecedence = precedence;
  }
  
  public ISimpleExpression Parse(Parser parser, ISimpleExpression left, IToken token) {
    return new PostfixSimpleExpression(left, token.GetTokenType());
  }

  public int GetPrecedence() {
    return (int) mPrecedence;
  }
  
  private readonly Precedence mPrecedence;
}
}
