using System;
using System.Collections.Generic;
using Bantam.Paselets;
using SimpleParser;

namespace Bantam
{    
    public class ParserMap : IParserMap
    {
        private readonly IDictionary<TokenType,IPrefixParselet> _prefixParselets =  new Dictionary<TokenType, IPrefixParselet>();
        private readonly IDictionary<TokenType, InfixParselet> _infixParselets = new Dictionary<TokenType, InfixParselet>();

        public ParserMap()
        {
            // Register all of the parselets for the grammar.    
            // Register the ones that need special parselets.
            Register(TokenType.NAME, new NameParselet());
            Register(TokenType.ASSIGN, new AssignParselet());
            Register(TokenType.QUESTION, new ConditionalParselet());
            Register(TokenType.LEFT_PAREN, new GroupParselet());
            Register(TokenType.LEFT_PAREN, new CallParselet());

            // Register the simple operator parselets.
            prefix(TokenType.PLUS, Precedence.PREFIX);
            prefix(TokenType.MINUS, Precedence.PREFIX);
            prefix(TokenType.TILDE, Precedence.PREFIX);
            prefix(TokenType.BANG, Precedence.PREFIX);

            // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
            postfix(TokenType.BANG, Precedence.POSTFIX);

            infixLeft(TokenType.PLUS, Precedence.SUM);
            infixLeft(TokenType.MINUS, Precedence.SUM);
            infixLeft(TokenType.ASTERISK, Precedence.PRODUCT);
            infixLeft(TokenType.SLASH, Precedence.PRODUCT);
            infixRight(TokenType.CARET, Precedence.EXPONENT);
        }

        public void Register(TokenType tokenType, IPrefixParselet parselet)
        {
            _prefixParselets.Add(tokenType, parselet);
        }

        public void Register(TokenType tokenType, InfixParselet parselet)
        {
            _infixParselets.Add(tokenType, parselet);
        }

        /// <summary>
        ///  Registers a postfix unary operator parselet for the given token and
        /// precedence.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="precedence"></param>
        public void postfix(TokenType token, Precedence precedence)
        {
            Register(token, new PostfixOperatorParselet(precedence));
        }

        /// <summary>
        /// Registers a prefix unary operator parselet for the given token and
        /// precedence.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="precedence"></param>
        public void prefix(TokenType token, Precedence precedence)
        {
            Register(token, new PrefixOperatorParselet(precedence));
        }

        /// <summary>
        /// Registers a left-associative binary operator parselet for the given token
        /// and precedence.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="precedence"></param>
        public void infixLeft(TokenType token, Precedence precedence)
        {
            Register(token, new BinaryOperatorParselet(precedence, false));
        }

        /// <summary>
        /// Registers a right-associative binary operator parselet for the given token and precedence.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="precedence"></param>
        public void infixRight(TokenType token, Precedence precedence)
        {
            Register(token, new BinaryOperatorParselet(precedence, true));
        }

        public Tuple<InfixParselet, bool> GetInfixParselet(TokenType tokenType)
        {
            InfixParselet parselet;
            var r = _infixParselets.TryGetValue(tokenType, out parselet);
            return new Tuple<InfixParselet, bool>(parselet, r);

        }

        public Tuple<IPrefixParselet, bool> GetPrefixParselet(TokenType tokenType)
        {
            IPrefixParselet parselet;
            var r = _prefixParselets.TryGetValue(tokenType, out parselet);
            return new Tuple<IPrefixParselet, bool>(parselet, r);

        }
    }

    
}