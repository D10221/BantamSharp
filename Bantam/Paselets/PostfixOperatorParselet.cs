using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
    /// <summary>
    /// Generic infix parselet for an unary arithmetic operator. Parses postfix
    /// unary "?" expressions.
    /// </summary>
    public class PostfixOperatorParselet : InfixParselet
    {   
        public PostfixOperatorParselet(Precedence precedence)
        {
            _precedence = precedence;
        }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            return new PostfixExpression(left, token.GetTokenType());
        }

        public int Precedence
        {
            get { return (int) _precedence; }
        }

        private readonly Precedence _precedence;
    }
}
