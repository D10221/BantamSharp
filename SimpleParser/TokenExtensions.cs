using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    //?
    public static class TokenExtensions
    {
        public static IEnumerable<T> Range<T>(this IEnumerable<T> source,int start, int end )
        {
            return source.Where((t, i) => i > start && i < end);
        }

        public static T With<T>(this T t, Action<T> action, Func<T,bool> condition=null)
        {
            condition = condition ?? (x => !Equals(x, default(T)));
            if (condition(t)) action(t);
            return default(T);
        }
        
        public static R Extract<T,R>(this T t, Func<T,R> action, Func<T,bool> condition=null)
        {
            condition = condition ?? (x => !Equals(x, default(T)));
            return condition(t) ? action(t) : default(R);
        }

        public static R Extract<R>(this Tuple<TokenType, char> tt, Func<Tuple<TokenType, char>, R> ttAction)
        {
            if (tt.HasValue())
                return  ttAction(tt);
            return default(R);
        }

        public static bool HasValue(this Tuple<TokenType, char> tt)
        {
            return tt != null && tt.Item1 != TokenType.NONE;
        }
        public static TokenType TknType(this Tuple<TokenType, char> tuple)
        {
            return tuple != null ? tuple.Item1 : TokenType.NONE;
        }

        public static char Char(this Tuple<TokenType, char> tuple)
        {
            return tuple != null ? tuple.Item2 : default(char);
        }
       
        public static Tuple<TokenType, char> GetTokenType(this IEnumerable<Tuple<TokenType, char>> tt, char c)
        {
            return tt.FirstOrDefault(t => t.Char() == c);
        }               

        /// <summary>
        /// If the TokenType represents a punctuator (i.e. a token that can split an identifier like '+', this will get its text.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public static char Punctuator(this TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.LEFT_PAREN: return '(';
                case TokenType.RIGHT_PAREN: return ')';
                case TokenType.COMMA: return ',';
                case TokenType.ASSIGN: return '=';
                case TokenType.PLUS: return '+';
                case TokenType.MINUS: return '-';
                case TokenType.ASTERISK: return '*';
                case TokenType.SLASH: return '/';
                case TokenType.CARET: return '^';
                case TokenType.TILDE: return '~';
                case TokenType.BANG: return '!';
                case TokenType.QUESTION: return '?';
                case TokenType.COLON: return ':';
                default: return default(char);
            }
        }

        public static bool IsValidPunctuator(this char c)
        {
            var reverse = Punctuators.ToDictionary(p => p.Value, p => p.Key);
            TokenType pp;
            var ok = reverse.TryGetValue(c, out pp);
            return ok;
        }
        
        public static readonly IDictionary<TokenType,char>  Punctuators= new Dictionary<TokenType, char>
        {
            {TokenType.LEFT_PAREN,'('},
            {TokenType.RIGHT_PAREN,')'},
            {TokenType.COMMA,','},
            {TokenType.ASSIGN,'='},
            {TokenType.PLUS,'+'},
            {TokenType.MINUS,'-'},
            {TokenType.ASTERISK,'*'},
            {TokenType.SLASH,'/'},
            {TokenType.CARET,'^'},
            {TokenType.TILDE,'~'},
            {TokenType.BANG,'!'},
            {TokenType.QUESTION,'?'},
            {TokenType.COLON,':'},
        };

        public static IEnumerable<char> Values()
        {           
            return TokenTypes.Values.Select(t=> t.Punctuator());
        }
    }
}