using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression<char>
    {
        public string Name { get; private set; }

        public NameExpression(string name)
        {
            Name = name;
        }
        public void Print(IBuilder<char> builder)
        {
            builder.Append(Name);
        }
    }
}