using System.Collections.Generic;
using System.Linq;

namespace uParser
{
    /// <summary>
    /// Convert text to tokens
    /// </summary>
    public class Tokenizer
    {
        private readonly IDictionary<string, TokenType> _punctuators;
        private readonly TokenSplitter _tokenSplitter;

        TokenFactory tokenFactory;

        public Tokenizer(IDictionary<string, TokenType> punctuators)
        {
            _punctuators = punctuators;
            _tokenSplitter = new TokenSplitter(
                delimiters: _punctuators.Select(x => x.Key)
            );
            tokenFactory = new TokenFactory(punctuators);
        }

        public IEnumerable<Token> Tokenize(string text)
        {            
            return _tokenSplitter.Split(text).Select(tokenFactory.GetToken).ToArray();
        }

    }
}