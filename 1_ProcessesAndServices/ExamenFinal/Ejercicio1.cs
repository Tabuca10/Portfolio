using System;
using System.Collections.Generic;
using System.Threading;

/*
-Utiliza la clase Random para generar 1000 números aleatorios entre 2 
Threads diferentes (500 un Thread y 500 el otro) teniendo en cuenta que 
si el número generado no es divisible por dos, tienes que generar otro de nuevo.

-Cada número generado correctamente se tiene que guardar en una Queue, 
todos los accesos a la clase Random se tienen que realizar a 
través de un Mutex o un Monitor.

-Un tercer Thread leer los números de la Queue y los mostrará por pantalla.

-Una vez todos los Threads hayan acabado sus respectivas tareas, 
el Main deberá mostrar un mensaje mostrando que han acabado.
*/

class Program
{
    static Queue<int> numerosQueue = new Queue<int>();
    static Mutex mutex = new Mutex();
    static Random random = new Random();

    static void Main(string[] args)
    {
        ThreadStart ts2 = new ThreadStart(CrearNumeros);
        ThreadStart ts3 = new ThreadStart(ProcesarNumeros);

        Thread t2 = new Thread(ts2);
        Thread t3 = new Thread(ts3);

        t2.Start();
        t3.Start();

        Program.CrearNumeros();

        t2.Join();
        t3.Join();

        Console.WriteLine("Todos los threads han acabado.");
    }

    static void CrearNumeros()
    {
        for (int i = 0; i < 500; i++)
        {
            int number;
            do
            {
                number = GenerarNumeroRandom();
            } while (number % 2 != 0);

            mutex.WaitOne();
            numerosQueue.Enqueue(number);
            mutex.ReleaseMutex();
        }
    }

    static int GenerarNumeroRandom()
    {
        mutex.WaitOne();
        int numero = random.Next();
        mutex.ReleaseMutex();
        return numero;
    }

    static void ProcesarNumeros()
    {
        while (true)
        {
            mutex.WaitOne();
            if (numerosQueue.Count > 0)
            {
                int numero = numerosQueue.Dequeue();
                Console.WriteLine($"Número leído: {numero}");
            }
            else
            {
                mutex.ReleaseMutex();
                Thread.Sleep(100);
                continue;
            }
            mutex.ReleaseMutex();
        }
    }
}
