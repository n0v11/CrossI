using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CrossI
{
    class Program
    {
        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string path = @"..\..\..\text.txt";
            string[] splitedArr = File.ReadAllText(path).ToLower().Split(" "); // В задании не указано в каком регистре и чем разделены слова.
            Dictionary<string, int> tripletsDict = new Dictionary<string, int>();

            if (splitedArr.Length > 2)
            {
                for (int i = 0; i < splitedArr.Length; i++)
                {
                    string word = splitedArr[i];
                    for (int m = 2; m < word.Length; m++)
                    {
                        string triplet = new (new[] {word[m - 2], word[m - 1], word[m]});
                        if (!tripletsDict.ContainsKey(triplet))
                        {
                            tripletsDict.Add(triplet, 1);
                        }
                        else
                        {
                            tripletsDict[triplet]++;
                        }
                    }
                }
            }

            tripletsDict = tripletsDict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (var (key, value) in tripletsDict)
            {
                Console.Write($"{key} - {value}, ");
            }
            Console.WriteLine("\n" + stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();
        }
    }
}
