using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class Simulation
    {
        public Simulation() //Konstruktor för simulation
        {
            int xSize = 100;
            int ySize = 25;
            City city = new City(Helper.GeneratePeople(10, 5, 3, xSize, ySize), xSize, ySize);
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CITY  ==============================================================================================");
                Console.Write(city.GetStringPlace());
                city.MovePeople();
                Console.WriteLine("========================================================================================================");
                Thread.Sleep(500);
            }

         

        }
    }
}
