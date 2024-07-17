using System;
using System.Threading;

class Program
{
    private static int movimientoMaximo = 5; //Numero de posiciones que se movera la pelota

    private int coordenadaX = 10; //Creo un par de variables para las coordenadas con las que hare operaciones mas adelante
    private int coordenadaY = 10 + movimientoMaximo; // Comenzar cerca del límite inferior

    private bool moviendoAbajo = true; //Dirección del movimiento
    
    public delegate void DirectionChangeHandler(string message); //Delegado para el event
    public event DirectionChangeHandler DirectionChanged; //Event basado en el delegado

    protected void Display(int x, int y, string s)
    {
        Console.SetCursorPosition(x, y); //La funcion SetCursorPosition me permite "apuntar" a una posicion de la consola con las coordenadas X y Y que yo pasare mas adelante
        Console.Write(s); //Con la funcion Write escribe y muestra el String que le pase yo al parametro 's'
    }

    protected void OnDirectionChanged(string message)
    {
        if (DirectionChanged != null) //Si DirectionChanged no es null, se invoca el evento
        {
            DirectionChanged.Invoke(message);
        }
    }

    private void HandleDirectionChange(string message)
    {
        Console.SetCursorPosition(0, 20); // Mostrar el mensaje en una posicion fija
        Console.WriteLine(message);
    }

    public void DisplayAnimation()
    {
        while (!Console.KeyAvailable) //While infinito hasta que se pulse alguna tecla
        {
            ClearAnimationArea(); //Borrar solo el area de la animacion, no toda la consola

            Display(coordenadaX - 2, coordenadaY - 1, " █████ "); //Dibujar una pelota circular
            Display(coordenadaX - 3, coordenadaY, " ███████ ");
            Display(coordenadaX - 3, coordenadaY + 1, " ███████ ");
            Display(coordenadaX - 2, coordenadaY + 2, " █████ ");

            Thread.Sleep(500); //Thread.Sleep para que la animacion no sea tan rapida

            if (coordenadaY == 16 || coordenadaY == 10)
            {
                Display(coordenadaX - 10, 18, " *********************");
            }

            if (moviendoAbajo) //Logica que bools para saber donde hay que mostrar los asteriscos
            {
                coordenadaY++;
                if (coordenadaY > 10 + movimientoMaximo)
                {
                    moviendoAbajo = false;
                    OnDirectionChanged("Dirección: hacia arriba");
                }
            }
            else
            {
                coordenadaY--;
                if (coordenadaY < 10 - movimientoMaximo)
                {
                    moviendoAbajo = true;
                    OnDirectionChanged("Dirección: hacia abajo");
                }
            }
        }
    }

    private void ClearAnimationArea()
    {
        for (int y = 0; y < 10 + movimientoMaximo + 3; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }

    static void Main(string[] args)
    {
        Program p = new Program(); //Creacion de un Program con nombre 'p' para asi llamar a funciones del codigo
        p.DirectionChanged += p.HandleDirectionChange;

        ThreadStart ts1 = new ThreadStart(p.DisplayAnimation); //Creacion y inicio del hilo para la animacion con ThreadStart
        Thread thread1 = new Thread(ts1);
        thread1.Start();
        
        Console.ReadKey(true); //Esperar a que el usuario presione una tecla para terminar

        thread1.Join();
    }
}
