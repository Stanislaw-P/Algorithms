using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class SubsequenceAlgorithm
    {
        List<int> x; List<int> y;
        int m = 0;
        int n = 0;
        int[,] lcs;
        (int, int)[,] prev;

        public SubsequenceAlgorithm(List<int> x, List<int> y)
        {
            this.x = x;
            m = x.Count();
            n = y.Count();
            lcs = new int[m, n];
            prev = new (int, int)[m, n];
        }

        // Тут можно убрать входные аргументы, тк это есть в конструкторе. Но для объяснения оставь!
        public void GetLCS(List<int> x, List<int> y)
        {
            m = x.Count();
            n = y.Count();
            lcs = new int[m, n];
            prev = new (int, int)[m, n];

            // Первая строка и столбец матрицы нули по опр НОП
            for (int i = 1; i < m; i++)
                lcs[i, 0] = 0;
            for (int j = 0; j < n; j++)
                lcs[0, j] = 0;
            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (x[i] == y[j])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                        prev[i, j] = (i - 1, j - 1);
                    }
                    else
                    {
                        if (lcs[i - 1, j] >= lcs[i, j - 1])
                        {
                            lcs[i, j] = lcs[i - 1, j];
                            prev[i, j] = (i - 1, j);
                        }
                        else
                        {
                            lcs[i, j] = lcs[i, j - 1];
                            prev[i, j] = (i, j - 1);
                        }
                    }
                }
            }
        }

        // Вывод LCS, вызывается как printLCS(m, n)
        public void printLCS(int i, int j)
        {
            if (i == 0 || j == 0) // Пришли к началу LCS
                return;
            if (prev[i, j] == (i - 1, j - 1)) // Если пришли в lcs[i, j] из lcs[i - 1, j - 1], то x[i] == y[j], надо вывести этот элемент
            {
                printLCS(i - 1, j - 1);
                Console.Write(x[i]);
            }
            else
            {
                if (prev[i, j] == (i - 1, j))
                    printLCS(i - 1, j);
                else
                    printLCS(i, j - 1);
            }
        }
    }
}
