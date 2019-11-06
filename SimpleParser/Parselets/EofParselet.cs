namespace SimpleParser
{
    internal class EofParselet<TTokenType> : IParselet<TTokenType>
    {
        public int Precedence => int.MaxValue;

        public ParseletType ParseletType => ParseletType.None;

        public TTokenType TokenType {get;private set; }

        public ISimpleExpression<TTokenType> Parse(IParser<TTokenType> parser, ILexer<IToken<TTokenType>> lexer, IToken<TTokenType> token, ISimpleExpression<TTokenType> left)
        {
            TokenType = token.TokenType;
            return new EofExpression<TTokenType>(token);
        }
    }
}