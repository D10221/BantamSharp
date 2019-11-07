using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class Parser<TTokenType> : IParser<TTokenType>
    {
        private readonly ILexer<IToken<TTokenType>> _lexer;
        private readonly IEnumerable<IParselet<TTokenType>> _parselets;

        private static readonly object EOF = new { EOF = true };
        IParselet<TTokenType> EofParselet = new EofParselet<TTokenType>();

        public Parser(
            ILexer<IToken<TTokenType>> lexer,
            IEnumerable<IParselet<TTokenType>> parselets)
        {
            _lexer = lexer;
            _parselets = parselets;
        }
        ///<sumary>
        /// returns the  parselet of specified parseletype for the provided token type
        ///</sumary>
        private IParselet<TTokenType> GetParselet(TTokenType tokenType, ParseletType parseletType)
        {
            var result = _parselets.Where(x => x.ParseletType == parseletType && x.TokenType.Equals(tokenType)).ToArray();
            var sorted = !result.Any() ? null : result.OrderBy(x => x.Precedence).ToArray();
            if (sorted != null && sorted.Count() > 1)
            {
                throw new ParseException($"Too many {parseletType} parslets for '{tokenType}' tokentype");
            }
            return sorted?.FirstOrDefault();
        }
        private int GetInFixPrecedence(TTokenType tokenType)
        {
            return GetParselet(tokenType, ParseletType.Infix)?.Precedence ?? 0;
        }

        private IToken<TTokenType> LookAhead()
        {
            return _lexer.LookAhead() ?? Token.Empty<TTokenType>(default, EOF);
        }
        private IToken<TTokenType> Consume()
        {
            return _lexer.Consume() ?? Token.Empty<TTokenType>(default, EOF);
        }
        #region IParser
        public ISimpleExpression<TTokenType> Parse(int precedence = 0)
        {
            try
            {
                ISimpleExpression<TTokenType> left = null;
                // ...
                {
                    var token = Consume();

                    var parselet = (token.Value == EOF ? EofParselet : null)
                            ?? GetParselet(token.TokenType, ParseletType.Prefix)
                            ?? GetParselet(token.TokenType, ParseletType.Infix);
                    if (parselet != null)
                    {
                        left = parselet.Parse(this, _lexer, token, null);
                    }
                }
                while (precedence < GetInFixPrecedence(LookAhead().TokenType))
                {
                    var token = Consume();
                    var infix = (token.Value == EOF ? EofParselet : null)
                        // ?? GetParselet(token.TokenType, ParseletType.Prefix)
                        ?? GetParselet(token.TokenType, ParseletType.Infix);                             
                    if (infix != null)
                    {
                        left = infix.Parse(this, _lexer, token, left);
                    }
                }
                return left ?? new EmptyExpression<TTokenType>();
            }
            catch (ParseException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new ParseException("Unexpected error parsing", ex);
            }
        }

        #endregion
    }
}