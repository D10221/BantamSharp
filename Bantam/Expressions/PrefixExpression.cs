using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixSimpleExpression : ISimpleExpression
    {
        public PrefixSimpleExpression(TokenType @operator, ISimpleExpression right)
        {
            _operator = @operator;
            _right = right;
            _punctuator = _operator.Punctuator();
            if (!_punctuator.IsValidPunctuator()) throw new ParseException("Not A valid oprator");
        }

        public void Print(IBuilder builder)
        {
 
            builder.Append("(").Append(_punctuator);
            _right.Print(builder);
            builder.Append(")");
        }

        private readonly TokenType _operator;
        private readonly ISimpleExpression _right;
        private char _punctuator;
    }
}
