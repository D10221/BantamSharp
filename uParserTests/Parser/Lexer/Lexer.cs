using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace uParserTests
{
    public class Lexer
    {
        private List<Token> queue;
        public Lexer(IEnumerable<Token> tokens)
        {
            queue = new List<Token>(tokens);
        }
        ///<sumary>
        /// Equeue next Token
        ///</sumary>
        public Token Lookup()
        {
            return FirstOrDefault();
        }
        ///<sumary>
        /// Lookup n positions , return true success 
        ///</sumary>
        public bool Lookup(int distance, out Token token)
        {
            if ((queue.Count - 1) >= distance)
            {
                token = Lookup(distance);
                return true;
            }
            token = default;
            return false;
        }
        ///<sumary>
        /// Lookup n positions 
        ///</sumary>
        public Token Lookup(int distance)
        {
            return queue.ToArray()[distance];
        }
        ///<sumary>
        /// Equeue next Token return is Match expected
        ///</sumary>
        public bool Lookup(TokenType tokenType, out Token token)
        {
            token = Lookup();
            return token?.TokenType == tokenType;
        }
        ///<sumary>
        /// Return and remove 1st Token from the queue
        ///</sumary>
        public Token Consume()
        {
            if (FirstOrDefault(out var token))
            {
                queue.RemoveAt(0);
            }
            return token;
        }
        ///<summary>
        /// Consume specific token
        ///</summary>
        public void Consume(Token token)
        {
            if (token == default) throw new System.ArgumentException("Input token required", nameof(token));

            var found = queue.Find(x => ReferenceEquals(x, token));
            if (found != default) queue.Remove(found);
            else throw new NotFoundException($"{token} NOT found!");
        }
        ///<summary>
        /// Try Consume specific token 
        ///</summary>
        public bool TryConsume(Token token)
        {
            try
            {
                Consume(token);
                return true;
            }
            catch (NotFoundException)
            {
                return false;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        ///<sumary>
        ///  Consume if matches expected, returns consumed
        ///</sumary>
        public bool ConsumeIf(TokenType expected, out Token next)
        {
            next = FirstOrDefault();
            bool success = next?.TokenType == expected;
            if (success) Consume();            
            return success;
        }
        ///<sumary>
        /// returns is not null 
        ///</sumary>
        private bool FirstOrDefault(out Token token)
        {
            token = !Any ? default : queue.ToArray()[0];
            return token != default;
        }
        ///<summary>
        /// Queue Count > 0 
        ///</summary>
        private bool Any => queue.Count > 0;
        ///<sumary>
        /// returns is not null 
        ///</sumary>
        private Token FirstOrDefault()
        {
            return queue.Count > 0 ? queue.ToArray()[0] : default;
        }
        ///<summary>
        ///
        ///</summary>
        public override string ToString()
        {
            var x = System.Linq.Enumerable.Aggregate(
                System.Linq.Enumerable.Select(queue, t => t.ToString()),
                (a, b) => a + "," + b
            );
            return "[" + x + "]";
        }
    }

    [Serializable]
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}