using SimpleParser;
using ParseException = SimpleParser.ParseException<SimpleParser.TokenType>;
namespace Bantam.Expressions
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixExpression : ISimpleExpression
    {
        public PostfixExpression(ITokenConfig<char> TokenConfig,ISimpleExpression left, TokenType @operator)
        {
            _left = left;
            _operator = @operator;
            _punctuator = TokenConfig.Punctuator(_operator);
            if (!TokenConfig.IsValidPunctuator(_punctuator)) throw new ParseException("Not A valid operator");
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            _left.Print(builder);           
            builder.Append(_punctuator).Append(")");
        }

       

        private readonly ISimpleExpression _left;
        private readonly TokenType _operator;
        private readonly char _punctuator;
    }
}
