using System;
using System.Collections.Generic;

namespace uParserTests
{
    /// <summary>
    ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
    ///     unary "-", "+", "~", and "!" expressions.
    /// </summary>
    public class PrefixOperatorParselet : PrefixParselet
    {
        public int Precedence { get; }

        public PrefixOperatorParselet(int precedence)
        {
            Precedence = precedence;
        }
        public ISimpleExpression Parse(Func<int,ISimpleExpression> parse, IList<Token> lexer, Token token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parse(Precedence);
            return new PrefixExpression(token, right);
        }
    }
}
