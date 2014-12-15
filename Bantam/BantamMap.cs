using System;
using System.Collections.Generic;
using Bantam.Paselets;
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleParser.TokenType>;
using IPrefixParselet = SimpleParser.IPrefixParselet<SimpleParser.TokenType>;
using IToken = SimpleParser.IToken<SimpleParser.TokenType>;
using InfixParselet = SimpleParser.InfixParselet<SimpleParser.TokenType>;
using IParserMap = SimpleParser.IParserMap<SimpleParser.TokenType>;

namespace Bantam
{    
    public class BantamMap : IParserMap
    {
        private readonly IDictionary<TokenType,IPrefixParselet> _prefixParselets =  new Dictionary<TokenType, IPrefixParselet>();
        private readonly IDictionary<TokenType, InfixParselet> _infixParselets = new Dictionary<TokenType, InfixParselet>();

        public BantamMap(ITokenConfig<char> tokenConfig)
        {

            // Register all of the parselets for the grammar.    
            // Register the ones that need special parselets.
            _prefixParselets.Add(TokenType.NAME, new NameParselet());
            _prefixParselets.Add(TokenType.LEFT_PAREN, new GroupParselet());

            // Register the simple operator parselets.
            _prefixParselets.Add(TokenType.PLUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig));
            _prefixParselets.Add(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig));
            _prefixParselets.Add(TokenType.TILDE, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig));
            _prefixParselets.Add(TokenType.BANG, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig));

            // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
            _infixParselets.Add(TokenType.BANG, new PostfixOperatorParselet(Precedence.POSTFIX,tokenConfig));
            _infixParselets.Add(TokenType.ASSIGN, new AssignParselet());
            _infixParselets.Add(TokenType.QUESTION, new ConditionalParselet());
            _infixParselets.Add(TokenType.LEFT_PAREN, new CallParselet());
            _infixParselets.Add(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig));
            _infixParselets.Add(TokenType.MINUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig));
            _infixParselets.Add(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig));
            _infixParselets.Add(TokenType.SLASH, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig));
            _infixParselets.Add(TokenType.CARET, new BinaryOperatorParselet(Precedence.EXPONENT, InfixType.Right,tokenConfig));
        }

        public void Register(TokenType tokenType, IPrefixParselet parselet)
        {
            _prefixParselets.Add(tokenType, parselet);
        }

        public void Register(TokenType tokenType, InfixParselet parselet)
        {
            _infixParselets.Add(tokenType, parselet);
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