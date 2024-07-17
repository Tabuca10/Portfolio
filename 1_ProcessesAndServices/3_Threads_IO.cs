using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsIO
{
    internal class Program
    {
        public static FileStream fst = new FileStream("cat.bmp", FileMode.Open);
        public static BinaryReader br = new BinaryReader(fst);

        static int posicionActual = 0;
        static long tamano;

        static bool acabado = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Leyendo archivo...");
            Thread.Sleep(3000);

            ThreadStart ts = new ThreadStart(leerArchivo);
            Thread t = new Thread(ts);
            t.Start();

            tamano = fst.Length;

            while (acabado != true)
            {
                double porcentaje = (double)(((double)posicionActual / (double)tamano) * 100.0);

                Console.WriteLine("Posicion actual: " + porcentaje + "%");
                Thread.Sleep(2000);
            }

            t.Join();

            Console.WriteLine("¡Archivo leído!");
            Console.ReadKey();
        }

        public static void leerArchivo()
        {
            for (int i = 0; i < tamano; i++)
            {
                br.ReadByte();
                posicionActual++;
                Thread.Sleep(1);
            }

            acabado = true;
        }
    }
}
