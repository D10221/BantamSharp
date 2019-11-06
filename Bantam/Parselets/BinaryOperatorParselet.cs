
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Generic infix parselet for a binary arithmetic operator. The only
    /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
    /// associativity, so we can use a single parselet class for all of those.
    /// </summary>
    public class BinaryOperatorParselet : IParselet<TokenType>
    {
        public int Precedence { get; }
        public TokenType TokenType { get; set; }
        public ParseletType ParseletType { get; } = ParseletType.Infix;

        public bool IsRight { get; }

        public BinaryOperatorParselet(TokenType tokenType, int precedence, InfixType infixType)
        {
            TokenType = tokenType;
            Precedence = precedence;
            IsRight = infixType == InfixType.Right;
        }

        public ISimpleExpression<TokenType> Parse(
                IParser<TokenType> parser, 
                ILexer<IToken<TokenType>> lexer,
                IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.Parse(Precedence - (IsRight ? 1 : 0));
            return new BinaryOperatorExpression(token, left, right);
        }
    }
}
