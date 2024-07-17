using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static Queue<string> inputQueue = new Queue<string>();
    static Queue<int> outputQueue = new Queue<int>();
    static object lockObject = new object();
    static string filePath = "data.txt";
    static List<string> lines = new List<string>();

    static void Main(string[] args)
    {
        ThreadStart outputThreadStart = new ThreadStart(OutputThread);
        Thread outputThread = new Thread(outputThreadStart);

        while (true)
        {
            string input = GetInput("Ingrese una cadena de texto (o 'continuar' para seguir): ");

            if (input.ToLower() == "continuar")
                break;

            lock (lockObject)
            {
                inputQueue.Enqueue(input);
            }
        }

        Console.WriteLine();
        outputThread.Start();
        outputThread.Join();
    }

    static void OutputThread()
    {
        while (true)
        {
            int lineNumber;
            string input;

            lock (lockObject)
            {
                input = GetInput("Ingrese el número de línea (o 'exit' para salir): ");

                if (input.ToLower() == "exit")
                    break;

                if (int.TryParse(input, out lineNumber))
                {
                    outputQueue.Enqueue(lineNumber);
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero.");
                }
            }

            lock (lockObject)
            {
                WriteToFile();
                ReadFromFile();
                ProcessQueues();
            }
        }
    }

    static string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    static void WriteToFile()
    {
        File.WriteAllLines(filePath, lines);
    }

    static void ReadFromFile()
    {
        lines = new List<string>(File.ReadAllLines(filePath));
    }

    static void ProcessQueues()
    {
        while (inputQueue.Count > 0)
        {
            string input = inputQueue.Dequeue();
            lines.Add(input);
        }

        while (outputQueue.Count > 0)
        {
            int lineNumber = outputQueue.Dequeue();
            if (lineNumber >= 1 && lineNumber <= lines.Count)
            {
                Console.WriteLine(lines[lineNumber - 1] + "\n");
            }
            else
            {
                Console.WriteLine($"La línea {lineNumber} no existe en el archivo.");
            }
        }
    }
}
