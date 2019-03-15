
namespace SimpleParser
{
    /// <summary>
    /// Interface for all expression AST node classes.
    /// </summary>
    public interface ISimpleExpression<TokenType>
    {
        IToken<TokenType> Token { get; }        
    }
}

