using System;
using System.Collections.Generic;

namespace uParserTests
{
    public interface InfixParselet 
    {
         int Precedence { get; }

         ISimpleExpression Parse(Func<int,ISimpleExpression> parse, IList<Token> lexer, Token token, ISimpleExpression left);        
    }
}