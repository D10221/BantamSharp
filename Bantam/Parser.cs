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

        public static Dictionary<TokenType, IParselet<TokenType>> PrefixParselets = new Dictionary<TokenType, IParselet<TokenType>>{
                { TokenType.NAME, new NameParselet()},
                { TokenType.LEFT_PAREN, new GroupParselet(TokenType.RIGHT_PAREN) },
                { TokenType.PLUS, new PrefixOperatorParselet((int)Precedence.PREFIX) },
                { TokenType.MINUS, new PrefixOperatorParselet((int)Precedence.PREFIX) },
                { TokenType.TILDE, new PrefixOperatorParselet((int)Precedence.PREFIX) },
                { TokenType.BANG, new PrefixOperatorParselet((int)Precedence.PREFIX) }
                };

        public static Dictionary<TokenType, InfixParselet<TokenType>> InfixParselets = new Dictionary<TokenType, InfixParselet<TokenType>>(){
                { TokenType.BANG, new PostfixOperatorParselet((int)Precedence.POSTFIX)},
                { TokenType.ASSIGN, new AssignParselet()},
                { TokenType.QUESTION, new ConditionalParselet() },
                { TokenType.LEFT_PAREN, new CallParselet()} ,
                { TokenType.PLUS, new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left) },
                { TokenType.MINUS, new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left)},
                { TokenType.ASTERISK, new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left) },
                { TokenType.SLASH, new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left)},
                { TokenType.CARET, new BinaryOperatorParselet((int)Precedence.EXPONENT, InfixType.Right)}
                };

        public static string Parse(string text)
        {
            var lexer = new Lexer(text, punctuators);
            var parser = new SimpleParser.Parser<TokenType>(lexer, PrefixParselets, InfixParselets);
            var result = parser.ParseExpression();
            var builder = new Builder();
            result.Print(builder);
            var actual = builder.Build();
            return actual;
        }
    }
}