namespace TokenSplitting
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

        public void Deconstruct(out string value, out int line, out int column)
        {
            value = Value;
            line = Line;
            column = Column;
        }
        public static implicit operator TokenSource((string value, int line, int column) x)
        {
            var (value, line, column) = x;
            return TokenSource.From(value, line, column);
        }
        public static implicit operator (string value, int line, int column)(TokenSource x){
            var (value, line, column) = x;
            return (value, line, column);
        }
    }
}