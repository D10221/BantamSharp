
using SimpleParser;
using System.Collections.Generic;
using InfixParselet = SimpleParser.InfixParselet<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using IParselet = SimpleParser.IParselet<Bantam.TokenType, char>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;

namespace Bantam
{
    public class BantamMap : IParserMap
    {
        private readonly IDictionary<TokenType, IParselet> _prefixParselets = new Dictionary<TokenType, IParselet>();
        private readonly IDictionary<TokenType, InfixParselet> _infixParselets = new Dictionary<TokenType, InfixParselet>();

        public BantamMap(ITokenConfig tokenConfig)
        {

            // Register all of the parselets for the grammar.    
            // Register the ones that need special parselets.
            _prefixParselets.Add(TokenType.NAME, new NameParselet());
            _prefixParselets.Add(TokenType.LEFT_PAREN, new GroupParselet());

            // Register the simple operator parselets.
            _prefixParselets.Add(TokenType.PLUS, new PrefixOperatorParselet((int)Precedence.PREFIX, tokenConfig));
            _prefixParselets.Add(TokenType.MINUS, new PrefixOperatorParselet((int)Precedence.PREFIX, tokenConfig));
            _prefixParselets.Add(TokenType.TILDE, new PrefixOperatorParselet((int)Precedence.PREFIX, tokenConfig));
            _prefixParselets.Add(TokenType.BANG, new PrefixOperatorParselet((int)Precedence.PREFIX, tokenConfig));

            // For kicks, we'll make "!" both prefix and postfix, kind of like ++.
            _infixParselets.Add(TokenType.BANG, new PostfixOperatorParselet((int)Precedence.POSTFIX, tokenConfig));
            _infixParselets.Add(TokenType.ASSIGN, new AssignParselet());
            _infixParselets.Add(TokenType.QUESTION, new ConditionalParselet());
            _infixParselets.Add(TokenType.LEFT_PAREN, new CallParselet());
            _infixParselets.Add(TokenType.PLUS, new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left, tokenConfig));
            _infixParselets.Add(TokenType.MINUS, new BinaryOperatorParselet((int)Precedence.SUM, InfixType.Left, tokenConfig));
            _infixParselets.Add(TokenType.ASTERISK, new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left, tokenConfig));
            _infixParselets.Add(TokenType.SLASH, new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left, tokenConfig));
            _infixParselets.Add(TokenType.CARET, new BinaryOperatorParselet((int)Precedence.EXPONENT, InfixType.Right, tokenConfig));
        }

        public void Register(TokenType tokenType, IParselet parselet)
        {
            _prefixParselets.Add(tokenType, parselet);
        }

        public void Register(TokenType tokenType, InfixParselet parselet)
        {
            _infixParselets.Add(tokenType, parselet);
        }

        public InfixParselet GetInfixParselet(TokenType tokenType)
        {
            _infixParselets.TryGetValue(tokenType, out InfixParselet parselet);
            return parselet;
        }

        public IParselet GetPrefixParselet(TokenType tokenType)
        {
            _prefixParselets.TryGetValue(tokenType, out IParselet parselet);
            return parselet;
        }
    }


}