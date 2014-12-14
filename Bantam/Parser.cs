using System.Collections.Generic;
using System.Linq;
using SimpleParser;
using PrefixParseletResult = System.Tuple<SimpleParser.IPrefixParselet, bool>;
using InfixParseletResult = System.Tuple<SimpleParser.InfixParselet, bool>;
namespace Bantam
{
    /// <summary>
    /// Bantam implementation of SimpleParser
    /// </summary>
    public class Parser : IParser
    {
        #region Dependencies

        private readonly ILexer _lexer;
        private readonly List<IToken> _tokens = new List<IToken>();

        #endregion


        public Parser(ILexer lexer,IParserMap parserMap)
        {
            _parserMap = parserMap;
            _lexer = lexer;
        }

        private PrefixParseletResult GetPrefixParselet(TokenType tokenType)
        {
            return _parserMap.GetPrefixParselet(tokenType);
        }

        private InfixParseletResult GetInfixParselet(IToken atoken)
        {
            return _parserMap.GetInfixParselet(atoken.TokenType);
        }

        #region IParser

        public ISimpleExpression ParseExpression()
        {

            var token = Consume();

            var tokenType = token.TokenType;


            var prefix =
                GetPrefixParselet(tokenType)
                    .OnError(() => { throw new ParseException(token); })
                    .OkResult();

            var left = prefix.Parse(this, token); //Expression

            while (0 < GetPrecedence())
            {
                var atoken = Consume();
                GetInfixParselet(atoken)
                    .OnSuccess(parselet =>
                    {
                        left = parselet.Parse(this, left, atoken);
                    });
            }

            return left;
        }


        public bool IsMatch(TokenType expected)
        {
            var token = lookAhead();
            if (token.TokenType != expected)
            {
                return false;
            }

            Consume();
            return true;
        }

        public IToken Consume(TokenType expected)
        {
            var token = lookAhead();
            if (token.TokenType != expected)
            {
                throw new ParseException("Expected token {0} and found {1}", expected, token.TokenType);
            }
            return Consume();
        }

        public IToken Consume()
        {
            // Make sure we've read the token.
            lookAhead();
            var token = _tokens.First();
            _tokens.RemoveAt(0);
            return token;
        }

        private IToken lookAhead()
        {

            while (!_tokens.Any())
            {
                var token = _lexer.Next();
                _tokens.Add(token);
            }
            return _tokens.First();
        }

        #endregion

        private int GetPrecedence() {
           
            var token = lookAhead();
            var tokenType = token.TokenType;            
            return tokenType == TokenType.NONE ? 0 : GetPrecedence(tokenType);
        }

        private int GetPrecedence(TokenType tokenType)
        {
            var precedence = 0;
            var result = _parserMap.GetInfixParselet(tokenType);
            if (result.Ok())
                precedence = result.Parselet().Precedence;
            return precedence;

           /* _parserMap.GetInfixParselet(tokenType)
                .OnSuccess(x => precedence = x.Precedence);*/
        }

        private readonly IParserMap _parserMap;
    }
}