using System;
using System.Collections.Generic;
using System.Linq;

namespace uParser
{
    public static class Lexer
    {
        ///<sumary>
        /// Peek queue's index position
        /// throws 'IndexOutOfRangeException'
        ///</sumary>
        public static Token Peek(this IList<Token> lexer, int index = 0)
        {
            if ((lexer.Count() - 1) >= index)
            {
                return lexer.ToArray()[index];
            }
            throw new IndexOutOfRangeException($"queue is less than {index} long");
        }
        ///<sumary>
        /// Lookup n positions , return true, success if no exceptions and token != default
        ///</sumary>
        public static bool TryPeek(this IList<Token> lexer, out Token token)
        {
            try
            {
                token = lexer.Peek(0);
                return token != default;
            }
            catch (System.Exception)
            {
                token = default;
                return false;
            }

        }
        ///<sumary>
        /// Peek Token, returns Matches expected <TokenType/> ignores IndexOutOfRangeException
        ///</sumary>
        public static bool TryPeek(this IList<Token> lexer, TokenType tokenType, out Token token)
        {
            try
            {
                token = lexer.Peek();
                return token?.TokenType == tokenType;
            }
            catch (IndexOutOfRangeException)
            {
                token = default;
                return false;
            }
        }
        ///<sumary>
        /// Peek next Token, return is Match expected
        ///</sumary>
        public static bool Peek(this IList<Token> lexer, TokenType tokenType, out Token token)
        {
            token = lexer.Peek();
            return token?.TokenType == tokenType;
        }
        public static Token Consume(this IList<Token> lexer)
        {
            if (lexer.FirstOrDefault(out var token)) lexer.RemoveAt(0);
            return token;
        }
        ///<summary>
        /// Consume specific token
        ///</summary>
        public static void Consume(this IList<Token> lexer, Token token)
        {
            if (token == default) throw new System.ArgumentException("Input token required", nameof(token));

            var found = lexer.FirstOrDefault(x => ReferenceEquals(x, token));
            if (found != default) lexer.Remove(found);
            else throw new TokenNotFoundException($"{token} NOT found!");
        }
        ///<sumary>
        ///  Consume if matches expected, returns consumed
        ///</sumary>
        public static bool ConsumeIf(this IList<Token> lexer, TokenType expected, out Token next)
        {
            next = lexer.FirstOrDefault();
            bool success = next?.TokenType == expected;
            if (success) lexer.Consume();
            return success;
        }
        ///<sumary>
        /// returns is not null 
        ///</sumary>
        public static bool FirstOrDefault(this IList<Token> lexer, out Token token)
        {
            token = !lexer.Any() ? default : lexer.FirstOrDefault();
            return token != default;
        }

        static string Text(this IList<Token> lexer)
        {
            var x = System.Linq.Enumerable.Aggregate(
                System.Linq.Enumerable.Select(lexer, t => t.ToString()),
                (a, b) => a + "," + b
            );
            return "[" + x + "]";
        }

    }
}