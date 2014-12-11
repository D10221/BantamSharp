using System;
using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameSimpleExpression : ISimpleExpression {
        private readonly string _name;

        public NameSimpleExpression(String name) {
            _name = name;
        }
  
        public String getName() { return _name; }
  
        public void Print(IBuilder builder) {
            builder.Append(_name);
        }
    }
}