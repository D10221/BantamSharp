namespace SimpleParser
{
    public abstract class OperatorExpressionBase<TTokenType,TCHAR> : ISimpleExpression<TCHAR>
    {
        protected OperatorExpressionBase(ITokenConfig<TTokenType, TCHAR> tokenConfig,ISimpleExpression<TCHAR> left, TTokenType @operator, ISimpleExpression<TCHAR> right)
        {
            Left = left;
            Right = right;
            Punctuator = tokenConfig.Punctuator(@operator);
            if (!tokenConfig.IsValidPunctuator(Punctuator)) throw new ParseException<TTokenType>("Not A valid operator");
            TokenConfig = tokenConfig;
        }

        protected ISimpleExpression<TCHAR> Left { get; private set; }

        protected ISimpleExpression<TCHAR> Right { get; private set; }

        protected ITokenConfig<TTokenType, TCHAR> TokenConfig { get; set; }

        protected TCHAR Punctuator { get; set; }

        public abstract void Print(IBuilder<TCHAR> builder);
    }
}