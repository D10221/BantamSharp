﻿using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression<TokenType>
    {
        public static NameExpression From(string token)
        {
            return new NameExpression(
                SimpleParser.Token.From(TokenType.NAME, token)
            );
        }
        public IToken<TokenType> Token { get; }

        public NameExpression(IToken<TokenType> token)
        {
            Token = token;
        }


        public void Visit(IExpressionVisitor<TokenType> visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{nameof(NameExpression)}:\"{Token}\"";
        }
    }
}