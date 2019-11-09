namespace uParser
{
    public interface IExpressionVisitor
    {
        void Visit(ISimpleExpression expression);
    }
}