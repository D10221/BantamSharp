using System;
using System.Collections.Generic;
using System.Linq;

namespace uParserTests
{
    public static class Bantam
    {
        static Lazy<Registry> Registry = new Lazy<Registry>(() =>
        {
            var registry = new Registry();

            // Register all of the parselets for the grammar.
            // Register the ones that need special parselets.
            registry.Register(TokenType.NAME, new NameParselet());
            registry.Register(TokenType.ASSIGN, new AssignParselet());
            registry.Register(TokenType.QUESTION, new ConditionalParselet());
            registry.Register(TokenType.PARENT_LEFT, new GroupParselet(TokenType.PARENT_RIGHT));
            registry.Register(TokenType.PARENT_LEFT, new CallParselet());

            // Register the simple operator parselets.
            registry.prefix(TokenType.PLUS, Precedence.PREFIX);
            registry.prefix(TokenType.MINUS, Precedence.PREFIX);
            registry.prefix(TokenType.TILDE, Precedence.PREFIX);
            registry.prefix(TokenType.BANG, Precedence.PREFIX);

            // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
            registry.postfix(TokenType.BANG, Precedence.POSTFIX);

            registry.infixLeft(TokenType.PLUS, Precedence.SUM);
            registry.infixLeft(TokenType.MINUS, Precedence.SUM);
            registry.infixLeft(TokenType.ASTERISK, Precedence.PRODUCT);
            registry.infixLeft(TokenType.SLASH, Precedence.PRODUCT);
            registry.infixRight(TokenType.CARET, Precedence.EXPONENT);
            return registry;
        });
        static Tokenizer Tokenizer = new Tokenizer(Punctuators.Reverse);
        static Func<string, Parser> Parser = ((text) =>
        {
            var tokens = Tokenizer.Tokenize(text).ToList();
            var parser = new Parser(tokens, Registry.Value);
            return parser;
        });
        public static ISimpleExpression Parse(string text)
        {
            return Parser(text).Parse();
        }
    }
}