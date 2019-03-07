using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression
    {
        public object Name { get; }

        public NameExpression(object name)
        {
            Name = name;
        }
        public void Print(IBuilder builder)
        {
            builder.Append(Name);
        }
    }
}