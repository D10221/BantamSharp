
namespace Bantam
{
    /**
    * Defines the different precendence levels used by the infix parsers. These
    * determine how a series of infix expressions will be grouped. For example,
    * "a + b * c - d" will be parsed as "(a + (b * c)) - d" because "*" has higher
    * precedence than "+" and "-". Here, bigger numbers mean higher precedence.
    */
    public enum Precedence
    {
        // Ordered in increasing precedence.
        ZERO = 0,
        ASSIGNMENT,
        CONDITIONAL ,
        OR, 
        AND,
        NOT_EQUALS,
        EQUALS, 
        GREATER_EQUALS_THAN,
        GREATER_THAN,
        LESS_EQUALS_THAN,
        LESS_THAN,
        SUM , 
        PRODUCT ,
        EXPONENT ,
        PREFIX ,
        POSTFIX ,
        CALL ,
    }
}
