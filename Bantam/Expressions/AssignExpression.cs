﻿using System;
using SimpleParser.Expressions;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;

namespace Bantam.Expressions
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : AssignExpressionBase<char> {
        public AssignExpression(String name, ISimpleExpression right) : base(name, right)
        {
        }

        public override void Print(IBuilder builder)
        {
            builder.Append("(").Append(Name).Append(" = ");
            Right.Print(builder);
            builder.Append(")");
        }
    }
}