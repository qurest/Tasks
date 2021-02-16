using System;

namespace FourthTask
{
    class FourthTask
    {
        // The regularity was found: (x - M) / N = M + 1 where x -- minimum number of permutations
        static void Main(string[] args)
        {
            string[] input = Console.In.ReadLine().Split(' ', '\n');

            int N = 0,
                M = 0,
                result = 0;

            if (input.Length > 1)
            {
                N = int.TryParse(input[0], out result) ? result : throw new ArgumentException($"Cannot parse {nameof(N)}");
                M = int.TryParse(input[1], out result) ? result : throw new ArgumentException($"Cannot parse {nameof(M)}");
            }
            else
            {
                throw new ArgumentException($"Not enough parameters in {nameof(input)}");
            }

            if (N < 1)
            {
                throw new ArgumentException($"{nameof(N)} connot be less than 1");
            }

            if (M > 1000)
            {
                throw new ArgumentException($"{nameof(M)} connot be greater than 1000");
            }

            result = (M + 1) * N + M;

            Console.Out.Write(result);
        }
    }
}
