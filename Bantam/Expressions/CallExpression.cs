using System;
using System.Collections.Generic;
using SimpleParser;

namespace Bantam.Expressions
{
    //TODO: Should only accept Name Expressions, and possibly the resulting NameExpression of a call(Func)eXpression
    // FOr now limit to Name Expressions
    /// <summary>
    /// A function call like "a(b, c, d)".
    /// </summary>
    public class CallExpression : ISimpleExpression
    {
        public CallExpression(ISimpleExpression function, List<ISimpleExpression> args)
        {
            if (!(function is NameExpression)) 
                throw new ArgumentException("Can't only deal with NameExpressions");
            _function = function;
            
            _args = args?? new List<ISimpleExpression>();
        }

        public void Print(IBuilder builder)
        {
            _function.Print(builder);
            builder.Append("(");
            for (var i = 0; i < _args.Count; i++)
            {
                _args[i].Print(builder);
                if (i < _args.Count - 1) builder.Append(", ");
            }
            builder.Append(")");
        }

        private readonly ISimpleExpression _function;
        private readonly List<ISimpleExpression> _args;
    }
}