using System;
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

        private IParselet<TTokenType> GetParselet(TTokenType tokenType, ParseletType parseletType)
        {
            var result = _parselets.Where(x => x.ParseletType == parseletType && x.TokenType.Equals(tokenType)).ToArray();
            var sorted = !result.Any() ? null : result.OrderBy(x => x.Precedence).ToArray();
            // Warning ! more than 1 ? 
            return sorted?.FirstOrDefault();
        }
        private int GetPrecedence(TTokenType tokenType)
        {
            return GetParselet(tokenType, ParseletType.Infix)?.Precedence ?? 0;
        }
        private int NextPrecedence()
        {
            var token = LookAhead();
            return GetPrecedence(token.TokenType);
        }

        private IToken<TTokenType> LookAhead()
        {
            return _lexer.LookAhead() ?? Token.Empty<TTokenType>(default, EOF);
        }

        #region IParser
        private IToken<TTokenType> Consume()
        {
            return _lexer.Consume() ?? Token.Empty<TTokenType>(default, EOF);
        }

        public ISimpleExpression<TTokenType> Parse(int precedence = 0)
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
            while (precedence < NextPrecedence()) //Get Next Precedence
            {
                var token = Consume();
                if (!token.IsEmpty)
                {
                    var parselet = (token.Value == EOF ? EofParselet : null)
                       // ?? GetParselet(token.TokenType, ParseletType.Prefix)
                        ?? GetParselet(token.TokenType, ParseletType.Infix);

                    if (parselet != null)
                    {
                        left = parselet.Parse(this, _lexer, token, left);
                    }
                }
            }
            return left ?? new EmptyExpression<TTokenType>();
        }

        #endregion
    }
}