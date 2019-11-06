﻿
namespace SimpleParser
{
    public interface IParselet<TTokenType>
    {
        int Precedence { get; }
        ParseletType ParseletType { get; }
        TTokenType TokenType { get; }
        ISimpleExpression<TTokenType> Parse(
            IParser<TTokenType> parser, 
            ILexer<IToken<TTokenType>> lexer,
            IToken<TTokenType> token, ISimpleExpression<TTokenType> left);
    }
}