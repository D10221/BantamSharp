using System;
using System.Collections.Generic;
using System.Linq;

namespace uParserTests
{
    using ParseLetRegistry = ValueTuple<IDictionary<TokenType, PrefixParselet>, IDictionary<TokenType, InfixParselet>>;
    public static class Bantam
    {
        static Lazy<ParseLetRegistry> Parselets = new Lazy<ParseLetRegistry>(() =>
        {
            IDictionary<TokenType, PrefixParselet> prefixes = new Dictionary<TokenType, PrefixParselet>();
            IDictionary<TokenType, InfixParselet> infixes = new Dictionary<TokenType, InfixParselet>();


            // Register all of the parselets for the grammar.
            // Register the ones that need special parselets.
            prefixes.Register(TokenType.NAME, new NameParselet());
            infixes.Register(TokenType.ASSIGN, new AssignParselet());
            infixes.Register(TokenType.QUESTION, new ConditionalParselet());
            prefixes.Register(TokenType.PARENT_LEFT, new GroupParselet(TokenType.PARENT_RIGHT));
            infixes.Register(TokenType.PARENT_LEFT, new CallParselet());

            // Register the simple operator parselets.
            prefixes.prefix(TokenType.PLUS, Precedence.PREFIX);
            prefixes.prefix(TokenType.MINUS, Precedence.PREFIX);
            prefixes.prefix(TokenType.TILDE, Precedence.PREFIX);
            prefixes.prefix(TokenType.BANG, Precedence.PREFIX);

            // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
            infixes.postfix(TokenType.BANG, Precedence.POSTFIX);

            infixes.infixLeft(TokenType.PLUS, Precedence.SUM);
            infixes.infixLeft(TokenType.MINUS, Precedence.SUM);
            infixes.infixLeft(TokenType.ASTERISK, Precedence.PRODUCT);
            infixes.infixLeft(TokenType.SLASH, Precedence.PRODUCT);
            infixes.infixRight(TokenType.CARET, Precedence.EXPONENT);
            return (prefixes, infixes);
        });
        static Tokenizer Tokenizer = new Tokenizer(Punctuators.Reverse);        
        public static ISimpleExpression Parse(string text)
        {
             var tokens = Tokenizer.Tokenize(text).ToList();
            var (prefixes, infixes) = Parselets.Value;            
            return Parser.Parse(tokens, prefixes, infixes)(0);
        }
    }
}