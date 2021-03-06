﻿
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
    /// </summary>
    public class ConditionalParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get; set; }
        public ParseletType ParseletType { get; } = ParseletType.Infix;
        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            var thenArm = parser.ParseExpression(/*Precedence.ZERO*/);
            parser.Consume(TokenType.COLON);
            //WHy  precedence -1 
            var elseArm = parser.ParseExpression(Precedence - 1);

            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence { get; } = (int)Bantam.Precedence.CONDITIONAL;
    }
}
