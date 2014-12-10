using System;
using System.Text;

namespace Bantam.Expressions
{
    /**
* A simple variable name expression like "abc".
*/
    public class NameExpression : Expression {
        private readonly string _name;

        public NameExpression(String name) {
            _name = name;
        }
  
        public String getName() { return _name; }
  
        public void Print(IBuilder builder) {
            builder.Append(_name);
        }
    }
}