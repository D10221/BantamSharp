using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    public static class Parser
    {
        public static Dictionary<TokenType, char> TokenConfig = new Dictionary<TokenType, char>
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

        public static Dictionary<TokenType, IParselet<TokenType, char>> PrefixParselets = new Dictionary<TokenType, IParselet<TokenType, char>>{
                { TokenType.NAME, new NameParselet()},
                { TokenType.LEFT_PAREN, new GroupParselet() },
                { TokenType.PLUS, new PrefixOperatorParselet((int)Precedence.PREFIX, TokenConfig) },
                { TokenType.MINUS, new PrefixOperatorParselet((int)Precedence.PREFIX, TokenConfig) },
                { TokenType.TILDE, new PrefixOperatorParselet((int)Precedence.PREFIX, TokenConfig) },
                { TokenType.BANG, new PrefixOperatorParselet((int)Precedence.PREFIX, TokenConfig) }
                };

        public static Dictionary<TokenType, InfixParselet<TokenType, char>> InfixParselets = new Dictionary<TokenType, InfixParselet<TokenType, char>>(){
                { TokenType.BANG, new PostfixOperatorParselet((int)Precedence.POSTFIX, TokenConfig)},
                { TokenType.ASSIGN, new AssignParselet()},
                {  TokenType.QUESTION, new ConditionalParselet() },
                { TokenType.LEFT_PAREN, new CallParselet()} ,
                {  TokenType.PLUS, new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left, TokenConfig) },
                { TokenType.MINUS, new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left, TokenConfig)},
                {  TokenType.ASTERISK, new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left, TokenConfig) },
                { TokenType.SLASH, new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left, TokenConfig)},
                { TokenType.CARET, new BinaryOperatorParselet((int)Precedence.EXPONENT, InfixType.Right, TokenConfig)}
                };

        public static string Parse(string text)
        {
            var lexer = new Lexer(text, TokenConfig);
            var parser = new Parser<TokenType, char>(lexer, PrefixParselets, InfixParselets);
            var result = parser.ParseExpression();
            var builder = new Builder();
            result.Print(builder);
            var actual = builder.Build();
            return actual;
        }
    }
}