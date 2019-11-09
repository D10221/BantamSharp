


namespace uParser
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ISimpleExpression 
    {
        public Token Token { get; }
        public ISimpleExpression Condition { get; }
        public ISimpleExpression Then { get; }
        public ISimpleExpression Else { get; }

        public ConditionalExpression(
           ISimpleExpression condition, ISimpleExpression then, ISimpleExpression @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }
        public override string ToString()
        {
            return $"{Token}";
        }

    }
}