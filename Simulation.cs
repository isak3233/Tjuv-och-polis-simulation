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
            List<Person> people = Helper.GeneratePeople(10, 5, 3, xSize, ySize);
            City city = new City(people, xSize, ySize);
            
            while (true)
            {
                Console.Clear();
                Console.Write(Helper.cityString + city.GetStringPlace() + Helper.cityLastString);
                city.MovePeople();
                Thread.Sleep(500);
            }

         

        }
    }
}
