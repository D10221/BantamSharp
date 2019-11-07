namespace uParserTests
{
    public interface InfixParselet 
    {
         int Precedence { get; }

         ISimpleExpression Parse(Parser parser, Lexer lexer, Token token, ISimpleExpression left);        
    }
}