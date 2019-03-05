namespace SimpleParser
{
    /// <summary>
    /// Interface for all expression AST node classes.
    /// </summary>
    public interface ISimpleExpression<TTokenBase>
    {
        /// <summary>
        ///   Build, the expression 
        /// </summary>
        /// <param name="builder"></param>
        void Print(IBuilder<TTokenBase> builder);
    }
}

