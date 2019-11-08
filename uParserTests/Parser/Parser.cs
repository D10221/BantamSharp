using System;

namespace uParserTests
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private readonly Registry _registry;
        public Parser(Lexer lexer, Registry registry)
        {
            _registry = registry;
            _lexer = lexer;
        }
        ///<summary>
        /// Gets next Token precedence
        ///</summary>
        private int getPrecedence()
        {
            if (!_lexer.TryPeek(out var token))
            {
                return 0;
            }
            if (!_registry.TryGetInfix(token.TokenType, out var parser))
            {
                return 0;
            }
            return parser.Precedence;
        }
        public ISimpleExpression Parse(int precedence = 0)
        {
            ISimpleExpression left = EmptyExpression.Default;

            var token = _lexer.Consume();
            if (token != default)
            {
                if (!_registry.TryGetPrefix(token.TokenType, out var prefix))
                {
                    throw new ParseException($"Parselet<Prefix<Token<{token.TokenType},<\"{token.Value}\">>>> NOT found.");
                }
                left = prefix?.Parse(this, _lexer, token);
                while (precedence < getPrecedence())
                {
                    token = _lexer.Consume();
                    if (token != default)
                    {
                        if (!_registry.TryGetInfix(token.TokenType, out var infix))
                        {
                            throw new ParseException($"Parselet<Infix<Token<{token.TokenType},<\"{token.Value}\">>>> NOT found.");
                        }
                        left = infix.Parse(this, _lexer, token, left) ?? left;
                    }
                }
            }
            return left;
        }
    }
}