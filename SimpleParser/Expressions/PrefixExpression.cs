namespace SimpleParser.Expressions
{
    public abstract class PrefixExpressionBase<TTokenType, TTokenBase> : ISimpleExpression<TTokenBase>
    {
        protected PrefixExpressionBase(ITokenConfig<TTokenType, TTokenBase> tokenConfig,
            TTokenType @operator, ISimpleExpression<TTokenBase> right)
        {
            _right = right;
            _punctuator = tokenConfig.Punctuator(@operator);

            if (!tokenConfig.IsValidPunctuator(_punctuator))
                throw new ParseException<TTokenType>("Not A valid operator");
        }

        protected TTokenBase Punctuator
        {
            get { return _punctuator; }
        }

        protected ISimpleExpression<TTokenBase> Right
        {
            get { return _right; }
        }

        public abstract void Print(IBuilder<TTokenBase> builder);

        private readonly ISimpleExpression<TTokenBase> _right;

        private readonly TTokenBase _punctuator;
    }
}
