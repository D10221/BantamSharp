using System;

namespace SimpleParser.Expressions
{
    public abstract class AssignExpressionBase<TCHAR> : ISimpleExpression<TCHAR>
    {
        protected AssignExpressionBase(String name, ISimpleExpression<TCHAR> right) {
            Name = name;
            Right = right;
        }

        protected string Name { get; private set; }

        protected ISimpleExpression<TCHAR> Right { get; private set; }

        public abstract void Print(IBuilder<TCHAR> builder);
    }
}