using System;

namespace task8UP
{
    class Program
    {
        public static int n = 4; // chislo vershin v grafe
        public static int timer;
        public static int[] tin = new int[n];
        public static int[] fup = new int[n];
        public static bool[] used = new bool[n];
        public static int[,] matrSmej = new int[n, n];
        public static int[,] matrIn = {
                {0,1,0,1},
                {1,0,0,0},
                {1,0,1,1},
                {0,1,1,0}
              };
        static void Main(string[] args)
        {
            MatrPreob();
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write(matrIn[i, j] + "  ");
                }
                Console.WriteLine();
            }
            find_bridges();
            Console.ReadKey();
        }
        public static void MatrPreob()
        {
            int k1, k2;
            for (int i = 0; i < matrIn.GetLength(1); i++)
            {
                k1 = 0;
                k2 = 0;
                for (int j = 0; j < matrIn.GetLength(0); j++)
                    if (matrIn[j, i] != 0)
                    {
                        if (k1 == 0) k1 = j;
                        else { k2 = j; }
                    }
                matrSmej[k1, k2] = 1;
                matrSmej[k2, k1] = 1;
            }
        }
        public static void dfs(int v, int p = -1) // poisk v glubinu
        {
            used[v] = true;
            tin[v] = fup[v] = timer++;
            for (int i = 0; i < n; ++i)
            {
                if (matrSmej[v, i] == 1)
                {
                    int to = i;
                    if (to == p) continue;
                    if (used[to]) fup[v] = Math.Min(fup[v], tin[to]);
                    else
                    {
                        dfs(to, v);
                        fup[v] = Math.Min(fup[v], fup[to]);
                        if (fup[to] > tin[v]) Console.WriteLine($"Мост: ( {v} , {to}  )");
                    }
                }
            }
        }
        public static void find_bridges() // poisk mostov
        {
            timer = 0;
            for (int i = 0; i < n; ++i)
            {
                used[i] = false;
            }
            for (int i = 0; i < n; ++i)
            {
                if (!used[i]) dfs(i);
            }
        }
    }
}