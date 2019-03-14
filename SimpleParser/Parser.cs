using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimpleParser
{
    public class Parser<TTokenType> : IParser<TTokenType>
    {
        private readonly ILexer<TTokenType> _lexer;
        private readonly IList<IToken<TTokenType>> _tokens = new List<IToken<TTokenType>>();
        private readonly IList<IToken<TTokenType>> _parsed = new List<IToken<TTokenType>>();
        private readonly IEnumerable<IParselet<TTokenType>> _parselets;

        public Parser(
            ILexer<TTokenType> lexer,
            IEnumerable<IParselet<TTokenType>> parselets)
        {
            _lexer = lexer;
            _parselets = parselets;
        }

        private IParselet<TTokenType> GetParselet(TTokenType tokenType, ParseletType parseletType)
        {
            return _parselets.FirstOrDefault(x => x.ParseletType == parseletType && x.TokenType.Equals(tokenType));
        }

        private IToken<TTokenType> LookAhead()
        {
            while (!_tokens.Any())
            {
                var token = _lexer.Next();
                _tokens.Add(token);
            }
            return _tokens.First();
        }
        private int GetPrecedence(TTokenType tokenType)
        {
            var result = GetParselet(tokenType, ParseletType.Infix);
            if (result != null)
                return result.Precedence;
            return 0;
        }
        private int GetPrecedence()
        {
            var token = LookAhead();
            return Equals(token.TokenType, default(TTokenType)) ? 0 : GetPrecedence(token.TokenType);
        }

        #region IParser

        public IEnumerable<IToken<TTokenType>> Tokens => _parsed;

        public ISimpleExpression<TTokenType> ParseExpression(int precedence = 0, object caller = null)
        {
            try
            {
                var token = Consume();

                var parselet =
                        GetParselet(token.TokenType, ParseletType.Prefix)
                        ?? GetParselet(token.TokenType, ParseletType.Infix);
                if (parselet == null && !token.IsEmpty && caller as IParselet<TTokenType> == null)
                {
                    throw new ParseException($"Missing parser for Token: '{token}'");
                }
                var left = parselet?.Parse(this, token, null);
                while (precedence < GetPrecedence()) //Get Next Precedence
                {
                    var atoken = Consume();
                    if (!atoken.IsEmpty)
                    {
                        var p = GetParselet(atoken.TokenType, ParseletType.Infix);
                        if (p != null)
                        {
                            left = p.Parse(this, atoken, left);
                        }
                    }
                }
                var diff = _lexer.Tokens.Count() - this._parsed.Count();
                // TODO: 
                if (caller as IParselet<TTokenType> == null && diff > 0)
                {
                    throw new ParseException($"Bad expresion:'{_lexer.ToString()}'");
                }
                return left ?? new EmptyExpression<TTokenType>();

            }
            catch (System.Exception ex)
            {
                Debug.Write(ex);
                throw;
            }
        }

        public bool IsMatch(TTokenType expected)
        {
            var token = LookAhead();
            if (!Equals(token.TokenType, expected))
            {
                return false;
            }

            Consume();
            return true;
        }

        public IToken<TTokenType> Consume(TTokenType expected)
        {
            var token = LookAhead();
            if (!Equals(token.TokenType, expected))
            {
                throw new ParseException( $"Expected token {expected} and found {token.TokenType}");
            }
            return Consume();
        }

        public IToken<TTokenType> Consume()
        {
            // Make sure we've read the token.
            LookAhead();
            var token = _tokens.First();
            _parsed.Add(token);
            _tokens.RemoveAt(0);
            return token;
        }
        #endregion
    }
}