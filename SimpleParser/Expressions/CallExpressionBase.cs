using System.Collections.Generic;

namespace SimpleParser.Expressions
{
    public abstract class CallExpressionBase<TCHAR> : ISimpleExpression<TCHAR>
    {
        protected CallExpressionBase(ISimpleExpression<TCHAR> function, List<ISimpleExpression<TCHAR>> args)
        {
            Function = function;
            
            Args = args?? new List<ISimpleExpression<TCHAR>>();
        }

        protected ISimpleExpression<TCHAR> Function { get; private set; }

        protected List<ISimpleExpression<TCHAR>> Args { get; private set; }

        public abstract void Print(IBuilder<TCHAR> builder);
    }
}