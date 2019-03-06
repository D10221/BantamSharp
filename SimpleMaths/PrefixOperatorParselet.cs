﻿
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleMaths.TokenType, string>;
using ISimpleExpression = SimpleParser.ISimpleExpression<string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<SimpleMaths.TokenType, string>;

namespace SimpleMaths
{
    /// <summary>
    ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
    ///     unary "-", "+", "~", and "!" expressions.
    /// </summary>
    public class PrefixOperatorParselet : IParselet<TokenType, string>
    {

        public int Precedence { get; }

        private readonly ITokenConfig _tokenConfig;

        public PrefixOperatorParselet(int precedence, ITokenConfig tokenConfig)
        {
            _tokenConfig = tokenConfig;
            Precedence = precedence;
        }

        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression(Precedence);

            return new PrefixExpression(_tokenConfig, token.TokenType, right);
        }

    }
}