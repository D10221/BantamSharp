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
}