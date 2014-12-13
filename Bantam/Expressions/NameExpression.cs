using System;
using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression {
        private readonly string _name;

        public NameExpression(String name) {
            _name = name;
        }
  
        public String GetName() { return _name; }
  
        public void Print(IBuilder builder) {
            builder.Append(_name);
        }
    }
}