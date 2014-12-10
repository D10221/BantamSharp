using Bantam.Expressions;

namespace Bantam.Paselets
{
    /**
 * Parses parentheses used to group an expression, like "a * (b + c)".
 */
public class GroupParselet : PrefixParselet {
  public Expression Parse(Parser parser, Token token) {
    Expression expression = parser.ParseExpression();
    parser.Consume(TokenType.RIGHT_PAREN);
    return expression;
  }
}
}
