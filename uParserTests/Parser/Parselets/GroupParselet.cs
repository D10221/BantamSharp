using System.Collections.Generic;
using System.Linq;

namespace uParserTests
{
    /// <summary>
    /// Parses token used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : PrefixParselet
    {
        public TokenType Right { get; }
        public GroupParselet(TokenType right)
        {
            Right = right;
        }
        public ISimpleExpression Parse(
            Parser parser,
            IList<Token> lexer,
            Token token
            )
        {
            var els = new List<ISimpleExpression>();
            Token next; // default is EOF
            while (!lexer.TryPeek(Right, out next) && next != default)
            {
                els.Add(parser.Parse());
            }
            if (next?.TokenType == Right)
            {
                lexer.Consume(next);
            }
            else
            {
                throw new ParseException(
                    $"Expected {Right} but found {next?.ToString() ?? "default"}");
            }
            if (!els.Any())
            {
                throw new ParseException($"{nameof(GroupExpression)} Can't be empty!");
            }
            return new GroupExpression(token, els.ToArray());
        }
    }
}
