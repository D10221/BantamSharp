using System.Collections.Generic;

namespace uParserTests
{
    public static class ParsetRegistryExtensions
    {       
        public static void Register(this IDictionary<TokenType, PrefixParselet> prefixParselets,
            TokenType token, PrefixParselet parselet)
        {
            prefixParselets.Add(token, parselet);
        }
        public static void Register(this IDictionary<TokenType, InfixParselet> infixParselets,
            TokenType token, InfixParselet parselet)
        {
            infixParselets.Add(token, parselet);
        }
        public static void postfix(
            this IDictionary<TokenType, InfixParselet> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Register(token, new PostfixOperatorParselet((int)precedence));
        }
        public static void prefix(this IDictionary<TokenType, PrefixParselet> prefixParselets,
            TokenType token, Precedence precedence)
        {
            prefixParselets.Register(token, new PrefixOperatorParselet((int)precedence));
        }

        public static void infixLeft(this IDictionary<TokenType, InfixParselet> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Register(token, new BinaryOperatorParselet((int)precedence, false));
        }

        public static void infixRight(this IDictionary<TokenType, InfixParselet> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Register(token, new BinaryOperatorParselet((int)precedence, true));
        }
    }
}