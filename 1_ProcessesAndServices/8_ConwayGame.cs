using System;
using System.Threading;

namespace Tasca_Nadal
{
    class Program
    {
        private static bool[,] grid;
        private static bool[,] nextGrid;
        private static int gridSize;
        private static Mutex mutex = new Mutex();
        private static bool automaticMode;

        static void Main(string[] args)
        {
            Console.WriteLine("Elige el modo de juego (escribe 'automatico' o 'manual'):");
            string mode = Console.ReadLine();
            automaticMode = mode.Equals("automatico");

            Console.WriteLine("Introduce el tamaño de la cuadrícula:");
            gridSize = int.Parse(Console.ReadLine());

            InitializeGrid();

            while (true)
            {
                PrintGrid();
                UpdateGrid();

                if (!automaticMode)
                {
                    Console.WriteLine("Presiona espacio para continuar...");
                    while (Console.ReadKey().Key != ConsoleKey.Spacebar) ;
                }
                else
                {
                    Thread.Sleep(20);
                }

            }
        }

        static void InitializeGrid()
        {
            grid = new bool[gridSize, gridSize];
            nextGrid = new bool[gridSize, gridSize];
            Random random = new Random();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = random.Next(2) == 0;
                }
            }
        }

        static void PrintGrid() //Mostro l'imatge per consola
        {
            Console.Clear();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j] ? "█" : " ");
                }
                Console.WriteLine();
            }
        }

        static void UpdateGrid()
        {
            Thread[] threads = new Thread[gridSize];

            for (int i = 0; i < gridSize; i++) //Recorro totes les files
            {
                int row = i;
                ThreadStart threadStart = () => UpdateRow(row); //Aquí utilitzo una expresió lambda per a utilitzar un ThreadStart per a cada fila 'row'.
                threads[i] = new Thread(threadStart);
                threads[i].Start();
            }

            // Espera a que els threads finalitzin
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            (grid, nextGrid) = (nextGrid, grid); //Actualitza l'informació del grid per a poder printar després.
        }

        static void UpdateRow(int row) //Utilitzo 'mutex' per assegurarme que cada thread que actualitza una fila no interfereixi amb els altres threads.
        {
            for (int col = 0; col < gridSize; col++)
            {
                int liveNeighbors = CountLiveNeighbors(row, col);

                mutex.WaitOne();
                if (grid[row, col])
                {
                    if (liveNeighbors == 2 || liveNeighbors == 3)
                    {
                        nextGrid[row, col] = true;
                    }
                    else
                    {
                        nextGrid[row, col] = false;
                    }
                }
                else
                {
                    if (liveNeighbors == 3)
                    {
                        nextGrid[row, col] = true;
                    }
                    else
                    {
                        nextGrid[row, col] = false;
                    }
                }
                mutex.ReleaseMutex();
            }
        }

        static int CountLiveNeighbors(int row, int col) //Calcula les cél·lules veïnes.
        {
            int liveNeighbors = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int newRow = (row + i + gridSize) % gridSize;
                    int newCol = (col + j + gridSize) % gridSize;

                    if (grid[newRow, newCol])
                    {
                        liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }
    }
}
