using System.Collections.Generic;
using IBuilder = SimpleParser.IBuilder<string>;

namespace SimpleMaths
{
    public class Builder:IBuilder
    {
        private string _text;

        public Builder()
        {
            _text = "0";
        }
        
       
        public IBuilder Append(string s)
        {           
            _text += s;
            return this;
        }

        public IBuilder Append(IEnumerable<string> cs)
        {
            foreach (var item in cs)
            {
                _text += item;
            }
            return this;
        }

        public string Build()
        {
            return _text.ToString();
        }
    }
}
