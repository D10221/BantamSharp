
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
    ///     unary "-", "+", "~", and "!" expressions.
    /// </summary>
    public class PrefixOperatorParselet : IParselet<TokenType>
    {
        public ParseletType ParseletType { get; } = ParseletType.Prefix;
        public TokenType TokenType { get; }
        public int Precedence { get; }

        public PrefixOperatorParselet(TokenType tokenType, int precedence)
        {
            TokenType = tokenType;
            Precedence = precedence;
        }

        public ISimpleExpression<TokenType> Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.Parse(Precedence);
            return new PrefixExpression(token, right);
        }
    }
}
