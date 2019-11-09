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
        public void GroupingThrows()
        {
            var ex = Assert.ThrowsException<ParseException>(() =>
            {
                var expr = Bantam.Parse("()");
            });
            IsNotNull(ex);
        }
        [TestMethod]
        public void GroupingThrows2()
        {
            var ex = ThrowsException<ParseException>(() => Bantam.Parse("(a")); 
            IsNotNull(ex);
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
        }
        [TestMethod]
        public void Grouping3()
        {
            var e = Bantam.Parse("a + (b + c) + d");
            var op1 = e as BinaryOperatorExpression;
            IsNotNull(op1);
            var left = op1.Left as BinaryOperatorExpression;
            IsTrue((left.Left as NameExpression).ToString().Equals("a"));
            var grp = left.Right as GroupExpression;
            IsNotNull(grp);
            var op2 = (grp.Elements.FirstOrDefault() as BinaryOperatorExpression);
            IsNotNull(op2);
            AreEqual((op2.Left as NameExpression).ToString(), "b");
            AreEqual((op2.Right as NameExpression).ToString(), "c");

        }
        [TestMethod]
        public void Grouping4()
        {
            var e = Bantam.Parse("a ^ (b + c)");
            var b = (e as BinaryOperatorExpression);
            IsNotNull(b);
            AreEqual(
                (b.Left as NameExpression).ToString(),
                "a"
            );
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
            var e = Bantam.Parse("a(b, c)");
            var f = e as FunctionCallExpression;
            IsNotNull(f);
            var a = f.Left as NameExpression;
            AreEqual(a.ToString(), "a");
            var param = f.Right.ToArray();
            AreEqual((param[0] as NameExpression).ToString(), "b");
            AreEqual((param[1] as NameExpression).ToString(), "c");
        }
        [TestMethod]
        public void FunctionCall2()
        {
            var e = Bantam.Parse("a(b) + c(d)");
            var b = (e as BinaryOperatorExpression);
            IsNotNull(b);
            var f1 = (b.Left as FunctionCallExpression);
            var f1Left = (f1.Left as NameExpression);
            AreEqual(f1Left.ToString(), "a");
            AreEqual(
                (f1.Right.FirstOrDefault() as NameExpression).ToString(),
                "b"
            );
            var f2 = b.Right as FunctionCallExpression;
            AreEqual((f2.Left as NameExpression).ToString(), "c");
            AreEqual(
                f2.Right.FirstOrDefault().ToString(),
                "d"
            );
        }
        [TestMethod]
        public void FunctionCallThrows()
        {
            var ex = ThrowsException<ParseException>(() =>
            {
                Bantam.Parse("a(a");
            });
            IsNotNull(ex as ParseException);
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
            //  "(~(!(-(+a))))"
            var e = Bantam.Parse("~!-+a");
            var x = e as PrefixExpression;
            IsNotNull(x);
            AreEqual(x.Token.ToString(), "~");
            var x1 = x.Right as PrefixExpression;
            IsNotNull(x1);
            AreEqual(x1.Token.ToString(), "!");
            var x2 = x1.Right as PrefixExpression;
            AreEqual(x2.Token.ToString(), "-");
            var x3 = x2.Right as PrefixExpression;
            AreEqual(x3.Token.ToString(), "+");
            var x4 = x3.Right as NameExpression;
            AreEqual(x4.ToString(), "a");
        }

        [TestMethod]
        public void UnaryAndBinaryPrecedence()
        {
            // Unary and binary predecence.
            var e = Bantam.Parse("-a * b");
            // "((-a)*b)"
            var b = e as BinaryOperatorExpression;
            IsTrue(b.Token.ToString().Equals("*"));
            var p = (b.Left as PrefixExpression);
            AreEqual(p.Token.ToString(), "-");
            var n = p.Right as NameExpression;
            IsTrue(n.ToString().Equals("a"));
            var n2 = b.Right as NameExpression;
            IsTrue(n2.ToString().Equals("b"));

            ParsedAreEqual("!a + b", "!a+b");
            ParsedAreEqual("~a ^ b", "~a^b");
            ParsedAreEqual("-a!", "-a!");
            ParsedAreEqual("!a!", "!a!");
        }
        [TestMethod]
        public void BinaryPrecedence()
        {
            // Binary precedence.
            ParsedAreEqual("a = b + c * d ^ e - f / g", "a=b+c*d^e-f/g");
        }
        [TestMethod]
        public void BinaryAssociativity()
        {
            // Binary associativity.
            ParsedAreEqual("a = b = c", "a=b=c");
            ParsedAreEqual("a + b - c", "a+b-c");
            ParsedAreEqual("a * b / c", "a*b/c");
            ParsedAreEqual("a ^ b ^ c", "a^b^c");
        }
        [TestMethod]
        public void ConditionalOperator()
        {
            // Conditional operator.
            ParsedAreEqual("a ? b : c ? d : e", "a?b:c?d:e");
            ParsedAreEqual("a ? b ? c : d : e", "a?b?c:d:e");
            ParsedAreEqual("a + b ? c * d : e / f", "a+b?c*d:e/f");
        }
    }
}
