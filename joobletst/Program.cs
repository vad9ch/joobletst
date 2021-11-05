using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joobletst
{
    class Program
    {
        public static Random rnd = new Random();
        static void Main(string[] args)
        {
            var path1 = @"Files\de-test-words.txt";
            var path2 = @"Files\de-results.txt"; 
            var path3 = @"Files\de-dictionary.txt";

            List<string> testWords = new List<string>();


            List<string> testWordsText = File.ReadAllLines(path1).ToList();
            List<string> results = File.ReadAllLines(path2).ToList();
            List<string> dict = File.ReadAllLines(path3).ToList();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var res = GetRes(testWordsText, dict);
            stopwatch.Stop();
            foreach(var i in res)
            {
                results.Add(i);
                //Console.WriteLine(i);
            }
            System.IO.File.WriteAllLines($"{path2}", results);
            Console.WriteLine($"Не найдено в словаре - {res.Where(x => x.Contains("Не разбивается")).Count()}");
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }

        public static IEnumerable<string> GetRes(IEnumerable<string> test , IEnumerable<string> dict)
        {
            var newDict = dict.AsParallel().Select(x => x.ToLower());
            var sorted = newDict.OrderByDescending(x => x.Length).ToList();
            ConcurrentBag<string> conc = new ConcurrentBag<string>();
            Parallel.ForEach(test, (item) =>
            {
                var ctn = new Contains();
                var res = ctn.ContainsS(item, sorted);
                string st = String.Join("-", res);
                conc.Add(st);
            });
            return conc;
        }
    }
}
