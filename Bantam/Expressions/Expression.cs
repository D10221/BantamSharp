using System.Text;

namespace Bantam.Expressions
{
    /// <summary>
    /// Interface for all expression AST node classes.
    /// </summary>
    public interface Expression 
    {
        /// <summary>
        ///   Build, the expression 
        /// </summary>
        /// <param name="builder"></param>
        void Print(IBuilder builder);
    }
}

