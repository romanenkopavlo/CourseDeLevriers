using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDeLevriers
{
    public class Levrier
    {
        private int instanceNumber;

        public int InstanceNumber
        {
            set
            {
                this.instanceNumber = value;
            }
        }
        public int Distance { get; set; }
        public Mutex Mutex { get; set; }
        public int Color { get; internal set; }
        public Random Random { get; set; }
        public ManualResetEvent depart;
        public ManualResetEvent arrivee;

        public void Run()
        {
            depart.WaitOne();
            for (int i = 0; i < Distance; i++)
            {
                Thread.Sleep(this.Random.Next(10));
                Mutex.WaitOne();
                Console.ForegroundColor = (ConsoleColor)Color;
                Console.WriteLine("Le levrier numero " + this.instanceNumber + " parcouru: " + (i + 1) + " metres");
                Mutex.ReleaseMutex();
            }
            arrivee.Set();
        }
    }
}
