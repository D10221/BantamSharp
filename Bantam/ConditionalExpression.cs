﻿
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ISimpleExpression<char>
    {
        public ConditionalExpression(
           ISimpleExpression<char> condition, ISimpleExpression<char> then, ISimpleExpression<char> @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

        private ISimpleExpression<char> Condition { get; }

        private ISimpleExpression<char> Then { get; }

        private ISimpleExpression<char> Else { get; }

        public void Print(IBuilder<char> builder)
        {
            builder.Append("(");
            Condition.Print(builder);
            builder.Append(" ? ");
            Then.Print(builder);
            builder.Append(" : ");
            Else.Print(builder);
            builder.Append(")");
        }
    }
}