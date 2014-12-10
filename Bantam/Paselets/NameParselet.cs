using Bantam.Expressions;

namespace Bantam.Paselets
{

    /**
 * Simple parselet for a named variable like "abc".
 */
    public class NameParselet : PrefixParselet {
        public Expression Parse(Parser parser, Token token) {
            return new NameExpression(token.GetText());
        }
    }
}