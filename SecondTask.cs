using System;
using System.Linq;

namespace SecondTask
{
    class SecondTask
    {
        // The idea is to make liner equation and solve it.
        static void Main(string[] args)
        {
            string allInput = Console.In.ReadToEnd();

            string[] input = allInput.Split(' ', '\n');

            short N = 0,
                  M = 0;

            if (input.Length > 1)
            {
                N = short.TryParse(input[0], out short result) ? result : throw new ArgumentException($"Cannot parse {nameof(N)}");
                M = short.TryParse(input[1], out result) ? result : throw new ArgumentException($"Cannot parse {nameof(M)}");
            }
            else
            {
                throw new ArgumentException($"Not enough parameters in {nameof(input)}");
            }

            NAndMValidation(N, M);

            short nubmerOfParametersfirstLine = 2;

            // tyreWear array is "a" array from the task 
            short[] tyreWear = new short[N];

            if (input.Length - nubmerOfParametersfirstLine == N)
            {
                for (short i = 2, j = 0; i < input.Length; i++, j++)
                {
                    tyreWear[j] = short.TryParse(input[i], out short result) ? result : throw new ArgumentException($"Cannot parse tyre wear of wheel number {j + 1}");
                }
            }
            else
            {
                throw new ArgumentException($"Not enough parameters in {nameof(input)}");
            }

            TyreWearValidation(tyreWear);

            float[] numerators = new float[tyreWear.Length],
                    denominator = new float[tyreWear.Length];
            float gcd = 1,
                  lcm = 1;

            Array.Copy(tyreWear, denominator, tyreWear.Length);

            for (int i = 0; i < numerators.Length; i++)
            {
                numerators[i] = 1;
            }

            for (short i = 0; i < N - 1; i++)
            {
                gcd = GetGcdByStein((int)denominator[i], (int)denominator[i + 1]);
                lcm = (denominator[i] * denominator[i + 1]) / gcd;

                for (short j = 0; j < i + 2; j++)
                {
                    if (denominator[j] < lcm)
                    {
                        numerators[j] = (lcm / denominator[j]) * numerators[j];
                        denominator[j] = lcm;
                    }
                }
            }

            float maxKilometers = M * lcm / numerators.Sum();

            Console.Out.Write(maxKilometers.ToString("0.000"));
        }

        private static int GetGcdByStein(int firstNumber, int secondNumber)
        {
            firstNumber = Math.Abs(firstNumber);
            secondNumber = Math.Abs(secondNumber);

            if (firstNumber == secondNumber)
            {
                return firstNumber;
            }

            if (firstNumber == 0)
            {
                return secondNumber;
            }

            if (secondNumber == 0)
            {
                return firstNumber;
            }

            if ((~firstNumber & 1) != 0)
            {
                if ((secondNumber & 1) != 0)
                {
                    return GetGcdByStein(firstNumber >> 1, secondNumber);
                }
                else
                {
                    return GetGcdByStein(firstNumber >> 1, secondNumber >> 1) << 1;
                }
            }

            if ((~secondNumber & 1) != 0)
            {
                return GetGcdByStein(firstNumber, secondNumber >> 1);
            }

            if (firstNumber > secondNumber)
            {
                return GetGcdByStein((firstNumber - secondNumber) >> 1, secondNumber);
            }

            return GetGcdByStein((secondNumber - firstNumber) >> 1, firstNumber);
        }

        private static void NAndMValidation(short N, short M)
        {
            if (N < 4)
            {
                throw new ArgumentException($"{nameof(N)} connot be less than 4");
            }
            else if (N > 10)
            {
                throw new ArgumentException($"{nameof(N)} connot be greater than 10");
            }

            if (N % 2 != 0)
            {
                throw new ArgumentException($"{nameof(N)} must be even");
            }

            if (M < N)
            {
                throw new ArgumentException($"{nameof(M)} connot be less than {nameof(N)}");
            }
            else if (M > 20)
            {
                throw new ArgumentException($"{nameof(M)} connot be greater than 20");
            }
        }

        private static void TyreWearValidation(short[] tyreWear)
        {
            for (int i = 0; i < tyreWear.Length; i++)
            {
                if (tyreWear[i] <= 0)
                {
                    throw new ArgumentException($"Tyre limit of wheel number {i+1} connot be less or equals 0");
                }
                else if(tyreWear[i] > 3000)
                {
                    throw new ArgumentException($"Tyre limit of wheel number {i + 1} connot be greater than 3000");
                }
            }
        }

    }
}
