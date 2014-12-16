using SimpleParser;
/*using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IPrefixParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IParser= SimpleParser.IParser<Bantam.TokenType,char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;*/
using SimpleParser.Expressions;

namespace Bantam.Paselets
{
    //TODO Move to SimpleParser
    public interface IParselet<TTokenType, TokenBase>
    {
        ISimpleExpression<TokenBase> Parse(IParser<TokenType,TokenBase> parser, IToken<TTokenType> token);
    }
}