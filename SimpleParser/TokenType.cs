using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public enum TokenType
    {        
        LEFT_PAREN,
        RIGHT_PAREN,
        COMMA,
        ASSIGN,
        PLUS,
        MINUS,
        ASTERISK,
        SLASH,
        CARET,
        TILDE,
        BANG,
        QUESTION,
        COLON,
        NAME,
        EOF,
        LETTER,
        WORD,
        NUMBER,
        NONE
    }

    public static class TokenTypes
    {
        //TokenTypes that are explicit punctuators.
        public static readonly IEnumerable<Tuple<TokenType, char>> Punctuators;

        public static readonly TokenType[] Values =
        {
            TokenType.LEFT_PAREN,
            TokenType.RIGHT_PAREN,
            TokenType.COMMA,
            TokenType.ASSIGN,
            TokenType.PLUS,
            TokenType.MINUS,
            TokenType.ASTERISK,
            TokenType.SLASH,
            TokenType.CARET,
            TokenType.TILDE,
            TokenType.BANG,
            TokenType.QUESTION,
            TokenType.COLON,
            TokenType.NAME,
            TokenType.EOF
        };

        static TokenTypes()
        {
            Punctuators = Values.Select(t => new Tuple<TokenType, Char> (t, t.Punctuator()));
        }
    }
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

        public static TokenType TryGetValue(this IEnumerable<Tuple<TokenType, char>> tt,char c)
        {
            return tt.FirstOrDefault(t => t.Char() == c).TknType();
        }

        public static Tuple<TokenType, char> TryGet(this IEnumerable<Tuple<TokenType, char>> tt, char c)
        {
            return tt.FirstOrDefault(t => t.Char() == c);
        }
        
        public static TokenType TryGetValue(this IEnumerable<Tuple<TokenType, char>> tt,string c)
        {
            if(!string.IsNullOrEmpty(c) && c.Length < 2 )
            return tt.FirstOrDefault(t => t.Char() == c.First()).TknType();
            return TokenType.NONE;
        }
        /**
   * If the TokenType represents a punctuator (i.e. a token that can split an
   * identifier like '+', this will get its text.
   */
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
       
        

        public static IEnumerable<char> Values()
        {
           
            return TokenTypes.Values.Select(t=> t.Punctuator());
        }
    }
}