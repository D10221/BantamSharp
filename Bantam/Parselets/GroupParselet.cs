using System;
using SimpleParser;


using IParser = SimpleParser.IParser<Bantam.TokenType>;

namespace Bantam
{
    /// <summary>
    /// Parses token used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get; }

        public ParseletType ParseletType { get; } = ParseletType.Prefix;

        public int Precedence { get; }

        public TokenType Right { get; }

        public GroupParselet(TokenType tokenType, TokenType right)
        {
            TokenType = tokenType;
            Right = right;
        }

        public ISimpleExpression<TokenType> Parse(
            IParser parser,
            ILexer<IToken<TokenType>> lexer,
            IToken<TokenType> token, ISimpleExpression<TokenType> _)
        {
            var expression = parser.Parse();
            if (expression == null || expression is EmptyExpression<TokenType>)
            {
                throw new ParseException($"GroupExpression can't be empty");
            }
            var (current, next) = lexer.ConsumeNext();
            next.Expect(Right);
            return expression;
        }
    }
}
