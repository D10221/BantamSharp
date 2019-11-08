using System;

namespace uParserTests
{
    public static class LexerExtensions
    {
        ///<sumary>
        /// Peek queue's index position
        /// throws 'IndexOutOfRangeException'
        ///</sumary>
        public static Token Peek(this Lexer lexer, int index = 0)
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
        public static bool TryPeek(this Lexer lexer, out Token token)
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
        /// Equeue next Token return is Match expected
        ///</sumary>
        public static bool Peek(this Lexer lexer, TokenType tokenType, out Token token)
        {
            token = lexer.Peek();
            return token?.TokenType == tokenType;
        }
        public static Token Consume(this Lexer lexer)
        {
            if (lexer.FirstOrDefault(out var token)) lexer.RemoveAt(0);
            return token;
        }
        ///<summary>
        /// Consume specific token
        ///</summary>
        public static void Consume(this Lexer lexer, Token token)
        {
            if (token == default) throw new System.ArgumentException("Input token required", nameof(token));

            var found = lexer.Find(x => ReferenceEquals(x, token));
            if (found != default) lexer.Remove(found);
            else throw new TokenNotFoundException($"{token} NOT found!");
        }
        ///<sumary>
        ///  Consume if matches expected, returns consumed
        ///</sumary>
        public static bool ConsumeIf(this Lexer lexer, TokenType expected, out Token next)
        {
            next = lexer.FirstOrDefault();
            bool success = next?.TokenType == expected;
            if (success) lexer.Consume();
            return success;
        }
        ///<sumary>
        /// returns is not null 
        ///</sumary>
        public static bool FirstOrDefault(this Lexer lexer, out Token token)
        {
            token = !lexer.Any() ? default : lexer.FirstOrDefault();
            return token != default;
        }

    }
}