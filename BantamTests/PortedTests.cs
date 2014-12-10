using System;
using System.Collections.Generic;
using System.Text;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class PortedTests
    {
        
        void Setup()
        {
            // Function call.
            _expectations.Add(new Tuple<string, string>("a()","a()"));
            _expectations.Add(new Tuple<string, string>("a(b)","a(b)"));
            _expectations.Add(new Tuple<string, string>("a(b, c)","a(b, c)"));
            _expectations.Add(new Tuple<string, string>("a(b)(c)","a(b)(c)"));
            _expectations.Add(new Tuple<string, string>("a(b) + c(d)","(a(b) + c(d))"));
            _expectations.Add(new Tuple<string, string>("a(b ? c : d, e + f)","a((b ? c : d), (e + f))"));

            // Unary precedence.
            _expectations.Add(new Tuple<string, string>("~!-+a","(~(!(-(+a))))"));
            _expectations.Add(new Tuple<string, string>("a!!!","(((a!)!)!)"));

            // Unary and binary predecence.
            _expectations.Add(new Tuple<string, string>("-a * b","((-a) * b)"));
            _expectations.Add(new Tuple<string, string>("!a + b","((!a) + b)"));
            _expectations.Add(new Tuple<string, string>("~a ^ b","((~a) ^ b)"));
            _expectations.Add(new Tuple<string, string>("-a!","(-(a!))"));
            _expectations.Add(new Tuple<string, string>("!a!","(!(a!))"));

            // Binary precedence.
            _expectations.Add(new Tuple<string, string>("a = b + c * d ^ e - f / g","(a = ((b + (c * (d ^ e))) - (f / g)))"));

            // Binary associativity.
            _expectations.Add(new Tuple<string, string>("a = b = c","(a = (b = c))"));
            _expectations.Add(new Tuple<string, string>("a + b - c","((a + b) - c)"));
            _expectations.Add(new Tuple<string, string>("a * b / c","((a * b) / c)"));
            _expectations.Add(new Tuple<string, string>("a ^ b ^ c","(a ^ (b ^ c))"));

            // Conditional operator.
            _expectations.Add(new Tuple<string, string>("a ? b : c ? d : e","(a ? b : (c ? d : e))"));
            _expectations.Add(new Tuple<string, string>("a ? b ? c : d : e","(a ? (b ? c : d) : e)"));
            _expectations.Add(new Tuple<string, string>("a + b ? c * d : e / f","((a + b) ? (c * d) : (e / f))"));

            // Grouping.
            _expectations.Add(new Tuple<string, string>("a + (b + c) + d","((a + (b + c)) + d)"));
            _expectations.Add(new Tuple<string, string>("a ^ (b + c)","(a ^ (b + c))"));
            _expectations.Add(new Tuple<string, string>("(!a)!","((!a)!)"));
        }

        private readonly List<Tuple<string, string>>  _expectations = new List<Tuple<string, string>>();

        [TestMethod]
        public void TestMethod1()
        {
            Setup();
            foreach (var expectation in _expectations)
            {
                var source = expectation.Item2;
                var expected = expectation.Item1;

                var actual = Parse(source);

                Assert.AreEqual(expected, actual);
            }
    
        }

        private static string Parse(string source)
        {
            var lexer = new Lexer(source);
            Parser parser = new BantamParser(lexer);

            var result = parser.ParseExpression();
            var builder = new Builder();
            result.Print(builder);
            var actual = builder.ToString();
            return actual;
        }


    }
}
