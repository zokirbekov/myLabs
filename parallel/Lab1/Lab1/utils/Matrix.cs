using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.utils
{
    public static class Matrix
    {
        public static int[][] CreateMatrixOnes(int n, int m)
        {
            var matrix = new int[n][];

            for (int i = 0; i < n; i++)
            {
                matrix[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    matrix[i][j] = 1;
                }
            }

            return matrix;
        }
    }
}
