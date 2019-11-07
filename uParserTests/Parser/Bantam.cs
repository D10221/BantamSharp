using System;
using System.Collections.Generic;

namespace uParserTests
{
    public static class Bantam
    {
        public static Func<string, ISimpleExpression> BantamParser()
        {
            Action<Parser> register = (parser) =>
            {
                void postfix(TokenType token, Precedence precedence)
                {
                    parser.Register(token, new PostfixOperatorParselet((int)precedence));
                }


                void prefix(TokenType token, Precedence precedence)
                {
                    parser.Register(token, new PrefixOperatorParselet((int)precedence));
                }


                void infixLeft(TokenType token, Precedence precedence)
                {
                    parser.Register(token, new BinaryOperatorParselet((int)precedence, false));
                }

                void infixRight(TokenType token, Precedence precedence)
                {
                    parser.Register(token, new BinaryOperatorParselet((int)precedence, true));
                }
                // Register all of the parselets for the grammar.
                // Register the ones that need special parselets.
                parser.Register(TokenType.NAME, new NameParselet());
                parser.Register(TokenType.ASSIGN, new AssignParselet());
                parser.Register(TokenType.QUESTION, new ConditionalParselet());
                parser.Register(TokenType.PARENT_LEFT, new GroupParselet(TokenType.PARENT_RIGHT));
                parser.Register(TokenType.PARENT_LEFT, new CallParselet());

                // Register the simple operator parselets.
                prefix(TokenType.PLUS, Precedence.PREFIX);
                prefix(TokenType.MINUS, Precedence.PREFIX);
                prefix(TokenType.TILDE, Precedence.PREFIX);
                prefix(TokenType.BANG, Precedence.PREFIX);

                // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
                postfix(TokenType.BANG, Precedence.POSTFIX);

                infixLeft(TokenType.PLUS, Precedence.SUM);
                infixLeft(TokenType.MINUS, Precedence.SUM);
                infixLeft(TokenType.ASTERISK, Precedence.PRODUCT);
                infixLeft(TokenType.SLASH, Precedence.PRODUCT);
                infixRight(TokenType.CARET, Precedence.EXPONENT);
            };


            return (text) =>
            {
                var tokenizer = new Tokenizer(Punctuators.Reverse);                
                var tokens = tokenizer.Tokenize(text);
                var lexer = new Lexer(tokens);
                var parser = new Parser(tokens);
                register(parser);
                return parser.Parse();
            };
        }


    }
}