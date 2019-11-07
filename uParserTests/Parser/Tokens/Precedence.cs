namespace uParserTests
{
    public enum Precedence
    {
        // Ordered in increasing precedence.
        ZERO = 0,
        ASSIGNMENT,
        CONDITIONAL,
        AND,
        OR,
        EQUALS,
        NOT_EQUAL,
        LIKE,
        GREATER_EQUALS_THAN,
        GREATER_THAN,
        LESS_EQUALS_THAN,
        LESS_THAN,
        SUM,
        PRODUCT,
        EXPONENT,
        PREFIX,
        POSTFIX,
        CALL,
    }
}