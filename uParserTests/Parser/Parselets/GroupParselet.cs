using System;
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
            Lexer lexer,
            Token token
            )
        {
            var els = new List<ISimpleExpression>();
            do
            {
                var e = parser.Parse();
                if (e != null)
                {
                    els.Add(e);
                }
                var next = lexer.LookAhead();
                if (next == null)
                {
                    // End;
                    break;
                }
                if (next.TokenType == Right)
                {
                    lexer.Consume();
                    break;
                }
            } while (true);
            if (!els.Any())
            {
                throw new ParseException($"{nameof(GroupExpression)} Can't be empty!");
            }
            return new GroupExpression(token, els.ToArray());
        }
    }
}
