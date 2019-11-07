using System.Collections.Generic;

namespace uParserTests
{
    public class Parser
    {
        private Lexer _lexer;
        private IDictionary<TokenType, PrefixParselet> prefixParselets = new Dictionary<TokenType, PrefixParselet>();
        private IDictionary<TokenType, InfixParselet> infixParselets = new Dictionary<TokenType, InfixParselet>();
        public Parser(IEnumerable<Token> tokens)
        {
            _lexer = new Lexer(tokens);
        }
        public void Register(TokenType token, PrefixParselet parselet)
        {
            prefixParselets.Add(token, parselet);
        }
        public void Register(TokenType token, InfixParselet parselet)
        {
            infixParselets.Add(token, parselet);
        }
        public Token consume(TokenType expected)
        {
            Token token = LookAhead();
            if (token.TokenType != expected)
            {
                throw new ParseException("Expected token " + expected +
                    " and found " + token.TokenType);
            }

            return Consume();
        }
        public Token Consume()
        {
            return _lexer.Consume();
        }
        public Token LookAhead()
        {
            return _lexer.LookAhead();
        }
        private int getPrecedence()
        {
            var tokenType = LookAhead()?.TokenType;
            return tokenType == null ? 0 :
                (infixParselets.TryGetValue((TokenType)tokenType, out var parser))
                ? parser.Precedence : 0;
        }
        public ISimpleExpression Parse(int precedence = 0)
        {
            ISimpleExpression left = EmptyExpression.Default;

            var token = Consume();
            if (token != null)
            {
                prefixParselets.TryGetValue(token.TokenType, out var prefix);
                if (prefix == null)
                {
                    // if token null EOF
                    throw new ParseException($"Parselet for Token<{token.TokenType}>(\"{token.Value}\") NOT found.");
                }
                left = prefix.Parse(this, _lexer, token);
                while (precedence < getPrecedence())
                {
                    token = Consume();
                    if (token != null)
                    {
                        infixParselets.TryGetValue(token.TokenType, out var infix);
                        left = infix?.Parse(this, _lexer, token, left) ?? left;
                    }
                }
            }
            return left;
        }
    }
}