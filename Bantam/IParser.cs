using Bantam.Expressions;
using Bantam.Paselets;

namespace Bantam
{
    public interface IParser
    {
        void Register(TokenType token, IPrefixParselet parselet);
        void Register(TokenType token, InfixParselet parselet);
        ISimpleExpression ParseExpression();
        bool IsMatch(TokenType expected);
        IToken Consume(TokenType expected);
        IToken Consume();
    }
}