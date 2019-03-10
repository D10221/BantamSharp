using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    public static class Parser
    {
        public static Dictionary<TokenType, string> Punctuators = new Dictionary<TokenType, string>
            {
                { TokenType.AND , "&&"},
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
                //{ TokenType.NAME, default(char)},
                //{ TokenType.EOF, default(char)}
            };
        public static IList<IParselet<TokenType>> Parselets = new List<IParselet<TokenType>>{
                new NameParselet(TokenType.NAME),
                new GroupParselet(TokenType.PAREN_LEFT, TokenType.PARENT_RIGHT),
                new PrefixOperatorParselet(TokenType.PLUS, (int)Precedence.PREFIX),
                new PrefixOperatorParselet(TokenType.MINUS, (int)Precedence.PREFIX),
                new PrefixOperatorParselet(TokenType.TILDE, (int)Precedence.PREFIX),
                new PrefixOperatorParselet(TokenType.BANG, (int)Precedence.PREFIX),
                new PostfixOperatorParselet(TokenType.BANG, (int)Precedence.POSTFIX),
                new AssignParselet(TokenType.ASSIGN, (int)Bantam.Precedence.ASSIGNMENT),
                new ConditionalParselet(TokenType.QUESTION, (int)Bantam.Precedence.CONDITIONAL) ,
                new FunctionCallParselet(TokenType.PAREN_LEFT, (int)Bantam.Precedence.CALL) ,
                new BinaryOperatorParselet(TokenType.PLUS, (int)Precedence.SUM, InfixType.Left),
                new BinaryOperatorParselet(TokenType.MINUS, (int)Precedence.SUM, InfixType.Left),
                new BinaryOperatorParselet(TokenType.ASTERISK, (int)Precedence.PRODUCT, InfixType.Left),
                new BinaryOperatorParselet(TokenType.SLASH, (int)Precedence.PRODUCT, InfixType.Left),
                new BinaryOperatorParselet(TokenType.CARET, (int)Precedence.EXPONENT, InfixType.Right)
                };
        public static ISimpleExpression Parse(string text)
        {
            var tokenizer = Tokenizer.From(Punctuators);
            var tokenFactory = TokenFactory.From(Punctuators.Reverse());
            var tokens = tokenizer.Tokenize(text, tokenFactory);
            var lexer = new Lexer<TokenType>(tokens);
            var parser = new SimpleParser.Parser<TokenType>(lexer, Parselets);
            return parser.ParseExpression();
        }

    }
    public class Printer
    {
        public static Printer Default = new Printer();
        public string Print(ISimpleExpression expression)
        {
            var builder = new Builder();
            expression.Print(builder);
            return builder.ToString();
        }
    }
}