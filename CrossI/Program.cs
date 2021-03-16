using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrossI
{
    class Program
    {
        static void Main()
        {
            string textPath = @"..\..\..\text.txt"; // some text with spaces
            Stopwatch stopwatch = Stopwatch.StartNew();
            string[] splitedArr = File.ReadAllText(textPath).ToLower()
                        .Split(" "); // В задании нет информации о форматировании исходных данных.

            ConcurrentDictionary<string, int> tripletsDict = TripletsAddOrUpdateToDict(splitedArr);
            int counter = 1;
            foreach (var (key, value) in tripletsDict.OrderByDescending(x => x.Value))
            {
                //Console.Write($"{key} - {value}, ");
                Console.Write($"{key}, ");
                if (counter > 9)
                    break;
                counter++;
            }

            stopwatch.Stop();
            Console.WriteLine($"\n{stopwatch.ElapsedMilliseconds} мс");
            Console.ReadKey();
        }

        private static ConcurrentDictionary<string, int> TripletsAddOrUpdateToDict(string[] splitedArr)
        {
            ConcurrentDictionary<string, int> tripletsDict = new();
            //Parallel.ForEach(splitedArr, word => //new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount }
            splitedArr.AsParallel().ForAll( word =>
            {
                for (int i = 2; i < word.Length; i++)
                {
                    string triplet = new string(new char[] { word[i - 2], word[i - 1], word[i] });
                    tripletsDict.AddOrUpdate(triplet, 1, (key, oldValue) => oldValue + 1);
                }
            });
            return tripletsDict;
        }

    }
}
