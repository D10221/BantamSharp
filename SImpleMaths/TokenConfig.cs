using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<string>;
using TokenFunction = System.Func<int, int, int>;
using TokenTypeConfig = System.Tuple<string, System.Func<int, int, int>>;
using ConfigItem = System.Tuple<SimpleMaths.TokenType,string, System.Func<int, int, int>>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType, string>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType, string>;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType,string>>;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType,string>>;
using IParserMap = SimpleParser.IParserMap<SimpleMaths.TokenType, string>;

namespace SimpleMaths
{
    public class TokenConfig : ITokenConfig<TokenType,string>
    {
        public IParserMap ParserMap { get; private set; }

        public TokenConfig()
        {
            ConfigItem[] config = {
                new ConfigItem(TokenType.PLUS,"+", (l,r)=> l+r ),
                new ConfigItem(TokenType.MINUS,"-", (l,r)=> l-r ),
                new ConfigItem(TokenType.ASTERISK,"*", (l,r)=> l*r ),
                new ConfigItem(TokenType.SLASH,"/", (l,r)=> r>0?  l/r : 0 ),
                new ConfigItem(TokenType.ASSIGN,"=", (l,r)=> r )
            };

            Prefix[] prefixes =
            {
                new Prefix(TokenType.PLUS, new PrefixOperatorParselet(Precedence.PREFIX,this)),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX,this))
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASSIGN, new AssignParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,this)),
                new Infix(TokenType.MINUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,this)),
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,this)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,this))
            };

            ParserMap = new ParserMap(prefixes,infixes);

            var values = Enum
                .GetValues(typeof (TokenType))
                .Cast<TokenType>()
                .ToArray();

            TokenTypes = values.Select(t => new Tuple<TokenType, string>(t, Punctuator(t)));

            Punctuators = config.ToDictionary(x => x.Item1, x => x.Item2);

            Funcs = config.ToDictionary(x => x.Item1, x => x.Item3);
        }
      
       
        private IDictionary<TokenType, TokenFunction> Funcs { get; set; }

        public Func<int, int, int> GetFunc(string punctuator)
        {
            var found = Punctuators.FirstOrDefault(p => p.Value == punctuator);
            var foundFUnc = Funcs.FirstOrDefault(f => f.Key == found.Key);
            return foundFUnc.Value;           
        }
       
        /// <summary>
        ///     If the TokenType represents a punctuator (i.e. a token that can split an identifier like '+', this will get its
        ///     text.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public string Punctuator(TokenType tokenType)
        {
            return Punctuators.FirstOrDefault(p => p.Key == tokenType).Value;
        }

        public bool IsValidPunctuator(string c)
        {
            var reverse = Punctuators.ToDictionary(p => p.Value, p => p.Key);
            TokenType pp;
            var ok = reverse.TryGetValue(c, out pp);
            return ok;
        }

        public  IDictionary<TokenType, string> Punctuators { get; private set; }

        public IEnumerable<Tuple<TokenType, string>> TokenTypes { get; private set; }
    }
}