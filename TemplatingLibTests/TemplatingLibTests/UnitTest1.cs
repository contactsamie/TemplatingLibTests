using System;
using System.Linq;
using System.Threading.Tasks;
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
        //        Parallel.ForEach(Enumerable.Range(0,100000),async (currentFile) =>
        //        {
        //            await  Task.Run(() => new BusyMan().FindPrimeNumber(1000000000));
        //        });
        //    }, iteration);
        //}
        [TestMethod]
        public void mustache()
        {
            Clock.Benchmark("mustache. Handlebars", () =>
            {
                //
            }, iteration);
        }
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
    }


}