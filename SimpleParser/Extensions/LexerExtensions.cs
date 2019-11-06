namespace SimpleParser
{
    public static class LexerExtensions {        
        public static (IToken<TTokenType> current, IToken<TTokenType> next,bool isMatch) IsMatch<TTokenType>(this ILexer<IToken<TTokenType>> lexer, TTokenType expected)
        {
            var next = lexer.LookAhead();
            var isMatch = Equals(next.TokenType, expected);
            var current = lexer.Consume();
            return (current, next, isMatch);
        }        
        public static (IToken<TTokenType> current, IToken<TTokenType> next) ConsumeNext<TTokenType>(this ILexer<IToken<TTokenType>> lexer)
        {
            var next = lexer.LookAhead();            
            return (lexer.Consume(), next);
        }        
    }
}