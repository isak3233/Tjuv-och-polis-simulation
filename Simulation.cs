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
            //List<Person> people = Helper.GeneratePeople(10, 10, 10, xSize, ySize);
            int[] dir = { -1, 0 };
            int[] dir1 = { 0, 1 };
            int[] dir2 = { -1, 0 };
            List<Person> people = new List<Person>();
            people.Add(new Police("Kalle1", 7, 3, dir1));
            people.Add(new Thief("Kalle2", 20, 1, dir2));
            people.Add(new Civil("Kalle3", 10, 5, dir));


            City = new City(people, xSize, ySize);
            Prison = new Prison(new List<Person>(), 10, 10);
        }
        public void BeginSimulation ()
        {
            while (true)
            {
                // Gör så att vi kan se i newsfeed när en person lämnar fängelset
                // Testa alla olika riktingar så att vår kollsion funkar rätt
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
                    //Thread.Sleep(2000);
                }
                else
                {
                    //Thread.Sleep(500);
                }
                Console.ReadLine();



            }
        }
    }
}
