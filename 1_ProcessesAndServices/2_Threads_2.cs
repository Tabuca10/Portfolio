using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static int[] valores = new int[10000];

        static void SecondaryThread()
        {
            for(int i = 0; i < 4999; i++)
            {
                if (EsPrimo(i))
                {
                    valores[i] = i;
                }
            }

        }

        static void Main(string[] args)
        {
            ThreadStart ts = new ThreadStart(SecondaryThread);
            Thread thread = new Thread(ts);
            thread.Start();

            for (int i = 0; i < 10000; i++)
            {
                valores[i] = 0;
            }

            for (int i = 5000; i < 10000; i++)
            {
                if (EsPrimo(i))
                {
                    valores[i] = i;
                }
            }

            thread.Join();

            foreach (var i in valores)
            {
                if (valores[i] != 0)
                {
                    Console.WriteLine(valores[i]);
                }
            }

            Console.ReadLine();
        }

        static bool EsPrimo(int numero)
        {
            if (numero < 2)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(numero); i++)
            {
                if (numero % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
