/*
TEORÍA

1-EXPLICA QUE ES UN THREAD, PARA QUE SIRVE Y EN QUE SE DIFERENCIA DE UN PROCESO (2-6 LÍNEAS):

Los threads son secuencias de instrucciones que el sistema operativo puede programar para su ejecución. 
A diferencia de los procesos, los threads son entidades más pequeñas y viven dentro de los procesos. 
En conclusión, un proceso es la instancia de un programa controlada por el sistema operativo, siendo 
entidades independientes que no comparten información entre sí, mientras que los threads
en un proceso pueden compartir información entre si.

2-NOMBRAS QUE POSIBLES USOS SE LE PUEDE DAR A LOS THREADS (MÍNIMO 4)

Trabajo interactivo y en segundo plano:

Realización simultánea de tareas para mejorar la velocidad y la percepción del usuario al permitir 
que el programa realice solicitudes antes de completar las anteriores.

Procesamiento asíncrono:

Implementación de elementos asíncronos mediante hilos, como en programas de procesamiento de texto 
que crean un hilo para guardar archivos temporales mientras el usuario continúa escribiendo sin interferencias.

Aceleración de la ejecución:

Ejecución simultánea de procesos, como la ejecución de un lote mientras otro hilo lee el 
siguiente lote de un dispositivo, para aumentar la eficiencia al realizar múltiples tareas concurrentemente.

Estructuración modular de los programas:

Organización eficiente de programas mediante la separación de funciones en hilos independientes, 
facilitando la gestión y comprensión del código al asignar a cada hilo una actividad específica.

3-EXPLICA EN POCAS PALABRAS QUE ES LA MEMORIA CACHE, PARA QUE SIRVE, Y CUANTOS NIVELES TIENE:

La memoria caché en informática es un recurso de la CPU que almacena temporalmente datos recientemente 
procesados en una memoria estática de acceso aleatorio (SRAM), ubicada cerca de la CPU para proporcionar acceso rápido a los datos.

La principal función de la memoria caché es almacenar datos o instrucciones que la CPU necesitará en un futuro inmediato. 
Esto mejora la velocidad de ejecución de procesos, evitando que la CPU espere, lo que resulta en un aumento del rendimiento del equipo.

Niveles:
Caché L1: Pequeña y rápida, almacena datos para operaciones clave.
Caché L2: Más grande y más lenta que la L1.
Caché L3: La más grande pero más lenta de todas.
*/

using System;
using System.Threading;

namespace Examen_1
{
    internal class Program
    {
        //Array de todos los numeros
        static int[] array1 = new int[862];

        //Array de numeros pares
        static int[] array2 = new int[862];

        //Array de numeros más grandes que el 15
        static int[] array3 = new int[862];

        static void Main(string[] args)
        {
            //Lleno array con la función que he creado
            LlenarArrayDeNumerosRandom(array1);

            //Creamos los threadstarts
            ThreadStart ts1 = new ThreadStart(CopiarNumerosParesPrimeraParte);
            ThreadStart ts2 = new ThreadStart(CopiarNumerosParesSegundaParte);

            ThreadStart ts4 = new ThreadStart(CopiarNumsMasGrandesQue15PrimeraParte);
            ThreadStart ts5 = new ThreadStart(CopiarNumsMasGrandesQue15SegundaParte);
            ThreadStart ts6 = new ThreadStart(CopiarNumsMasGrandesQue15TerceraParte);

            //Creamos los threads
            Thread threadPar1 = new Thread(ts1);
            Thread threadPar2 = new Thread(ts2);

            Thread threadMasDeQuince1 = new Thread(ts4);
            Thread threadMasDeQuince2 = new Thread(ts5);
            Thread threadMasDeQuince3 = new Thread(ts6);

            //Iniciamos los threads para llenar el segundo array de numeros pares
            threadPar1.Start();
            threadPar2.Start();

            //Iniciamos los threads para llenar el tercer array de numeros mayores que el 15
            threadMasDeQuince1.Start();
            threadMasDeQuince2.Start();
            threadMasDeQuince3.Start();

            //Nuesto main sigue siendo un thread asi que tambien le decimos que ayude a copiar los numeros pares
            CopiarNumerosParesTerceraParte();

            //Hacemos el join de todos los threads para asegurarnos que hayan acabado
            threadPar1.Join();
            threadPar2.Join();

            threadMasDeQuince1.Join();
            threadMasDeQuince2.Join();
            threadMasDeQuince3.Join();

            string arrayString = string.Join(", ", array1);
            Console.WriteLine("ARRAY 1 \n\n" + arrayString + "\n\n");

            string arrayString2 = string.Join(", ", array2);
            Console.WriteLine("ARRAY 2; PARES \n\n" + arrayString2 + "\n\n");

            string arrayString3 = string.Join(", ", array3);
            Console.WriteLine("ARRAY 3; MAYORES QUE 15 \n\n" + arrayString3);

            Console.WriteLine("\nTodos los threads han terminado.");
            Console.ReadLine();
        }

        //Llena un array de numeros random segun especifiques
        static void LlenarArrayDeNumerosRandom(int[] array)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(46); // Números aleatorios entre 0 y 45
            }
        }

        //Funciones que copian los numeros pares de un array en concreto a otro
        static void CopiarNumerosParesPrimeraParte()
        {
            for (int i = 0; i < 287; i++)
            {
                if (array1[i] % 2 == 0)
                {
                    array2[i] = array1[i];
                }
            }
        }
        static void CopiarNumerosParesSegundaParte()
        {
            for (int i = 287; i < 574; i++)
            {
                if (array1[i] % 2 == 0)
                {
                    array2[i] = array1[i];
                }
            }
        }
        static void CopiarNumerosParesTerceraParte()
        {
            for (int i = 574; i < 862; i++)
            {
                if (array1[i] % 2 == 0)
                {
                    array2[i] = array1[i];
                }
            }
        }

        //Funciones que copian los numeros más grandes que 15 de un array en concreto a otro
        static void CopiarNumsMasGrandesQue15PrimeraParte()
        {
            for (int i = 0; i < 287; i++)
            {
                if (array1[i] > 15)
                {
                    array3[i] = array1[i];
                }
            }
        }
        static void CopiarNumsMasGrandesQue15SegundaParte()
        {
            for (int i = 287; i < 574; i++)
            {
                if (array1[i] > 15)
                {
                    array3[i] = array1[i];
                }
            }
        }
        static void CopiarNumsMasGrandesQue15TerceraParte()
        {
            for (int i = 574; i < 862; i++)
            {
                if (array1[i] > 15)
                {
                    array3[i] = array1[i];
                }
            }
        }

    }
}
