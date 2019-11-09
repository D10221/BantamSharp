namespace uParser
{
    public class TokenSource 
    {
        public static TokenSource From(string value, int line, int column)
        {
            return new TokenSource
            {
                Value = value,
                Line = line,
                Column = column
            };
        }
        public string Value { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        
        public override string ToString()
        {
            return Value?.ToString();
        }
    }
}