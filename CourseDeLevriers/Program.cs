using System.IO.IsolatedStorage;

namespace CourseDeLevriers
{
    public class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex();
            Random random = new Random();
            ManualResetEvent[] events = new ManualResetEvent[10];
            ManualResetEvent depart = new ManualResetEvent(false);
            List<int> ordreArrivee = new List<int>();

            for (int i = 0; i < 10; i++) 
            {
                Levrier levrier = new Levrier();
                levrier.InstanceNumber = i;
                levrier.Random = random;
                levrier.Color = i + 1;
                levrier.Mutex = mutex;
                levrier.Distance = 50;
                levrier.arrivee = new ManualResetEvent(false);
                levrier.depart = depart;
                events[i] = levrier.arrivee;

                Thread thread = new Thread(levrier.Run);
                thread.Start();
            }

            depart.Set();

            int position;
            int arrives = 0;

            while (arrives < 10)
            {
                position = WaitHandle.WaitAny(events);
                mutex.WaitOne();
                Console.WriteLine("Le n° " + position + " est arrivé !   <=======================================================================");
                mutex.ReleaseMutex();
                arrives++;
                ordreArrivee.Add(position);
                events[position].Reset();
            }

            Console.WriteLine("-----===== Tableau des arrivées =====-----");
            foreach (int i in ordreArrivee)
            {
                Console.WriteLine("Levrier " + i);
            }
            Console.ReadLine();
        }
    }
}
