using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class Simulation
    {
        private City City;

        public Simulation() //Konstruktor för simulation
        {
            int xSize = 100;
            int ySize = 25;
            List<Person> people = Helper.GeneratePeople(10, 5, 10, xSize, ySize);
            City = new City(people, xSize, ySize);        
        }
        public void BeginSimulation ()
        {
            while (true)
            {
                Console.Clear();
                
                City.MovePeople();

                int amountOfEvents = City.DetectCollisionAndApplyAction();
                
                City.WriteOutCity();
                StatusFeed.WriteStatus(City.People);
                City.NewsFeed.WriteOutNews();

                if (amountOfEvents > 0)
                {
                    Thread.Sleep(2000);
                }
                else
                {
                    Thread.Sleep(500);
                }



            }
        }
    }
}
