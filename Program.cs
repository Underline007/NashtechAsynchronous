using System;
using System.Threading.Tasks;

namespace NashtechAsynchronousDay2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int StartNumber;
            int EndNumber;

 
            Console.Write("Enter Start Number: ");
            while (!int.TryParse(Console.ReadLine(), out StartNumber))
            {
                Console.WriteLine("Invalid");
                Console.Write("Enter the start number agian: ");
            }

          
            Console.Write("Enter End Number: ");
            while (!int.TryParse(Console.ReadLine(), out EndNumber))
            {
                Console.WriteLine("Invalid");
                Console.Write("Enter the end number agian: ");
            }

            Console.WriteLine($"Prime numbers between {StartNumber} and {EndNumber}:");

            int[] primeNumbers = await GetPrimeNumbersAsync(StartNumber, EndNumber);

            foreach (int prime in primeNumbers)
            {
                Console.WriteLine(prime);
            }
        }

        static async Task<int[]> GetPrimeNumbersAsync(int start, int end)
        {
            return await Task.Run(() =>
            {
                List<int> primesList = new List<int>();

                for (int number = start; number <= end; number++)
                {
                    if (IsPrime(number))
                    {
                        primesList.Add(number);
                    }
                }
                return primesList.ToArray();
            });
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
