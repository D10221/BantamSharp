using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<string>;

namespace SimpleMaths
{
    public enum TokenType
    {
        NONE,       
        ASSIGN,
        PLUS,
        MINUS,
        ASTERISK,
        SLASH,       
        NAME,
        EOF,
    }
    public class TokenConfig : ITokenConfig<TokenType,string>
    {
        public TokenConfig()
        {
            TokenTypes = new[]
            {
               
                TokenType.ASSIGN,
                TokenType.PLUS,
                TokenType.MINUS,
                TokenType.ASTERISK,
                TokenType.SLASH,              
                TokenType.NAME,
                TokenType.EOF
            }.Select(
                t => new Tuple<TokenType, string>(t, Punctuator(t)));

            Punctuators =  new Dictionary<TokenType, string>
            {
            
                {TokenType.ASSIGN, "="},
                {TokenType.PLUS, "+"},
                {TokenType.MINUS, "-"},
                {TokenType.ASTERISK, "*"},
                {TokenType.SLASH, "/"},
           
            };            
        }

        private IDictionary<TokenType, Func<int, int, int>> Funcs=new Dictionary<TokenType, Func<int, int, int>>
        {
            {TokenType.PLUS, (left,right)=> left+right },
            {TokenType.MINUS, (left,right)=> left-right },
            {TokenType.ASTERISK, (left,right)=> left*right },
            {TokenType.SLASH,(left,right)=> left / right },
            {TokenType.ASSIGN, (l,r)=> r}
        };

        public Func<int, int, int> GetFunc(string punctuator)
        {
            var found = Punctuators.FirstOrDefault(p => p.Value == punctuator);
            var foundFUnc = Funcs.FirstOrDefault(f => f.Key == found.Key);
            return foundFUnc.Value;           
        }

        public int? Op(ISimpleExpression leftExpression,ISimpleExpression rightRightExpression,TokenType tokenType)
        {
            int? res = null;
            int leftRes;
            
            var left = leftExpression.ToString();
            
            var right = rightRightExpression.ToString();

            if (int.TryParse(left, out leftRes))
            {
                int rightRes;
                if (int.TryParse(right, out rightRes))
                {
                    switch (tokenType)
                    {
                        case TokenType.PLUS:
                            res = leftRes + rightRes;
                            break;
                        case TokenType.MINUS:
                            res = leftRes - rightRes;
                            break;
                        
                    }
                }
            }
            return res;

        }

        /// <summary>
        ///     If the TokenType represents a punctuator (i.e. a token that can split an identifier like '+', this will get its
        ///     text.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public string Punctuator(TokenType tokenType)
        {
            switch (tokenType)
            {
              
                case TokenType.ASSIGN:
                    return "=";
                case TokenType.PLUS:
                    return "+";
                case TokenType.MINUS:
                    return "-";
                case TokenType.ASTERISK:
                    return "*";
                case TokenType.SLASH:
                    return "/";               
                default:
                    return default(string);
            }
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