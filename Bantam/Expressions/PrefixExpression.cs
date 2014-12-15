using SimpleParser;
using ParseException = SimpleParser.ParseException<SimpleParser.TokenType>;

namespace Bantam.Expressions
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : ISimpleExpression
    {
        public PrefixExpression(ITokenConfig<char> tokenConfig,TokenType @operator, ISimpleExpression right)
        {
            _right = right;
            _punctuator = tokenConfig.Punctuator(@operator);

            if (!tokenConfig.IsValidPunctuator(_punctuator))
                throw new ParseException("Not A valid operator");
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(").Append(_punctuator);
            _right.Print(builder);
            builder.Append(")");
        }

        private readonly ISimpleExpression _right;
        private readonly char _punctuator;
    }
}
