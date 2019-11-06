
using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    ///     Parselet to parse a function call like "a(b, c, d)".
    /// </summary>
    public class FunctionCallParselet : IParselet<TokenType>
    {
        public FunctionCallParselet(TokenType tokenType, int precedence)
        {
            TokenType = tokenType;
            Precedence = precedence;
        }

        public TokenType TokenType { get; }
        public int Precedence { get; }
        public ParseletType ParseletType { get; } = ParseletType.Infix;

        public ISimpleExpression<TokenType> Parse(
            IParser<TokenType> parser, 
            ILexer<IToken<TokenType>> lexer,
            IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            // Parse the comma-separated arguments until we hit, ")".
            var args = new List<ISimpleExpression<TokenType>>();

            // There may be no arguments at all.
            var next = lexer.LookAhead();
            if (next.TokenType == TokenType.PARENT_RIGHT)
            {
                lexer.Consume();
                return new FunctionCallExpression(left, args);
            }
            do
            {
                var e = parser.Parse();
                args.Add(e);                
                next = lexer.LookAhead();
                if(next.TokenType == TokenType.COMMA){
                    lexer.Consume();
                }    
            } while (next.TokenType == TokenType.COMMA);

            if (next.TokenType != TokenType.PARENT_RIGHT)
            {
                throw new ParseException($"Expected {TokenType.PARENT_RIGHT}");
            }            
            lexer.Consume();
            return new FunctionCallExpression(left, args);
        }
    }
}