using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionSum
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please write size of array");
                int n = Convert.ToInt32(Console.ReadLine());
                if (n < 2)
                    throw new Exception("Invalid size");

                List<int> list = new List<int>();

                Console.WriteLine("Please write array items");

                for (int i = 0; i < n; i++)
                    list.Add(Convert.ToInt32(Console.ReadLine()));

                int[] mas = list.Distinct().OrderBy(i => i).ToArray();

                List<Tuple<int, int>> result = new List<Tuple<int, int>>();

                int targetSum;
                Console.WriteLine("Please write sum");
                targetSum = Convert.ToInt32(Console.ReadLine());

                int leftIndex = 0;
                int rightIndex = n - 1;

                bool findOnce = false;

                unsafe
                {
                    fixed (int* a = &mas[0])
                    {
                        while ((leftIndex != rightIndex) && (leftIndex <= (n - 1)) && (rightIndex >= 0))
                        {
                            int currentSum = a[leftIndex] + a[rightIndex];
                            if (currentSum < targetSum)
                                leftIndex++;
                            else if (currentSum > targetSum)
                                rightIndex--;
                            else
                            {
                                if (a[leftIndex] < a[rightIndex])
                                    result.Add(new Tuple<int, int>(a[leftIndex], a[rightIndex]));
                                else
                                    result.Add(new Tuple<int, int>(a[rightIndex], a[leftIndex]));

                                rightIndex--;
                                leftIndex++;

                                findOnce = true;
                            }
                        }

                        if (findOnce)
                            result.Distinct().OrderBy(i => i.Item1).ToList().ForEach(i => Console.WriteLine("Values: {0} + {1} = {2}", i.Item1, i.Item2, targetSum));
                        else
                            Console.WriteLine("Values not found");
                    }
                }
            }
            catch
            {
                // should be normal logs and exceptions there
                Console.WriteLine("Oops!");
            }
        }
    }
}


