using System;
using System.Collections.Generic;

namespace uParserTests
{
    public class Registry
    {

        private IDictionary<TokenType, PrefixParselet> prefixParselets = new Dictionary<TokenType, PrefixParselet>();
        private IDictionary<TokenType, InfixParselet> infixParselets = new Dictionary<TokenType, InfixParselet>();
        public void Register(TokenType token, PrefixParselet parselet)
        {
            prefixParselets.Add(token, parselet);
        }
        public void Register(TokenType token, InfixParselet parselet)
        {
            infixParselets.Add(token, parselet);
        }
        public void postfix(TokenType token, Precedence precedence)
        {
            Register(token, new PostfixOperatorParselet((int)precedence));
        }
        public void prefix(TokenType token, Precedence precedence)
        {
            Register(token, new PrefixOperatorParselet((int)precedence));
        }

        public void infixLeft(TokenType token, Precedence precedence)
        {
            Register(token, new BinaryOperatorParselet((int)precedence, false));
        }

        public void infixRight(TokenType token, Precedence precedence)
        {
            Register(token, new BinaryOperatorParselet((int)precedence, true));
        }

        public bool TryGetInfix(TokenType tokenType, out InfixParselet Parselet)
        {
            return infixParselets.TryGetValue(tokenType, out Parselet);
        }

        public bool TryGetPrefix(TokenType tokenType, out PrefixParselet prefix)
        {
            return prefixParselets.TryGetValue(tokenType, out prefix);
        }
    }
    public class Parser
    {
        private readonly Lexer _lexer;
        private readonly Registry _registry;
        public Parser(Lexer lexer, Registry registry)
        {
            _registry = registry;
            _lexer = lexer;
        }

        private int getPrecedence()
        {
            if (!_lexer.Lookup(0, out var token))
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