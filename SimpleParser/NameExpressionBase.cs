using System;

namespace SimpleParser
{
    public abstract class NameExpressionBase<TCHAR> : ISimpleExpression<TCHAR>
    {
        protected NameExpressionBase(String name) {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract void Print(IBuilder<TCHAR> builder);
    }
}