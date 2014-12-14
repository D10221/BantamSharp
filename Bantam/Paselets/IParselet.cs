using SimpleParser;

namespace Bantam.Paselets
{
    //TODO
    public interface IParselet
    {
        ISimpleExpression Parse(IParser parser, IToken token);
    }
}