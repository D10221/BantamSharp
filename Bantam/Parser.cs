using SimpleParser;
using System;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    /// Non Language specific implementation
    /// </summary>
    public static class ParserFactory
    {
        /// <summary>
        /// misc. operators
        /// </summary>
        public static Dictionary<TokenType, string> Punctuators = new Dictionary<TokenType, string>
            {
                { TokenType.AND , "&&"},
                { TokenType.AT , "@"},
                { TokenType.ASSIGN, "="},
                { TokenType.ASTERISK, "*"},
                { TokenType.BANG, "!"},
                { TokenType.CARET, "^"},
                { TokenType.COLON, ":"},
                { TokenType.COMMA, ","},
                { TokenType.EQUALS, "=="},
                { TokenType.MINUS, "-"},
                { TokenType.NOT_EQUAL, "!="},
                { TokenType.OR , "||"},
                { TokenType.PAREN_LEFT, "("},
                { TokenType.PARENT_RIGHT, ")"},
                { TokenType.PLUS, "+"},
                { TokenType.QUESTION, "?"},
                { TokenType.SLASH, "/"},
                { TokenType.TILDE, "~"},
                { TokenType.LIKE, "LIKE"},  
                // { TokenType.LITERAL, string.Empty },
                // { TokenType.DOUBLE_QUOTE, "\"" },
                //{ TokenType.SINGLE_QUOTE, "'" },
                //{ TokenType.BACKTICK, "`" },
                // { TokenType.NAME, string.Empty},
                // { TokenType.NUMBER, string.Empty},
                // { TokenType.EOF, string.Empty}
            };

        /// <summary>
        /// Parsers
        /// </summary>
        public static IList<IParselet<TokenType>> Parselets = new List<IParselet<TokenType>>{
                new NameParselet(TokenType.NAME),
                new LiteralParselet(TokenType.LITERAL),
                new GroupParselet(TokenType.PAREN_LEFT, TokenType.PARENT_RIGHT),
                new PrefixOperatorParselet(TokenType.AT, (int)Precedence.PREFIX),
                new PrefixOperatorParselet(TokenType.PLUS, (int)Precedence.PREFIX),
                new PrefixOperatorParselet(TokenType.MINUS, (int)Precedence.PREFIX),
                new PrefixOperatorParselet(TokenType.TILDE, (int)Precedence.PREFIX),
                new PrefixOperatorParselet(TokenType.BANG, (int)Precedence.PREFIX),
                new PostfixOperatorParselet(TokenType.BANG, (int)Precedence.POSTFIX),
                new AssignParselet(TokenType.ASSIGN, (int)Precedence.ASSIGNMENT),
                new ConditionalParselet(TokenType.QUESTION, (int)Precedence.CONDITIONAL) ,
                new FunctionCallParselet(TokenType.PAREN_LEFT, (int)Precedence.CALL) ,
                new BinaryOperatorParselet(TokenType.PLUS, (int)Precedence.SUM, InfixType.Left),
                new BinaryOperatorParselet(TokenType.MINUS, (int)Precedence.SUM, InfixType.Left),
                new BinaryOperatorParselet(TokenType.ASTERISK, (int)Precedence.PRODUCT, InfixType.Left),
                new BinaryOperatorParselet(TokenType.SLASH, (int)Precedence.PRODUCT, InfixType.Left),
                new BinaryOperatorParselet(TokenType.CARET, (int)Precedence.EXPONENT, InfixType.Right),
                new BinaryOperatorParselet(TokenType.EQUALS, (int)Precedence.EQUALS, InfixType.Left),
                new BinaryOperatorParselet(TokenType.NOT_EQUAL, (int)Precedence.NOT_EQUAL, InfixType.Left),
                new BinaryOperatorParselet(TokenType.AND, (int)Precedence.AND, InfixType.Left),
                new BinaryOperatorParselet(TokenType.OR, (int)Precedence.OR, InfixType.Left),
                new BinaryOperatorParselet(TokenType.LIKE, (int)Precedence.LIKE, InfixType.Left),
                };
        /// <summary>
        /// Returns Expression
        /// </summary>        
        public static Func<string, ISimpleExpression<TokenType>> Create()
        {
            var tokenizer = Tokenizer.From(Punctuators);
            var tokenFactory = TokenFactory.From(Punctuators.Reverse());
            /**
             *
             */
            return text =>
            {
                var tokens = tokenizer.Tokenize(text, tokenFactory);
                var lexer = new Lexer<TokenType>(tokens);
                var parser = new SimpleParser.Parser<TokenType>(lexer, Parselets);
                return parser.ParseExpression();
            };
        }

    }
}