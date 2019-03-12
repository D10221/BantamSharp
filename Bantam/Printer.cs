using SimpleParser;

namespace Bantam
{
    public class Printer
    {
        public static Printer Default = new Printer();
        public string Print(ISimpleExpression<TokenType> expression)
        {
            var builder = new Builder();
            expression.Print(builder);
            return builder.ToString();
        }
    }
}