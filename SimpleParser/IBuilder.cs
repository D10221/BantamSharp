
namespace SimpleParser
{
    public interface IExpressionVisitor<T>
    {
        void Visit(ISimpleExpression<T> expression);
    }
}