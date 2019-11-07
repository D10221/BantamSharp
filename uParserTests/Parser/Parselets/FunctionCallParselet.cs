

using System.Collections.Generic;
using System.Linq;

namespace uParserTests
{
    /// <summary>
    ///     Parselet to parse a function call like "a(b, c, d)".
    /// </summary>
    public class CallParselet : InfixParselet
    {
        public int Precedence => (int)uParserTests.Precedence.CALL;

        public TokenType Right { get; } = TokenType.PARENT_RIGHT;

        public ISimpleExpression Parse(
            Parser parser,
            Lexer lexer,
            Token token,
            ISimpleExpression left)
        {
            var args = new List<ISimpleExpression>();
            Token next = null;
            do
            {
                next = lexer.LookAhead();
                next = next ?? lexer.Consume(); // ignore EOF                
                if (next == null)
                {
                    break;
                }
                if (next.TokenType != Right)
                {
                    var e = parser.Parse();
                    if (e != null)
                    {
                        args.Add(e);
                    }
                }
                else
                {
                    lexer.Consume();
                }
            } while (next.TokenType != Right);

            if (next?.TokenType != Right)
            {
                throw new ParseException($"Expected {Right} but found {next?.TokenType}");
            }
            return new FunctionCallExpression(left, args);
        }
    }
}