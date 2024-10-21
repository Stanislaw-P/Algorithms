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

        public SubsequenceAlgorithm() { }
        public SubsequenceAlgorithm(List<int> x, List<int> y)
        {
            this.x = x;
            m = x.Count();
            n = y.Count();
            lcs = new int[m, n];
            prev = new (int, int)[m, n];
        }

        // Тут можно убрать входные аргументы, тк это есть в конструкторе. Но для объяснения оставь!
        public int GetLCS(List<int> x, List<int> y)
        {
            m = x.Count();
            n = y.Count();
            lcs = new int[m+1, n+1];
            prev = new (int, int)[m+1, n+1];

            // Первая строка и столбец матрицы нули по опр НОП
            for (int i = 1; i <= m; i++)
                lcs[i, 0] = 0;
            for (int j = 0; j <= n; j++)
                lcs[0, j] = 0;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (x[i-1] == y[j-1])
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
            return lcs[m, n]; // Длина наибольшей общей подпоследовательности
        }

        // Вывод LCS, вызывается как printLCS(m, n)
        public void printLCS(int i, int j)
        {
            if (i == 0 || j == 0) // Пришли к началу LCS
                return;
            if (prev[i, j] == (i - 1, j - 1)) // Если пришли в lcs[i, j] из lcs[i - 1, j - 1], то x[i] == y[j], надо вывести этот элемент
            {
                printLCS(i - 1, j - 1);
                Console.Write(x[i-1] + " ");
            }
            else
            {
                if (prev[i, j] == (i - 1, j))
                    printLCS(i - 1, j);
                else
                    printLCS(i, j - 1);
            }
        }

        // НВП
        public List<int> findLIS(List<int> a)
        {
            int n = a.Count(); // Размер исходной последовательности
            int[] prev = new int[n]; // Для восстановления ответа заведем массив prev[0...n−1],
                                     // где prev[i]
                                     // будет означать индекс в массиве a[],
                                     // при котором достигалось наибольшее значение d[i].
                                     // Для вывода ответа будем идти от элемента с максимальным значениям d[i]
                                     // по его предкам.
            int[] L = new int[n];

            for (int i = 0; i < n; i++) // Тут идем до n - 1 
            {
                L[i] = 1;
                prev[i] = -1;
                for (int j = 0; j < i; j++) // Тут идем до i - 1 как и в алгоритме
                {
                    if (a[j] < a[i] && L[j] + 1 > L[i])
                    {
                        L[i] = L[j] + 1;
                        prev[i] = j;
                    }
                }
            }
            int pos = 0; // индекс последнего элемента НВП
            int length = L[0]; // Длина НВП
            for (int i = 0; i < n; i++)
            {
                if (L[i] > length)
                {
                    pos = i;
                    length = L[i];
                }
            }

            // Восстановление ответа
            List<int> answer = new List<int>();
            while (pos != -1)
            {
                answer.Add(a[pos]);
                pos = prev[pos];
            }
            answer.Reverse(); // Изменить порядок элементов на обратный
            return answer;
        }
    }
}