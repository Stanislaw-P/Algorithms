using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Algorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>()
            {
                [1] = new List<int> { 2, 3 },
                [2] = new List<int> { 4, 5 },
                [3] = new List<int> { 6, 7 },
                [4] = null,
                [5] = new List<int> { 8 },
                [6] = null,
                [7] = null
            };
            //graph.GenerateGraph(8, 7);
            Console.WriteLine();
            #endregion
            var x = new List<int> { 2, 3, 5, 4 };
            var y = new List<int> { 1, 2, 3, 4 };
            SubsequenceAlgorithm algorithm = new SubsequenceAlgorithm(x, y);
            algorithm.GetLCS(x, y);
            algorithm.printLCS(x.Count()-1, y.Count()-1);

        }

        // Обзоды графа
        #region
        // Обход в ширину
        static void BFS(Dictionary<int, List<int>> graph, bool[] used, int node)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(node); // Кладем в очередь узел
            used[node] = true; // Помечаем узел как пройденный
            while (queue.Count != 0)
            {
                node = queue.Dequeue(); // Берем узел из очереди
                Console.Write(node + " "); // Выводим его
                if (DictIsNullOrEmty(graph, node)) // эту проверку можно убрать, она не нужна в процессе объяснения алгоритма
                    continue;
                foreach (var neighbour in graph[node]) // Пробегаемся по списку смежных вершин с вершиной node
                    if (!used[neighbour]) // Если не посещали вершину
                    {
                        used[neighbour] = true; // Помечаем ее как пройденную
                        queue.Enqueue(neighbour); // Добавляем в КОНЕЦ очереди пройденную вершину
                    }
            }
        }

        // Обход в глубину
        static void DFS(Dictionary<int, List<int>> graph, bool[] used, int node)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(node); // Кладем узел в стек
            used[node] = true; // Помечаем его как пройденный
            while (stack.Count != 0)
            {
                node = stack.Pop(); // Берем верхний узел из стека
                Console.Write(node + " "); // Выводим его
                if (DictIsNullOrEmty(graph, node)) // эту проверку можно убрать, она не нужна в процессе объяснения алгоритма
                    continue;
                foreach (var neighbour in graph[node]) // Пробегаемся по списку смежных вершин с вершиной node
                    if (!used[neighbour]) // Если не посещали вершину
                    {
                        used[neighbour] = true;  // Помечаем ее как пройденную
                        stack.Push(neighbour); // Добавляем в КОНЕЦ стека пройденную вершину
                    }
            }
        }

        // Необязетельный метод проверки словаря!!!
        static bool DictIsNullOrEmty(Dictionary<int, List<int>> graph, int node)
        {
            return !graph.ContainsKey(node) || graph[node] == null;
        }
        #endregion

        // Алгоритмы строк
        static int SubstringSearch(string text, string substring)
        {
            for (int i = 0; i <= text.Length - substring.Length; i++)
            {
                for (int j = 0; j < substring.Length; j++)
                {
                    if (substring[j] != text[i + j])
                        break;
                    if (j == substring.Length - 1)
                        return i;
                }
            }
            return -1;
        }

        // Префикс функция
        static int[] prefFunction(string text)
        {
            int[] pi = new int[text.Length];
            for (int i = 1; i < pi.Length; i++)
            {
                int j = pi[i - 1];
                while (j > 0 && text[j] != text[i])
                {
                    j = pi[j - 1];
                }
                if (text[i] == text[j])
                    j++;
                pi[i] = j;
            }
            return pi;
        }

        // КМП
        static int KMP(string text, string substring)
        {
            int[] pf = prefFunction(substring);
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                while (index > 0 && substring[index] != text[i]) // Пока индекс подстроки не равен нулю и символы не равны
                    index = pf[index - 1]; // Получаем индекс сдвига
                if (substring[index] == text[i])
                    index++;
                if (index == substring.Length)
                    return i - index + 1; // Индекс начала подстроки
            }
            return -1;
        }

        // Алгоритм Бойера-Мура
        //static int BM(string text, string substring)
        //{
        //    int n = substring.Length;
        //    int i = 0;
        //    int j = n - 1;
        //    char lastChar = text[n - 1];
        //    int[] sample = GetSample(substring);

        //    while (j + i < text.Length)
        //    {
        //        if (text[j + i] != substring[j] || j == 0)
        //        {
        //            int shiftIndex = -1; // Индекс сдвига
        //            for (int k = 0; k < n; k++)
        //            {
        //                if(lastChar == substring[k])
        //                {
        //                    shiftIndex = k;
        //                    break;
        //                }
        //            }
        //            if (shiftIndex == -1)
        //                i += n;
        //            else
        //                i += sample[shiftIndex];
        //            j = n - 1;
        //            lastChar = text[j + i];
        //        }
        //        else
        //            j--;
        //    }

        //    return i;
        //}

        // Построение таблицы для Бойера-Мура
        static int[] GetSample(string substring)
        {
            int n = substring.Length;
            int[] sample = new int[n];
            for (int i = n - 2; i > -1; i--)
            {
                sample[i] = n - i - 1;
                for (int j = n - 2; j > i; j--)
                {
                    if (substring[i] == substring[j])
                    {
                        sample[i] = sample[j];
                        break;
                    }
                }
            }
            sample[n - 1] = n;
            for (int i = n - 2; i > -1; i--)
            {
                if (substring[n - 1] == substring[i])
                {
                    sample[n - 1] = sample[i];
                    break;
                }
            }
            return sample;
        }
    }
}