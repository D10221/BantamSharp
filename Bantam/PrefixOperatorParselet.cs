
using SimpleParser;
using System.Collections.Generic;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;

namespace Bantam
{
    /// <summary>
    ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
    ///     unary "-", "+", "~", and "!" expressions.
    /// </summary>
    public class PrefixOperatorParselet : IParselet<TokenType, char>
    {
        public int Precedence { get; }

        public IDictionary<TokenType, char> TokenTypes { get; }

        public PrefixOperatorParselet(int precedence, IDictionary<TokenType, char> tokenTypes)
        {
            Precedence = precedence;
            TokenTypes = tokenTypes;
        }

        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression(Precedence);

            return new PrefixExpression(TokenTypes, token.TokenType, right);
        }
    }
}
