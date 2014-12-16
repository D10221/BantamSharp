﻿using System.Collections.Generic;
using SimpleMaths;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType,string>> ;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType,string>>;
using ParserConfig = SimpleParser.ParserConfig<SimpleMaths.TokenType,string>;
using ParserMap = SimpleParser.ParserMap<SimpleMaths.TokenType,string>;
using IParserMap = SimpleParser.IParserMap<SimpleMaths.TokenType,string>;
using Parser = SimpleParser.Parser<SimpleMaths.TokenType,string>;
using IBuilder = SimpleParser.IBuilder<string>;


namespace SimpleMathTests
{
    public class TestParser
    {
        private readonly ParserConfig _config;

        public TestParser(ParserConfig config, IBuilder builder)
        {
            _config = config;
            _builder = builder;           
        }

        public  string Parse(string source)
        {            
            var lexer = new Lexer(source, new TokenConfig());
            var parserMap = new ParserMap(_config.PrefixParselets,_config.InfixParselets);
            var parser = new Parser(lexer,parserMap);
            var result = parser.ParseExpression();
           
            result.Print(_builder);
            var actual = _builder.Build();
            return actual;
        }

        public static readonly TestParserFactory Factory =  new TestParserFactory();
        private readonly IBuilder _builder;

        public  class TestParserFactory
       {
           public  TestParser CreateNew(IEnumerable<Prefix> prefixes , IEnumerable<Infix> infixes = null)
           {
               var config = ParserConfig.Factory.CreateNew(prefixes, infixes);
               return new TestParser(config, new Builder());
           }

            
       }
    }
}