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
        private Prison Prison;

        public Simulation() //Konstruktor för simulation
        {
            int xSize = 100;
            int ySize = 25;
            List<Person> people = Helper.GeneratePeople(10, 10, 10, xSize, ySize);



            City = new City(people, xSize, ySize);
            Prison = new Prison(new List<Person>(), 10, 10);
        }
        public void BeginSimulation ()
        {
            while (true)
            {
                Console.Clear();
                
                City.MovePeople();
                int amountOfEvents = City.DetectCollisionAndApplyAction();

                List<Person> newPrisoners = City.GetThiefsTaken();
                Prison.AddPrisoners(newPrisoners);
                City.RemovePeopleFromPlace(newPrisoners);

                List<Person> prisonersToRelease = Prison.GetReleasedThiefs();
                // Gör så att tjuvarna får en ny position i staden innan vi lägger in dom
                City.AddPeopleToPlace(prisonersToRelease);
                Prison.RemovePeopleFromPlace(prisonersToRelease);


                Prison.MovePeople();

                
                
                
                
                City.WriteOutCity();
                Prison.WriteOutPrison();
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
