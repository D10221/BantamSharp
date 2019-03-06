
namespace SimpleParser
{
    public abstract class PostfixExpressionBase<TokenType, TCHAR> : ISimpleExpression<TCHAR>
    {
        protected PostfixExpressionBase(ITokenConfig<TokenType, TCHAR> TokenConfig, ISimpleExpression<TCHAR> left, TokenType @operator)
        {
            Left = left;
            Punctuator = TokenConfig.Punctuator(@operator);
            if (!TokenConfig.IsValidPunctuator(Punctuator)) throw new ParseException<TokenType>("Not A valid operator");
        }

        protected ISimpleExpression<TCHAR> Left { get; private set; }

        protected TCHAR Punctuator { get; private set; }

        public abstract void Print(IBuilder<TCHAR> builder);
    }
}