using System;
using FuManchu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mustache;
using TemplatingLibTests.BenchMarkUtility;

namespace TemplatingLibTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly object context = new {world = "World"};
        private readonly int iteration = 100;

        private readonly string template =
            @"{{#if value}}
    True
{{/if}}

{{#if value}}
    True
{{else}}
    False
{{/if}}

{{#if value1}}
    Value 1
{{#elseif value2}}
    Value 2
{{else}}
   None
{{/if}}";

        [TestMethod]
        public void FuManchuTest_separate()
        {
            HandlebarTemplate compile = null;
            Clock.Benchmark("FuManchu. Handlebars compile", () =>
            {
                compile = Handlebars.Compile("<template-name>", template);
                compile(context);
            }, iteration);

            Clock.Benchmark("FuManchu. Handlebars run ", () => { compile(context); }, iteration);
        }

        [TestMethod]
        public void FuManchuTest_both()
        {
            Clock.Benchmark("FuManchu. Handlebars", () =>
            {
                var compile = Handlebars.Compile("<template-name>", template);
                compile(context);
            }, iteration);
        }


        [TestMethod]
        public void HandlebarsDotNet_separate()
        {
            Func<object, string> compile = null;
            Clock.Benchmark("HandlebarsDotNet. Handlebars compile", () =>
            {
                compile = HandlebarsDotNet.Handlebars.Compile(template);
                compile(context);
            }, iteration);

            Clock.Benchmark("HandlebarsDotNet. Handlebars run ", () => { compile(context); }, iteration);
        }

        [TestMethod]
        public void HandlebarsDotNet_both()
        {
            Clock.Benchmark("HandlebarsDotNet. Handlebars", () =>
            {
                var compile = HandlebarsDotNet.Handlebars.Compile(template);
                compile(context);
            }, iteration);
        }


        //[TestMethod]
        //public void busyman()
        //{
        //    Clock.BenchmarkCpu("busy man", () =>
        //    {
        //        new BusyMan().FindPrimeNumber(1000000);
        //    }, iteration);
        //}
        [TestMethod]
        public void mustache()
        {
            Clock.Benchmark("mustache. Handlebars", () =>
            {
                var compiler = new FormatCompiler();
                var generator = compiler.Compile(template);
                var result = generator.Render(context);
            }, iteration);
        }
    }

    public class BusyMan
    {
        public void Slow()
        {
            var nthPrime = FindPrimeNumber(1000); //set higher value for more time
        }

        public long FindPrimeNumber(int n)
        {
            var count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                var prime = 1; // to check if found a prime
                while (b*b <= a)
                {
                    if (a%b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                    count++;
                a++;
            }
            return --a;
        }
    }
}