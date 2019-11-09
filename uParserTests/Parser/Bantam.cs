using System;
using System.Collections.Generic;
using System.Linq;


namespace uParserTests
{
    using ParseLetRegistry = ValueTuple<IDictionary<TokenType, PrefixParselet>, IDictionary<TokenType, (int, InfixParselet)>>;
    using static Parselets;    

    public static class Bantam
    {
        static Lazy<ParseLetRegistry> Parselets = new Lazy<ParseLetRegistry>(() =>
        {
            IDictionary<TokenType, PrefixParselet> prefixes = new Dictionary<TokenType, PrefixParselet>();
            IDictionary<TokenType, (int, InfixParselet)> infixes = new Dictionary<TokenType, (int, InfixParselet)>();
            // Register all of the parselets for the grammar.
            // Register the ones that need special parselets.
            prefixes.Register(TokenType.NAME, NameParselet());
            infixes.Register(TokenType.ASSIGN, AssignParselet());
            infixes.Register(TokenType.QUESTION, ConditionalParselet());
            prefixes.Register(TokenType.PARENT_LEFT, GroupParselet(TokenType.PARENT_RIGHT));
            infixes.Register(TokenType.PARENT_LEFT, CallParselet());

            // Register the simple operator parselets.
            prefixes.Prefix(TokenType.PLUS, Precedence.PREFIX);
            prefixes.Prefix(TokenType.MINUS, Precedence.PREFIX);
            prefixes.Prefix(TokenType.TILDE, Precedence.PREFIX);
            prefixes.Prefix(TokenType.BANG, Precedence.PREFIX);

            // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
            infixes.Postfix(TokenType.BANG, Precedence.POSTFIX);

            infixes.InfixLeft(TokenType.PLUS, Precedence.SUM);
            infixes.InfixLeft(TokenType.MINUS, Precedence.SUM);
            infixes.InfixLeft(TokenType.ASTERISK, Precedence.PRODUCT);
            infixes.InfixLeft(TokenType.SLASH, Precedence.PRODUCT);
            infixes.InfixRight(TokenType.CARET, Precedence.EXPONENT);
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