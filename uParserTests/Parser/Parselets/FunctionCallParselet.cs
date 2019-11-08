

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
            Token next = default;
            while (!lexer.ConsumeIf(Right, out next) && next != default)
            {
                if (lexer.TryPeek(out next) && next != default && next.TokenType == TokenType.COMMA)
                {
                    lexer.Consume();
                }
                else
                {
                    args.Add(parser.Parse());
                }
            }
            if (next?.TokenType != Right)
            {
                throw new ParseException($"Expected {Right} but found {next?.ToString() ?? "Nothing"}");
            }
            return new FunctionCallExpression(left, args);
        }
    }
}