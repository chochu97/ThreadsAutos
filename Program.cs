using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadAutos
{
    internal class Program
    {
        static readonly object lockObj = new object();
        static List<string> posiciones = new List<string>();
        static int ordenLlegada = 1;
        const int meta = 100;
        static void Main(string[] args)
        {
            Thread auto1 = new Thread(() => Carrera("Ferrari Rojo"));
            Thread auto2 = new Thread(() => Carrera("Lamborghini Aventador"));
            Thread auto3 = new Thread(() => Carrera("Gol Trend"));

            auto1.Priority = ThreadPriority.Lowest;
            auto2.Priority = ThreadPriority.Normal;
            auto3.Priority = ThreadPriority.Highest;

            auto3.Start();
            auto1.Start();
            auto2.Start();

            auto3.Join();
            auto1.Join();
            auto2.Join();
            

            Console.WriteLine("Resultados de la carrera");
            for(int i = 0; i < posiciones.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {posiciones[i]}");
            }

            Console.ReadLine();
        }

        static void Carrera(string nombreAuto)
        {
            for(int i= 0; i < meta; i++)
            {
                lock (lockObj)
                {
                    Console.WriteLine($"{nombreAuto}: {i}");
                }

                Thread.Sleep(100);
            }

            lock (lockObj)
            {
                posiciones.Add($"{ordenLlegada}. {nombreAuto}");
                ordenLlegada++;
                Console.WriteLine($"{nombreAuto} ha llegado a la meta!");
            }
        }
    }
}
