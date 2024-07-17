using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                System.Console.WriteLine("no argument provided. Use 'producer' or 'consumer' argument.");
                Environment.Exit(-1);
            }
            if (args[0] == "producer") new Producer().Start();
            else if (args[0] == "consumer") new Consumer().Start();
            else
            {
                System.Console.WriteLine("incorrect argument");
                Environment.Exit(-1);
            }
        }
    }
}
