using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uParserTests
{
    using static Assert;
    [TestClass]
    public class ParserTests
    {
        /**
        * Parses the given chunk of code and verifies that it matches the expected
        * pretty-printed result.
        */
        void ParsedAreEqual(string source, string expected, [CallerMemberName] string caller = null)
        {
            var expr = Bantam.Parse(source);
            var builder = Printer.Create();
            string actual = builder.Print(expr);
            Assert.AreEqual(expected, actual, false, caller);
        }
        [TestMethod]
        public void Names()
        {
            var expr = Bantam.Parse("a") as NameExpression;
            IsNotNull(expr);
            ParsedAreEqual(expr.Token.Value.ToString(), "a");

        }
        [TestMethod]
        public void PostFix()
        {
            var expr = Bantam.Parse("a!") as PostfixExpression;
            IsNotNull(expr);
            IsTrue(expr.Token.TokenType == TokenType.BANG);
            IsTrue(expr.Token.Value.ToString() == "!");
            var name = expr.Left as NameExpression;
            IsNotNull(name);
            ParsedAreEqual(
                name.Token.Value.ToString(),
                "a"
            );
        }
        [TestMethod]
        public void PostFix1()
        {
            var postFix = Bantam.Parse("a!!") as PostfixExpression;
            IsNotNull(postFix);
            var postFix1 = postFix.Left as PostfixExpression;
            IsNotNull(postFix1);
            var name = postFix1.Left as NameExpression;
            IsNotNull(name);
            IsTrue(name.ToString().Equals("a"));
        }
        [TestMethod]
        public void GroupingVsFunctionCall()
        {
            // Grouping vs Function Call
            ParsedAreEqual("((a))", "((a))");
        }
        [TestMethod]
        public void GroupingVsFunctionCall1()
        {
            // Grouping vs Function Call
            ParsedAreEqual("(a())()", "(a())()");
        }
        [TestMethod]
        public void GroupingVsFunctionCall2()
        {
            // Grouping vs Function Call
            ParsedAreEqual("(a+ b)()", "(a+b)()");
        }
        [TestMethod]
        public void GroupingVsFunctionCall3()
        {
            // Grouping vs Function Call
            ParsedAreEqual("(a())", "(a())");
        }
        [TestMethod]
        public void Grouping()
        {
            Assert.ThrowsException<ParseException>(() =>
            {
                var expr = Bantam.Parse("()");
            });
        }
        [TestMethod]
        public void Grouping0()
        {
            var e = Bantam.Parse("((a+b)+c)");
            var g = e as GroupExpression;
            IsNotNull(g);
            var binary = g.Elements.FirstOrDefault() as BinaryOperatorExpression;
            IsNotNull(binary);
            IsTrue(
                (binary.Right as NameExpression).Token.Value.ToString().Equals("c")
            );
            var group2 = binary.Left as GroupExpression;
            IsNotNull(group2);
            var x = group2.Elements.FirstOrDefault() as BinaryOperatorExpression;
            var a = x.Left as NameExpression;
            IsTrue(a.Token.Value.ToString().Equals("a"));
            var b = x.Right as NameExpression;
            IsTrue(b.Token.Value.ToString().Equals("b"));
        }
        [TestMethod]
        public void GroupingVsPostfix()
        {
            var e = Bantam.Parse("(a)!");
            var postFix = e as PostfixExpression;
            IsNotNull(postFix);
            var child = postFix.Left as GroupExpression;
            IsNotNull(child);
            var name = child.Elements.FirstOrDefault() as NameExpression;
            IsNotNull(name);
            ParsedAreEqual(name.Token.Value.ToString(), "a");
        }
        [TestMethod]
        public void Grouping2()
        {
            ParsedAreEqual("(b + c)", "(b+c)");
            ParsedAreEqual("a + (b + c) + d", "((a+(b+c))+d)");
        }
        [TestMethod]
        public void Grouping3()
        {
            ParsedAreEqual("a ^ (b + c)", "(a^(b+c))");
        }
        [TestMethod]
        public void FunctionCall()
        {
            ParsedAreEqual("a()", "a()");
            ParsedAreEqual("a(b)", "a(b)");
        }
        [TestMethod]
        public void FunctionCall1()
        {
            ParsedAreEqual("a(b, c)", "a(b,c)");
        }
        [TestMethod]
        public void FunctionCall2()
        {
            ParsedAreEqual("a(b) + c(d)", "(a(b)+c(d))");
            ParsedAreEqual("a(b ? c : d, e + f)", "a((b?c:d),(e+f))");
        }
        [TestMethod]
        public void FunctionChain()
        {
            ParsedAreEqual("a(b)(c)", "a(b)(c)");
        }
        [TestMethod]
        public void Test1()
        {
            ParsedAreEqual("~a", "~a");
        }
        [TestMethod]
        public void UnaryPrecedence()
        {
            // Unary precedence.
            ParsedAreEqual("a!!!", "(((a!)!)!)");
            ParsedAreEqual("~!-+a", "(~(!(-(+a))))");
        }

        [TestMethod]
        public void UnaryAndBinaryPrecedence()
        {
            // Unary and binary predecence.
            ParsedAreEqual("-a * b", "((-a)*b)");
            ParsedAreEqual("!a + b", "((!a)+b)");
            ParsedAreEqual("~a ^ b", "((~a)^b)");
            ParsedAreEqual("-a!", "(-(a!))");
            ParsedAreEqual("!a!", "(!(a!))");
        }
        [TestMethod]
        public void BinaryPrecedence()
        {
            // Binary precedence.
            ParsedAreEqual("a = b + c * d ^ e - f / g", "(a=((b+(c*(d^e)))-(f/g)))");
        }
        [TestMethod]
        public void BinaryAssociativity()
        {
            // Binary associativity.
            ParsedAreEqual("a = b = c", "(a=(b=c))");
            ParsedAreEqual("a + b - c", "((a+b)-c)");
            ParsedAreEqual("a * b / c", "((a*b)/c)");
            ParsedAreEqual("a ^ b ^ c", "(a^(b^c))");
        }
        [TestMethod]
        public void ConditionalOperator()
        {
            // Conditional operator.
            ParsedAreEqual("a ? b : c ? d : e", "(a?b:(c?d:e))");
            ParsedAreEqual("a ? b ? c : d : e", "(a?(b?c:d):e)");
            ParsedAreEqual("a + b ? c * d : e / f", "((a+b)?(c*d):(e/f))");
        }
    }
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void Test1()
        {
            var tokens = new[] {
                new Token(TokenType.PARENT_LEFT,"("),
                new Token(TokenType.PARENT_RIGHT,")")
            };
            var lexer = new Lexer(tokens);
            var token = lexer.Consume();
            IsNotNull(token);
        }
    }
}
