namespace SimpleParser
{
    public abstract class ConditionalExpressionBase<TCHAR> : ISimpleExpression<TCHAR>
    {
        protected ConditionalExpressionBase(
            ISimpleExpression<TCHAR> condition, ISimpleExpression<TCHAR> then, ISimpleExpression<TCHAR> @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

        protected ISimpleExpression<TCHAR> Condition { get; private set; }

        protected ISimpleExpression<TCHAR> Then { get; private set; }

        protected ISimpleExpression<TCHAR> Else { get; private set; }

        public abstract void Print(IBuilder<TCHAR> builder);
    }
}