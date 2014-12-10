using System;
using System.Collections.Generic;
using System.Linq;
using Bantam.Expressions;
using Bantam.Paselets;

namespace Bantam
{
    public class Parser
    {
        private readonly Lexer _lexer;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
        }
        
        public void Register(TokenType token, PrefixParselet parselet) {
            _parselets.Add(token, parselet);
        }
  
        public void Register(TokenType token, InfixParselet parselet) {
            _infixParselets.Add(token, parselet);
        }
  
        public Expression ParseExpression() {
           
            var token = Consume();
                      
            var tokenType = token.GetTokenType();

            PrefixParselet prefix;
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
            Token token = lookAhead();
            if (token.GetTokenType() != expected) {
                return false;
            }
    
            Consume();
            return true;
        }
  
        public Token Consume(TokenType expected) {
            Token token = lookAhead();
            if (token.GetTokenType() != expected) {
                throw new ParseException("Expected token {0} and found {1}", expected, token.GetTokenType());
            }
    
            return Consume();
        }
  
        public Token Consume() {
            // Make sure we've read the token.
            lookAhead();
            var token  = _tokens.First();
            _tokens.RemoveAt(0);
            return token;
        }
  
        private Token lookAhead() {

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

        private readonly List<Token> _tokens = new List<Token>();
        private readonly IDictionary<TokenType, PrefixParselet> _parselets =
            new Dictionary<TokenType, PrefixParselet>();
        private readonly IDictionary<TokenType, InfixParselet> _infixParselets =
            new Dictionary<TokenType, InfixParselet>();
    }
}