using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    public static class Parser
    {
        public static Dictionary<TokenType, char> punctuators = new Dictionary<TokenType, char>
            {
                { TokenType.LEFT_PAREN, '('},
                { TokenType.RIGHT_PAREN, ')'},
                { TokenType.COMMA, ','},
                { TokenType.ASSIGN, '='},
                { TokenType.PLUS, '+'},
                { TokenType.MINUS, '-'},
                { TokenType.ASTERISK, '*'},
                { TokenType.SLASH, '/'},
                { TokenType.CARET, '^'},
                { TokenType.TILDE, '~'},
                { TokenType.BANG, '!'},
                { TokenType.QUESTION, '?'},
                { TokenType.COLON, ':'},
                //{ TokenType.NAME, default(char)},
                //{ TokenType.EOF, default(char)}
            };

        public static IList<IParselet<TokenType>> Parselets = new List<IParselet<TokenType>>{
                new NameParselet(){ TokenType = TokenType.NAME},
                new GroupParselet(TokenType.RIGHT_PAREN) { TokenType = TokenType.LEFT_PAREN},
                new PrefixOperatorParselet((int)Precedence.PREFIX) { TokenType = TokenType.PLUS},
                new PrefixOperatorParselet((int)Precedence.PREFIX) { TokenType = TokenType.MINUS},
                new PrefixOperatorParselet((int)Precedence.PREFIX) { TokenType = TokenType.TILDE},
                new PrefixOperatorParselet((int)Precedence.PREFIX) { TokenType = TokenType.BANG},
                new PostfixOperatorParselet((int)Precedence.POSTFIX) { TokenType = TokenType.BANG},
                  new AssignParselet() { TokenType = TokenType.ASSIGN },
                new ConditionalParselet() { TokenType = TokenType.QUESTION},
                new FunctionCallParselet(){ TokenType = TokenType.LEFT_PAREN} ,
                 new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left) { TokenType = TokenType.PLUS},
                 new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left) { TokenType = TokenType.MINUS },
                new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left){ TokenType = TokenType.ASTERISK},
                new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left) { TokenType = TokenType.SLASH },
                new BinaryOperatorParselet((int)Precedence.EXPONENT, InfixType.Right) { TokenType = TokenType.CARET}
                };


        public static string Parse(string text)
        {
            var tokens = new Tokenizer(punctuators).Tokenize(text);
            var lexer = new Lexer<TokenType>(tokens);
            var parser = new SimpleParser.Parser<TokenType>(lexer, Parselets);
            var expression = parser.ParseExpression();
            var builder = new Builder();
            expression.Print(builder);
            var actual = builder.ToString();
            return actual;
        }
    }
}