using System;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PerformanceCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();
            string s1 = "qwe";
            Console.WriteLine($"string {watch.ElapsedTicks}");
            watch.Stop();

            var summary = BenchmarkRunner.Run<PerformanceCompare>();
            Console.ReadLine();
        }
    }
    [MemoryDiagnoser]
    public class PerformanceCompare
    {
        List<int> list = new List<int>();
        public PerformanceCompare()
        {
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }

        }

        [Benchmark]
        public void ForPerformance()
        {
            var forList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                forList.Add(i);
            }
        }
        [Benchmark]
        public void ForeachPerformance()
        {
            var forEachList = new List<int>();
            foreach (int item in list)
            {
                forEachList.Add(item);
            }
        }
        [Benchmark]
        public void ArrayPerformance()
        {
            int[] array = new int[1000];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }
        [Benchmark]
        public void ListPerformance()
        {
            List<int> list2 = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                list2.Add(list[i]);
            }
        }
        [Benchmark]
        public void StringPerformance()
        {
            string s1 = "qwe";
            for (int i = 0; i < 1000; i++)
            {
                s1 += "rty";
            }
        }
        [Benchmark]
        public void StringBuilderPerformance()
        {
            StringBuilder s2 = new StringBuilder("qwe");
            s2.Append("rty");
        }
        [Benchmark]
        public void ClassPerformance()
        {
            ClassProduct classProduct = new ClassProduct(20, "Mobile");
        }
        [Benchmark]
        public void StructPerformance()
        {
            StructProduct structProduct = new StructProduct(40, "Fridge");
        }
        [Benchmark]
        public void ThreadPerformance()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(Work);
                thread.Start();
                thread.Join();
            }
        }
        [Benchmark]
        public void TaskPerformance()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Run(async () =>
                {
                    Work();
                }
                    ).GetAwaiter().GetResult();
            }
        }
        public void Work()
        {
            int total = 0;
            for (int i = 0; i < 1000; i++)
            {
                total += i;
            }
        }
    }
    public class ClassProduct
    {
        double price;
        string name;
        public ClassProduct(double price, string name)
        {
            this.price = price;
            this.name = name;
        }
    }
    public struct StructProduct
    {
        double price;
        string name;
        public StructProduct(double price, string name)
        {
            this.price = price;
            this.name = name;
        }
    }
}
