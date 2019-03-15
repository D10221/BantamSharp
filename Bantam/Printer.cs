using SimpleParser;

namespace Bantam
{
    public class Printer
    {
        public static Printer Default = new Printer();
        public string Print(ISimpleExpression<TokenType> expression)
        {
            var builder = new Builder();
            builder.Visit(expression);
            return builder.ToString();
        }
    }
}