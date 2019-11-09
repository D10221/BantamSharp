using System;
using System.Collections.Generic;

namespace uParserTests
{
    using Parse = Func<int, ISimpleExpression>;
    public static class Parser
    {        
        public static Parse Parse(
            IList<Token> lexer,
            IDictionary<TokenType, PrefixParselet> prefixes,
            IDictionary<TokenType, InfixParselet> infixes)
        {
            Func<int> getPrecedence = () =>
            {
                if (!lexer.TryPeek(out var token))
                {
                    return 0;
                }
                if (!infixes.TryGetValue(token.TokenType, out var parser))
                {
                    return 0;
                }
                return parser.Precedence;
            };

            Parse parse = null;
            parse = (precedence) =>
            {
                ISimpleExpression left = EmptyExpression.Default;

                var token = lexer.Consume();
                if (token != default)
                {
                    if (!prefixes.TryGetValue(token.TokenType, out var prefix))
                    {
                        throw new ParseException($"Parselet<Prefix<Token<{token.TokenType},<\"{token.Value}\">>>> NOT found.");
                    }
                    left = prefix.Parse(parse, lexer, token);

                    while (precedence < getPrecedence())
                    {
                        token = lexer.Consume();
                        if (token != default)
                        {
                            if (!infixes.TryGetValue(token.TokenType, out var infix))
                            {
                                throw new ParseException($"Parselet<Infix<Token<{token.TokenType},<\"{token.Value}\">>>> NOT found.");
                            }
                            left = infix.Parse(parse, lexer, token, left) ?? left;
                        }
                    }
                }
                return left;
            };
            return parse;
        }

    }
}