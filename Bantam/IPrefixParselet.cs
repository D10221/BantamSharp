using Bantam.Expressions;

namespace Bantam
{
    public interface IPrefixParselet
    {
        ISimpleExpression Parse(IParser parser, IToken token);
    }
}