

namespace uParserTests
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression
    {        
        public Token Token { get; }

        public NameExpression(Token token)
        {
            Token = token;
        }
        public override string ToString()
        {
            return $"{Token}";
        }
    }
}