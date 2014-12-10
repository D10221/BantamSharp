using Bantam;
using Bantam.Expressions;

public interface PrefixParselet
{
    Expression Parse(Parser parser, Token token);
}