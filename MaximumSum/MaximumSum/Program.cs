using System;
using System.Linq;

namespace MaximumSum
{
    class Program
    {
        /// <summary>
        /// The ints to be used
        /// </summary>
        int[][] numsTree =
            {
              new int[] {215},
              new int[] {192, 124},
              new int[] {117, 269, 442},
              new int[] {218, 836, 347, 235},
              new int[] {320, 805, 522, 417, 345},
              new int[] {229, 601, 728, 835, 133, 124},
              new int[] {248, 202, 277, 433, 207, 263, 257},
              new int[] {359, 464, 504, 528, 516, 716, 871, 182},
              new int[] {461, 441, 426, 656, 863, 560, 380, 171, 923},
              new int[] {381, 348, 573, 533, 448, 632, 387, 176, 975, 449},
              new int[] {223, 711, 445, 645, 245, 543, 931, 532, 937, 541, 444},
              new int[] {330, 131, 333, 928, 376, 733, 017, 778, 839, 168, 197, 197},
              new int[] {131, 171, 522, 137, 217, 224, 291, 413, 528, 520, 227, 229, 928},
              new int[] {223, 626, 034, 683, 839, 052, 627, 310, 713, 999, 629, 817, 410, 121},
              new int[] {924, 622, 911, 233, 325, 139, 721, 218, 253, 223, 107, 233, 230, 124, 233},
            };

        /// <summary>
        /// The size of this Matrix/JaggedArray
        /// </summary>
        int n = 15;

        static void Main(string[] args)
        {
            Program p = new Program();

            p.PrintResult(p.MaxSumPath(p.numsTree));

            Console.ReadLine();
        }

        /// <summary>
        /// Finds the maximum sum path of a jagged array
        /// while taking into account interchanging parity
        /// (odd, even, odd, even)
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private int[] MaxSumPath(int[][] tree)
        {
            /// Create a dynamic programming jagged array
            /// in order to keep track of the algorithms values
            int[][] dp = new int[n][];
            for (int i = 0; i < dp.GetLength(0); i++)
            {
                dp[i] = new int[n];
            }

            /// Iterate from top to bottom and create
            /// a dynamic array for comparison
            for (int i = 0; i < tree.Length; i++)
            {
                for (int j = 0; j < tree[i].Length; j++)
                {
                    /// On top row, simply copy the values
                    if (i == 0)
                    {
                        dp[i][j] = tree[i][j];
                        continue;
                    }

                    /// We are on the left most value there is only
                    /// a parent directly above not diagonally
                    if (j == 0)
                    {
                        dp[i][j] = tree[i][j] + dp[i - 1][j];
                    }


                    else
                    {
                        /// Find the corresponding values in the 
                        /// dynamic programming jagged array
                        int topLeftParent = dp[i - 1][j - 1];
                        int directParent = dp[i - 1][j];


                        /// Test the values while taking into account 
                        /// interchanging parity, and increment the dynamic array
                        if (topLeftParent >= directParent && (tree[i - 1][j - 1].IsEven()) != (tree[i][j].IsEven()))
                        {
                            dp[i][j] = tree[i][j] + topLeftParent;
                            continue;
                        }
                        else if (directParent >= topLeftParent && (tree[i - 1][j].IsEven()) != (tree[i][j].IsEven()))
                        {
                            dp[i][j] = tree[i][j] + directParent;
                        }
                    }

                }
            }

            /// Find which path was taken by the algorithm
            int maxValue = dp[n - 1].Max();
            int start = Array.IndexOf(dp[n - 1], maxValue);

            /// instantiate and initialize the array
            /// that we will eventually return
            int[] path = new int[n];
            path[n - 1] = tree[n - 1][start];

            /// Iterate from bottom to top and compare the 
            /// values in the dynamic array to those of the 
            /// ones passed in as a parameter
            for (int i = n - 1; i > 0; i--)
            {
                for (int j = 0; j < tree[i].Length; j++)
                {
                    if (tree[i][j] == path[i])
                    {
                        /// there is only a direct parent
                        if (j == 0)
                        {
                            path[i - 1] = tree[i - 1][j];
                            continue;
                        }

                        /// create ints that represent the values
                        /// in the dynamic array
                        int left = dp[i - 1][j - 1];
                        int top = dp[i - 1][j];

                        /// compare those values and then set the resulatant
                        /// array to the values in the parameterized array
                        if (left > top)
                        {
                            path[i - 1] = tree[i - 1][j - 1];
                            continue;
                        }
                        else
                        {
                            path[i - 1] = tree[i - 1][j];
                        }
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// Prints the output in the requested format
        /// Max sum: sum
        /// Path: path
        /// </summary>
        /// <param name="result"></param>
        private void PrintResult(int[] result)
        {
            Console.WriteLine("Max sum: " + result.Sum());

            string tmp = "Path: {";

            foreach (int i in result)
            {
                tmp += i + ", ";
            }

            Console.WriteLine(tmp.Remove(tmp.Length - 2) + " }");
        }
    }
}

public static class Extensions
{
    /// <summary>
    /// Returns true if the int passed in is even
    /// </summary>
    /// <param name="value">int to evaluate</param>
    /// <returns></returns>
    public static bool IsEven(this int i)
    {
        return i % 2 == 0;
    }
}
