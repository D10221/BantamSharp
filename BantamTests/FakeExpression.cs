using SimpleParser;

using ParseException = SimpleParser.ParseException<Bantam.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using IParselet = SimpleParser.IParselet<Bantam.TokenType, char>;
using InfixParselet = SimpleParser.InfixParselet<Bantam.TokenType, char>;

namespace BantamTests
{
    public class FakeExpression : ISimpleExpression<char>
    {
        private readonly string _what;

        public FakeExpression(string what)
        {
            _what = what;
        }

        public void Print(IBuilder builder)
        {
            builder.Append(_what);
        }
    }
}