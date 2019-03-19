using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativeCodeChallenge
{
    public class NumberPrinter
    {
        public void PrintNumbers(List<int> numbers)
        {
            var reverseList = numbers.Reverse<int>();
            var uniqueThreadAndNumberCollection = new ConcurrentDictionary<int, string>();
            var task1 = PrintNumbersAsync(numbers, "t1", 500, uniqueThreadAndNumberCollection);
            var task2 = PrintNumbersAsync(reverseList, "t2", 1000, uniqueThreadAndNumberCollection);
            task1.Wait();
            task2.Wait();

            //here there is assumption that the number will be only once in list. So for example if the list is 1,2,1,3,4 then 1 will be only comes once and which thread executed it.
            Console.WriteLine("Threads who printed number first");
            foreach (var keyValPair in uniqueThreadAndNumberCollection)
            {
                Console.WriteLine(keyValPair.Key + " : " + keyValPair.Value);
            }

        }

        private async Task PrintNumbersAsync(IEnumerable<int> numbers, string threadName, int delay, ConcurrentDictionary<int, string> dictionary)
        {
            await Task.Run(() =>
            {
                foreach (var number in numbers)
                {
                    Console.WriteLine(threadName + " : " + number);
                    if (!dictionary.ContainsKey(number))
                        dictionary.TryAdd(number, threadName);
                    Thread.Sleep(delay);
                }
            });
        }
    }
}
