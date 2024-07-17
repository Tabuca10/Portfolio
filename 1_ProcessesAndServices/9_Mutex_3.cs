using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    private static List<string> filesToDisplay = new List<string>();
    private static Mutex mutex = new Mutex();
    private static bool funcionando = true;

    static void Main(string[] args)
    {
        ThreadStart ts = new ThreadStart(MostrarArchivos);
        Thread mostrarThread = new Thread(ts);
        mostrarThread.Start();

        Console.WriteLine("Introduce el directorio de la carpeta o no pongas nada en caso que quieras salir: ");

        while (funcionando)
        {
            string path = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                funcionando = false;
                break;
            }
            else if(!string.IsNullOrWhiteSpace(path))
            {
                ProcesarDirectorio(path);
            }
        }

        mostrarThread.Join();
        Console.WriteLine("Programa finalizado.");
    }

    static void ProcesarDirectorio(string path)
    {
        if (!Directory.Exists(path))
        {
            Console.WriteLine("El directorio no se ha encontrado: " + path);
            return;
        }
        else if (Directory.Exists(path)) {

            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                mutex.WaitOne();
                filesToDisplay.Add(file);
                mutex.ReleaseMutex();
            }
        }
    }

    static void MostrarArchivos()
    {
        while (funcionando || filesToDisplay.Count > 0)
        {
            mutex.WaitOne();
            if (filesToDisplay.Count > 0)
            {
                Console.WriteLine(filesToDisplay[0]);
                filesToDisplay.RemoveAt(0);
            }
            mutex.ReleaseMutex();
            Thread.Sleep(1000);
        }
    }
}
