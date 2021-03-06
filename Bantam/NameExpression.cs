﻿using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression
    {
        public object Token { get; }

        public NameExpression(IToken<TokenType> token)
        {
            Token = token;
        }

        public NameExpression(string token)
        {
            this.Token = SimpleParser.Token.From(TokenType.NAME, token);
        }

        public void Print(IBuilder builder)
        {
            builder.Append(Token);
        }

        public override string ToString()
        {
            return $"NameExpression:\"{Token}\"";
        }
    }
}