
namespace SimpleParser
{
    /// <summary>
    /// Interface for all expression AST node classes.
    /// </summary>
    public interface ISimpleExpression
    {
        object Token { get; }
        /// <summary>
        ///   Build, the expression 
        /// </summary>
        /// <param name="builder"></param>
        void Print(IBuilder builder);
    }
}

