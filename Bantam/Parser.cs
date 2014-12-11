using System.Collections.Generic;
using System.Linq;
using SimpleParser;

namespace Bantam
{
    public class Parser : IParser
    {
        private readonly ILexer _lexer;

        public Parser(ILexer lexer)
        {
            _lexer = lexer;
        }
        
        public void Register(TokenType token, IPrefixParselet parselet) {
            _parselets.Add(token, parselet);
        }
  
        public void Register(TokenType token, InfixParselet parselet) {
            _infixParselets.Add(token, parselet);
        }
  
        public ISimpleExpression ParseExpression() {
           
            var token = Consume();
                      
            var tokenType = token.GetTokenType();

            IPrefixParselet prefix;
            if (!_parselets.TryGetValue(tokenType, out prefix)) 
                throw new ParseException(token);

            var left = prefix.Parse(this, token); //Expression
    
            while (0 < GetPrecedence()) {
                
                var atoken = Consume();

                InfixParselet infix;
                if (_infixParselets.TryGetValue(atoken.GetTokenType(),out infix))
                {                                        
                    left = infix.Parse(this, left, atoken);
                }
            }
    
            return left;
        }
        
  
        public bool IsMatch(TokenType expected) {
            IToken token = lookAhead();
            if (token.GetTokenType() != expected) {
                return false;
            }
    
            Consume();
            return true;
        }
  
        public IToken Consume(TokenType expected) {
            IToken token = lookAhead();
            if (token.GetTokenType() != expected) {
                throw new ParseException("Expected token {0} and found {1}", expected, token.GetTokenType());
            }
    
            return Consume();
        }
  
        public IToken Consume() {
            // Make sure we've read the token.
            lookAhead();
            var token  = _tokens.First();
            _tokens.RemoveAt(0);
            return token;
        }
  
        private IToken lookAhead() {

            while (!_tokens.Any())
            {
                var token = _lexer.Next();
                _tokens.Add(token);
            }
            return _tokens.First();            
        }

        private int GetPrecedence() {
            int precedence = 0;

            var token = lookAhead();
            var tokenType = token.GetTokenType();
            
            if (tokenType == TokenType.NONE) 
                return precedence;

            InfixParselet parser ;
            if (_infixParselets.TryGetValue(tokenType, out parser))
                precedence =  parser.GetPrecedence();
    
            return precedence;
        }

        private readonly List<IToken> _tokens = new List<IToken>();
        private readonly IDictionary<TokenType, IPrefixParselet> _parselets =
            new Dictionary<TokenType, IPrefixParselet>();
        private readonly IDictionary<TokenType, InfixParselet> _infixParselets =
            new Dictionary<TokenType, InfixParselet>();
    }
}