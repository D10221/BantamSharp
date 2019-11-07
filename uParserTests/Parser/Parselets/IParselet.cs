namespace uParserTests
{
    public interface PrefixParselet 
    {
        ISimpleExpression Parse(Parser parser, Lexer lexer, Token token);
    }    
}