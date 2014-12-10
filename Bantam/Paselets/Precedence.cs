namespace Bantam.Paselets
{
    /**
* Defines the different precendence levels used by the infix parsers. These
* determine how a series of infix expressions will be grouped. For example,
* "a + b * c - d" will be parsed as "(a + (b * c)) - d" because "*" has higher
* precedence than "+" and "-". Here, bigger numbers mean higher precedence.
*/
    public static class Precedence {
        // Ordered in increasing precedence.
        public static readonly int ASSIGNMENT  = 1;
        public static readonly int CONDITIONAL = 2;
        public static readonly int SUM         = 3;
        public static readonly int PRODUCT     = 4;
        public static readonly int EXPONENT    = 5;
        public static readonly int PREFIX      = 6;
        public static readonly int POSTFIX     = 7;
        public static readonly int CALL        = 8;
    }
}