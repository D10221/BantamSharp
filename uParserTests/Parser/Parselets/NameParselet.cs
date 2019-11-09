

using System;
using System.Collections.Generic;
using System.Linq;


namespace uParserTests
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : PrefixParselet
    {
        public int Precedence { get; }

        public NameParselet()
        {

        }

        public ISimpleExpression Parse(
            Func<int,ISimpleExpression> parse,
            IList<Token> lexer,
            Token token
            )
        {
            return new NameExpression(token);
        }
    }
}