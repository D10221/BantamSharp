using System;
using SimpleParser.Expressions;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType, string>>;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType, string>>;
using ParserConfig = SimpleParser.ParserConfig<SimpleMaths.TokenType, string>;
using ParserMap = SimpleParser.ParserMap<SimpleMaths.TokenType, string>;
using IParserMap = SimpleParser.IParserMap<SimpleMaths.TokenType, string>;
using Parser = SimpleParser.Parser<SimpleMaths.TokenType, string>;
using IBuilder = SimpleParser.IBuilder<string>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<string>;
using IParser= SimpleParser.IParser<SimpleMaths.TokenType,string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;
namespace SimpleMaths
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : ISimpleExpression<string>
    {
        private readonly string _name;

        public NameExpression(String name) {
            _name = name;
        }
  
        public String GetName() { return _name; }
  
        public void Print(IBuilder builder) {
            builder.Append(_name);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}