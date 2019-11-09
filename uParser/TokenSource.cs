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
        public string Value { get; private set; }
        public int Line { get; private set; }
        public int Column { get; private set; }

        public override string ToString()
        {
            return Value?.ToString();
        }
    }
}