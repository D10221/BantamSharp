using SimpleParser;
using System;
using IBuilder = SimpleParser.IBuilder<string>;

namespace SimpleMaths
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression<string>
    {
        private readonly string _name;

        public NameExpression(String name)
        {
            _name = name;
        }

        public String GetName() { return _name; }

        public void Print(IBuilder builder)
        {
            builder.Append(_name);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}