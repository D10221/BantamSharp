using System.Collections.Generic;

namespace uParserTests
{
    public interface InfixParselet 
    {
         int Precedence { get; }

         ISimpleExpression Parse(Parser parser, IList<Token> lexer, Token token, ISimpleExpression left);        
    }
}