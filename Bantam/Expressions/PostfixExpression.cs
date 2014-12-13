using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixSimpleExpression : ISimpleExpression
    {
        public PostfixSimpleExpression(ISimpleExpression left, TokenType @operator)
        {
            _left = left;
            _operator = @operator;
             _punctuator = _operator.Punctuator();
            if (!_punctuator.IsValidPunctuator()) throw new ParseException("Not A valid oprator");
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
