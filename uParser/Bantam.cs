using System;
using System.Collections.Generic;
using System.Linq;


namespace uParser
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
            prefixes.Add(TokenType.NAME, NameParselet());
            infixes.Add(TokenType.ASSIGN, AssignParselet());
            infixes.Add(TokenType.QUESTION, ConditionalParselet());
            prefixes.Add(TokenType.PARENT_LEFT, GroupParselet(TokenType.PARENT_RIGHT));
            infixes.Add(TokenType.PARENT_LEFT, CallParselet());

            // Register the simple operator parselets.
            prefixes.AddPrefix(TokenType.PLUS, Precedence.PREFIX);
            prefixes.AddPrefix(TokenType.MINUS, Precedence.PREFIX);
            prefixes.AddPrefix(TokenType.TILDE, Precedence.PREFIX);
            prefixes.AddPrefix(TokenType.BANG, Precedence.PREFIX);

            // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
            infixes.AddPostfix(TokenType.BANG, Precedence.POSTFIX);

            infixes.AddInfixLeft(TokenType.PLUS, Precedence.SUM);
            infixes.AddInfixLeft(TokenType.MINUS, Precedence.SUM);
            infixes.AddInfixLeft(TokenType.ASTERISK, Precedence.PRODUCT);
            infixes.AddInfixLeft(TokenType.SLASH, Precedence.PRODUCT);
            infixes.Add(TokenType.CARET, Precedence.EXPONENT);
            return (prefixes, infixes);
        });
        static Tokenizer Tokenizer = new Tokenizer(Punctuators.Reverse);
        public static ISimpleExpression Parse(string text)
        {
            var tokens = Tokenizer.Tokenize(text).ToList();
            var (prefixes, infixes) = Parselets.Value;
            return Parser.ParseFty(tokens, prefixes, infixes).Invoke();
        }
    }
}