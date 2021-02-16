using System;
using System.Collections.Generic;

namespace ThirdTask
{
    class ThirdTask
    {
        static void Main(string[] args)
        {
            string[] input = Console.In.ReadLine().Split(' ');

            short A = 0,
                  B = 0,
                  N = 0;

            if (input.Length > 2)
            {
                A = short.TryParse(input[0], out short result) ? result : throw new ArgumentException($"Cannot parse {nameof(A)}");
                B = short.TryParse(input[1], out result) ? result : throw new ArgumentException($"Cannot parse {nameof(B)}");
                N = short.TryParse(input[2], out result) ? result : throw new ArgumentException($"Cannot parse {nameof(N)}");
            }
            else
            {
                throw new ArgumentException($"Not enough parameters in {nameof(input)}");
            }

            Validation(A, B, N);

            short tempN = N,
                  numberOfBInSplit = 0,
                  partOfATermsInN = 0;

            List<short[]> tableWithSplits = new List<short[]>();

            for (short numberOfAInSplit = 0; partOfATermsInN < N; numberOfAInSplit++)
            {
                numberOfBInSplit = 0;
                tempN = N;

                partOfATermsInN = (short)(numberOfAInSplit * A);

                tempN -= partOfATermsInN;

                while (tempN > 0)
                {
                    tempN -= B;
                    numberOfBInSplit++;
                }

                if (tempN == 0)
                {
                    tableWithSplits.Add(new short[] { numberOfAInSplit, numberOfBInSplit });
                }
            }

            double maxResult = 0;
            double productOfNumbers = 0;

            if (tableWithSplits.Count > 0)
            {
                foreach (var line in tableWithSplits)
                {
                    productOfNumbers = Math.Pow(A, line[0]) * Math.Pow(B, line[1]);

                    if (maxResult < productOfNumbers)
                    {
                        maxResult = productOfNumbers;
                    };
                }
            }

            Console.Out.Write(maxResult);
        }

        private static void Validation(short A, short B, short N)
        {
            if (A <= 1)
            {
                throw new ArgumentException($"{nameof(A)} connot be equal or less than 1");
            }

            if (B >= 10)
            {
                throw new ArgumentException($"{nameof(B)} connot be equal or more than 10");
            }

            if (N <= 10)
            {
                throw new ArgumentException($"{nameof(N)} connot be equal or less than 10");
            }
            else if (N >= 100)
            {
                throw new ArgumentException($"{nameof(N)} connot be equal or more than 100");
            }
        }
    }
}
