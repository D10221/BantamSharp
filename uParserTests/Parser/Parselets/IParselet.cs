using System.Collections.Generic;

namespace uParserTests
{
    public interface PrefixParselet
    {
        ISimpleExpression Parse(Parser parser, IList<Token> lexer, Token token);
    }
}