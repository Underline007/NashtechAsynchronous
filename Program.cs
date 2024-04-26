using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NashtechAsynchronousDay2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int startNumber;
            int endNumber;
            Console.Write("Enter Start Number: ");
            while (!int.TryParse(Console.ReadLine(), out startNumber))
            {
                Console.WriteLine("Invalid");
                Console.Write("Enter the start number again: ");
            }

            Console.Write("Enter End Number: ");
            while (!int.TryParse(Console.ReadLine(), out endNumber) || endNumber < startNumber)
            {
                if (endNumber < startNumber)
                {
                    Console.WriteLine("End number should be greater than or equal to start number.");
                }
                else
                {
                    Console.WriteLine("Invalid");
                }
                Console.Write("Enter the end number again: ");
            }

            Console.WriteLine($"Prime numbers between {startNumber} and {endNumber}:");
            // Performance of Sync Method
            Stopwatch timeConsumingOfSyncMethod = Stopwatch.StartNew();
            int[] primeNumbersSync = GetPrimeNumbers(startNumber, endNumber);
            timeConsumingOfSyncMethod.Stop();
            Console.WriteLine($"Time of Sync method: {timeConsumingOfSyncMethod.ElapsedMilliseconds}ms");
            // Performance of Async Method
            Stopwatch timeConsumingOfAsyncMethod = Stopwatch.StartNew();
            int[] primeNumbersAsync = await GetPrimeNumbersAsync(startNumber, endNumber);
            timeConsumingOfAsyncMethod.Stop();
            Console.WriteLine("Asynchronous method:");
            Console.WriteLine($"Time of Async method: {timeConsumingOfAsyncMethod.ElapsedMilliseconds}ms");
            // Display Result
            foreach (int prime in primeNumbersAsync)
            {
                Console.Write(prime+", ");
            }
        }
        // Async Method
        static async Task<int[]> GetPrimeNumbersAsync(int start, int end)
        {
            const int range = 1000; 
            List<Task<int[]>> tasks = new List<Task<int[]>>();

            for (int i = start; i <= end; i += range)
            {
                int startRange = i;
                int endRange = Math.Min(i + range - 1, end);

                tasks.Add(Task.Run(() => CheckPrimeNumbersInRange(startRange, endRange)));
            }
            await Task.WhenAll(tasks);
            return tasks.SelectMany(t => t.Result).ToArray();
        }
        static int[] CheckPrimeNumbersInRange(int start, int end)
        {
            return Enumerable.Range(start, end - start + 1)
                             .Where(IsPrime)
                             .ToArray();
        }
        // Sync Method
        static int[] GetPrimeNumbers(int start, int end)
        {
            return Enumerable.Range(start, end - start + 1)
                .Where(IsPrime)
                .ToArray();
        }

        static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
