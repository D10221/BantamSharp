
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ISimpleExpression<TokenType> 
    {
        public IToken<TokenType> Token { get; }
        public ISimpleExpression<TokenType> Condition { get; }
        public ISimpleExpression<TokenType> Then { get; }
        public ISimpleExpression<TokenType> Else { get; }

        public ConditionalExpression(
           ISimpleExpression<TokenType> condition, ISimpleExpression<TokenType> then, ISimpleExpression<TokenType> @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

    }
}