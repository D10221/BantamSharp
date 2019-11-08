using System;
using System.Collections.Generic;

namespace uParserTests
{
    public class Lexer
    {
        private List<Token> queue;
        public Lexer(IEnumerable<Token> tokens)
        {
            queue = new List<Token>(tokens);
        }        
        public Token[] ToArray(){
            return queue.ToArray();
        }
        public int Count(){
            return queue.Count;
        }
        ///<sumary>
        /// Return and remove 1st Token from the queue
        ///</sumary>
        public void RemoveAt(int index)
        {
            queue.RemoveAt(index);
        }        
        public Token Find(Predicate<Token> predicate){            
            return queue.Find(predicate);
        }
        public void Remove(Token token){
            queue.Remove(token);
        }
        ///<summary>
        /// Queue Count > 0 
        ///</summary>
        public bool Any()
        {
            return queue.Count > 0;
        }

        ///<sumary>
        /// returns is not null 
        ///</sumary>
        public Token FirstOrDefault()
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
}