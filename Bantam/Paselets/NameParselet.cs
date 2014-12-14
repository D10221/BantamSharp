using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : IPrefixParselet
    {
        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            return new NameExpression(token.GetText());
        }
    }
}