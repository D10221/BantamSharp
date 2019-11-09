using System;
using System.Collections.Generic;

namespace uParserTests
{
    public interface PrefixParselet
    {
        ISimpleExpression Parse(Func<int,ISimpleExpression> parse, IList<Token> lexer, Token token);
    }
}