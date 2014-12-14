using Bantam.Paselets;
using SimpleParser;

namespace Bantam
{
    
/// <summary>
/// Extends the generic Parser class with support for parsing the actual Bantam
/// grammar
/// </summary>
public class BantamParser : Parser {
  public BantamParser(ILexer lexer,IParserMap parserMap) :base(lexer,parserMap){
        
    
  }
  
  
  
 
}
}
