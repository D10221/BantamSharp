using System;
using System.Collections.Generic;
using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class CallExpression : ISimpleExpression
    {
        public CallExpression(ISimpleExpression function, List<ISimpleExpression> args)
        {
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