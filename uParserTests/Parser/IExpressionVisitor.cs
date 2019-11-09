namespace uParserTests
{
    public interface IExpressionVisitor
    {
        void Visit(ISimpleExpression expression);
    }
}