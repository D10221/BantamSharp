using SimpleParser;
using System;
using System.Linq;
using System.Text;

namespace Bantam
{   
    public class Printer : IExpressionVisitor<TokenType>
    {
        public static Printer Create(){
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
        public string Print(ISimpleExpression<TokenType> expression){
            this.Visit(expression);
            return _builder.ToString();
        }
        #region  IExpressionVisitor
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
                        Visit(e.Right);
                        _builder.Append(")");
                        break;
                    }
                case BinaryOperatorExpression e:
                    {
                        _builder.Append("(");
                        Visit(e.Left);
                        _builder.Append(e.Token);
                        Visit(e.Right);
                        _builder.Append(")");
                        break;
                    }
                case ConditionalExpression e:
                    {
                        _builder.Append("(");
                        Visit(e.Condition);
                        _builder.Append("?");
                        Visit(e.Then);
                        _builder.Append(":");
                        Visit(e.Else);
                        _builder.Append(")");
                        break;
                    }
                case EmptyExpression<TokenType> e:
                    {
                        break;
                    }
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
                        _builder.Append("(");
                        Visit(e.Left);
                        _builder.Append(e.Token);
                        _builder.Append(")");
                        break;
                    }
                case PrefixExpression e:
                    {
                        _builder.Append("(");
                        _builder.Append(e.Token);
                        Visit(e.Right);
                        _builder.Append(")");
                        break;
                    }
                default: throw new Exception($"Unknown expression:'${expression.GetType()}'");
            }
        }
        #endregion
    }
}