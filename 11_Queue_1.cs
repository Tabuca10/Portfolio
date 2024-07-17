using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    // Cola para almacenar las tareas
    static Queue<string> strings = new Queue<string>();

    // Evento para indicar que hay una nueva tarea
    static AutoResetEvent nuevaTarea = new AutoResetEvent(false);

    // Hilo que se encarga de procesar las tareas
    static void Thread()
    {
        while (true)
        {
            // Esperar a que haya un nuevo string
            nuevaTarea.WaitOne();

            // Obtener el siguiente string de la cola
            string tarea = strings.Dequeue();

            // Procesar el string (en este caso, añadir "Completed" al final)
            string resultado = tarea + " Completed";

            // Mostrar el string completado
            Console.WriteLine(resultado);
        }
    }

    static void Main(string[] args)
    {
        // Iniciar el hilo procesador
        ThreadStart ts1 = new ThreadStart(Thread);
        Thread t1 = new Thread(ts1);
        t1.Start();

        // Bucle principal para leer y agregar strings
        while (true)
        {
            // Leer un nuevo string del usuario
            Console.Write("Introduce un string: ");
            string tarea = Console.ReadLine();

            // Agregar el string a la cola
            strings.Enqueue(tarea);

            // Señalar que hay un nuevo string
            nuevaTarea.Set();
        }
    }
}
