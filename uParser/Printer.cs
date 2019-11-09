using System;
using System.Linq;
using System.Text;

namespace uParser
{
    public class Printer : IExpressionVisitor
    {
        public static Printer Create()
        {
            return new Printer();
        }
        private readonly StringBuilder _builder = new StringBuilder();

        public void Visit(object c)
        {
            _builder.Append(c);
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
        public string Print(ISimpleExpression expression)
        {
            this.Visit(expression);
            return _builder.ToString();
        }
        public void Visit(ISimpleExpression expression)
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
                        _builder.Append(e.Token);
                        _builder.Append("=");
                        Visit(e.Right);
                        break;
                    }
                case BinaryOperatorExpression e:
                    {
                        Visit(e.Left);
                        _builder.Append(e.Token);
                        Visit(e.Right);
                        break;
                    }
                case ConditionalExpression e:
                    {                      
                        Visit(e.Condition);
                        _builder.Append("?");
                        Visit(e.Then);
                        _builder.Append(":");
                        Visit(e.Else);                      
                        break;
                    }
                case EmptyExpression e: { break; }
                case FunctionCallExpression e:
                    {
                        Visit(e.Left);
                        _builder.Append("(");
                        int count = e.Right.Count();
                        // var i = 0; i < count; i++
                        var i = 0;
                        foreach (var arg in e.Right)
                        {
                            Visit(arg);
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
                        Visit(e.Left);
                        _builder.Append(e.Token);
                        break;
                    }
                case PrefixExpression e:
                    {
                        _builder.Append(e.Token);
                        Visit(e.Right);
                        break;
                    }
                case GroupExpression e:
                    {
                        _builder.Append("(");
                        foreach (var el in e.Elements)
                        {
                            Visit(el);
                        }
                        _builder.Append(")");
                        break;
                    }                
                default: throw new Exception($"Unknown expression:'${expression.GetType()}'");
            }
        }
    }
}