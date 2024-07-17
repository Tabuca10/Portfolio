using System;
using System.Collections.Generic;
using System.Threading;
/*
-Cread una lista de 1000 números aleatorios.
-Una vez creada, ordenad la del número más pequeño al más grande 
de la forma más rápida posible, usando dos o más Threads 
(No se puede usar las funciones Sort incluidas en C#).

-Una vez los Threads hayan acabado el Thread "Main" deberá mostrar la lista por consola.

[Nota]: Con lista me refiero a array, lista o queue, elegid la que os sea más cómoda.
*/

namespace Examen2_Ejercicio2
{
    class Program
    {
        static List<int> numeros = new List<int>();
        static int[] numerosOrdenados = new int[1000];
        static object lockObj = new object();

        static void Main(string[] args)
        {
            Random rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                numeros.Add(rand.Next(1, 1001));
            }

            ThreadStart ts1 = new ThreadStart(OrdenarThread1);
            Thread t1 = new Thread(ts1);
            t1.Start();

            ThreadStart ts2 = new ThreadStart(OrdenarThread2);
            Thread t2 = new Thread(ts2);
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("Lista ordenada:");
            foreach (int num in numerosOrdenados)
            {
                Console.WriteLine(num);
            }
            Console.ReadLine();
        }

        static void OrdenarThread1()
        {
            for (int i = 0; i < 500; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < 500; j++)
                {
                    if (numeros[j] < numeros[minIndex])
                    {
                        minIndex = j;
                    }
                }
                lock (lockObj)
                {
                    int temp = numeros[i];
                    numeros[i] = numeros[minIndex];
                    numeros[minIndex] = temp;
                }
            }
            for (int i = 0; i < 500; i++)
            {
                numerosOrdenados[i] = numeros[i];
            }
        }

        static void OrdenarThread2()
        {
            for (int i = 500; i < 1000; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < 1000; j++)
                {
                    if (numeros[j] < numeros[minIndex])
                    {
                        minIndex = j;
                    }
                }
                lock (lockObj)
                {
                    int temp = numeros[i];
                    numeros[i] = numeros[minIndex];
                    numeros[minIndex] = temp;
                }
            }
            for (int i = 500; i < 1000; i++)
            {
                numerosOrdenados[i] = numeros[i];
            }
        }
    }
}
