using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TokenSplitting;

namespace TokenSplittingTests
{
    using static Assert;

    [TestClass]
    public class TokenSplittingTests
    {
        [TestMethod]
        public void LineSplit1()
        {
            var lsp = new LineSplitter(new Matcher(new[] { "" }, false), false);
            var x = lsp.SplitLine("!");
            var (value, column) = x.FirstOrDefault();
            AreEqual(0, column);
            AreEqual("!", value);
        }
        [TestMethod]
        public void LineSplitIncudeEmpty()
        {
            var lsp = new LineSplitter(new Matcher(new[] { "" }, false), true);
            var x = lsp.SplitLine(" a");
            var (value, column) = x.FirstOrDefault();
            AreEqual(0, column);
            AreEqual("", value);
        }
        [TestMethod]
        public void TokenSplitterLines()
        {
            // "",1,b,!,""
            var tokens = new TokenSplitter(new[] { "." }).Split("\n1\na\n!\n").ToArray();
            AreEqual(tokens[0].Column, 0);
            AreEqual(tokens[1].Column, 0);
            AreEqual(tokens[2].Column, 0);

            AreEqual(tokens[0].Line, 1);
            AreEqual(tokens[1].Line, 2);
            AreEqual(tokens[2].Line, 3);

            AreEqual(tokens[0].Value, "1");
            AreEqual(tokens[1].Value, "a");
            AreEqual(tokens[2].Value, "!");
        }
        [TestMethod]
        public void TokenSplitterAcceptsEmptyString()
        {
            AreEqual(0, split("").Count());
        }
        [TestMethod]
        public void TokenSplitterAsTokenSource()
        {
            var splitter = new TokenSplitter(new[] { "." });
            var tokens = spliter.Split(" a");
            var x = tokens.FirstOrDefault();
            AreEqual(1, x.Column);
            AreEqual(0, x.Line);
            AreEqual("a", x.Value);
        }
        ///<summary>
        /// SHow why Symbols2 doesn't work
        ///</summary>
        [TestMethod]
        public void Symbols()
        {
            var splitter = new TokenSplitter(new[]{
                  // "/*",  "*/", "*" , !Order Matter, means is wrong!
                    "/*", "*" , "*/"
            });
            var tokens = Values(splitter.Split(@"/***/")).ToArray();
            AreEquivalent(tokens, "/*", "*", "*/");
        }
        [TestMethod]
        public void Symbols2()
        {
            var splitter = new TokenSplitter(new[]{
                 ",",";",".", // delimiters
                    "(",")", "[","]","<",">","{","}", // grouping
                    "=",  //asignment
                    "+", "-", "*","/", "%",// operators                        
                    "!","?",":", // logical
                    "~","#","@","$", // modifiers
                    "\"","'","`", //quotes
                    "^","&","|", // binary & logical
                    // composite
                     "&&", "||",
                    "==", "!=", "=>",
                    "<>", ">=", "<=",
                    "/*", "*/",
                    "<-", "->"
            });
            var tokens = Values(splitter.Split(@"
                ,;.
                ()
                []
                <a>
                {}
                :=
                +-*/%
                !?
                ~#@$
                \""'`
                ^&|
                TODO
                && ||
                == != =>
                <> >= <=
                /***/
                -> <-
            ")).ToArray();
            AreEquivalent(tokens,
                    ",", ";", ".",
                    "(", ")",
                    "[", "]",
                    "<", "a", ">",
                    "{", "}", // grouping
                    ":", "=", //asignment
                    "+", "-", "*", "/", "%",// operators                        
                    "!", "?", // logical
                    "~", "#", "@", "$", // modifiers
                    "\"", "'", "`", //quotes
                    "^", "&", "|", // binary & logical
                    "TODO",
                    // composite
                    "&&", "||",
                    "==", "!=", "=>",
                    "<>", ">=", "<=",
                    "/*", "*", "*/",
                    "->", "<-"
                );
        }
        [TestMethod]
        public void CompositePostFix()
        {
            // use '+=' as  1 token or let the parser figure out +=            
            AreEqual("i,+,=,1",
                Joined(",")(Values(split("i+=1"))));
        }
        [TestMethod]
        public void BinaryVsLogical()
        {
            // & vs && ...and || vs | they are 
            AreEqual("a,|,1,&,&,b,&,2",
                Joined(",")(Values(split(
                    "a | 1 && b & 2"
                    ))));
        }
        [TestMethod]
        public void Comments()
        {
            AreEqual("-,-,a,/,*,?,*,/,/,/,b,<,!,-,x,-,>",
                Joined(",")(Values(split(
                    "--a /*?*/ \r //b <!-x->"
                    ))));
        }
        string[] operators = {
                    ",",";",".", // delimiters
                    "(",")", "[","]","<",">","{","}", // grouping
                    "=",  //asignment
                    "+", "-", "*","/", "%",// operators                        
                    "!","?",":", // logical
                    "~","#","@","$", // modifiers
                    "\"","'","`", //quotes
                    "^","&","|", // binary & logical
                };
        TokenSplitter spliter => new TokenSplitter(operators);

        bool AreEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            var e = expected is T[]? (T[])expected : expected.ToArray();
            var a = actual is T[]? (T[])actual : actual.ToArray();
            if (e.Length != a.Length) throw new Exception($"Expected {e.Length} length but found {a.Length} length");
            for (var i = 0; i < e.Length; i++)
            {
                AreEqual(e[i], a[i]);
            }
            return true;
        }
        bool AreEquivalent<T>(IEnumerable<T> actual, params T[] expected) => AreEquivalent(expected, actual);
        IList<TokenSource> split(string x) => spliter.Split(x);
        IEnumerable<string> Values(IList<TokenSource> input)
        {
            return input.Select(x => x.Value);
        }
        Func<IEnumerable<string>, string> Joined(string separator = "")
        {
            return input => input.Aggregate((a, b) => a + separator + b);
        }
        IList<T> Concat<T>(IList<T> a, params IList<T>[] param)
        {
            IList<T> output = new List<T>();
            foreach (var i in param)
            {
                foreach (var x in i)
                {
                    output.Add(x);
                }
            }
            return output;
        }
    }
}