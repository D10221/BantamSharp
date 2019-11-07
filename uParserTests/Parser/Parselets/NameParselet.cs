

using System.Linq;


namespace uParserTests
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : PrefixParselet
    {
        public int Precedence { get; }

        public NameParselet()
        {

        }

        public ISimpleExpression Parse(
            Parser parser,
            Lexer lexer,
            Token token
            )
        {
            return new NameExpression(token);
        }
    }
}