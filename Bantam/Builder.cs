using SimpleParser;
using System;
using System.Linq;
using System.Text;

namespace Bantam
{
    /// <summary>
    /// Abstraction :)
    /// </summary>
    public class Builder : IExpressionVisitor<TokenType>
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public void Visit(object c)
        {
            _builder.Append(c);
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        public void Visit(ISimpleExpression<TokenType> expression)
        {
            switch (expression)
            {
                case NameExpression e:
                    {
                        _builder.Append(e.Token);
                        break;
                    }
                case AssignExpression e:
                    {
                        _builder.Append("(");
                        _builder.Append(e.Token);
                        _builder.Append("=");
                        e.Right.Visit(this);
                        _builder.Append(")");
                        break;
                    }
                case BinaryOperatorExpression e:
                    {
                        _builder.Append("(");
                        e.Left.Visit(this);
                        _builder.Append(e.Token);
                        e.Right.Visit(this);
                        _builder.Append(")");
                        break;
                    }
                case ConditionalExpression e:
                    {
                        _builder.Append("(");
                        e.Condition.Visit(this);
                        _builder.Append("?");
                        e.Then.Visit(this);
                        _builder.Append(":");
                        e.Else.Visit(this);
                        _builder.Append(")");
                        break;
                    }
                case EmptyExpression<TokenType> e:
                    {
                        break;
                    }
                case FunctionCallExpression e:
                    {
                        e.Left?.Visit(this);
                        _builder.Append("(");
                        int count = e.Right.Count();
                        // var i = 0; i < count; i++
                        var i = 0;
                        foreach (var arg in e.Right)
                        {
                            arg.Visit(this);
                            if (++i < count) _builder.Append(",");
                        }
                        _builder.Append(")");
                        break;
                    }
                case LiteralExpression e:
                    {
                        _builder.Append(e.Token);
                        break;
                    }
                case PostfixExpression e:
                    {
                        _builder.Append("(");
                        e.Left.Visit(this);
                        _builder.Append(e.Token);
                        _builder.Append(")");
                        break;
                    }
                case PrefixExpression e:
                    {
                        _builder.Append("(");
                        _builder.Append(e.Token);
                        e.Right.Visit(this);
                        _builder.Append(")");
                        break;
                    }
                default: throw new Exception($"Unknown expression:'${expression.GetType()}'");
            }
        }
    }
}