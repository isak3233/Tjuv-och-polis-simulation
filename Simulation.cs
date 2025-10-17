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
        private StatusFeed StatusFeed;
        private Prison Prison;

        public Simulation() //Konstruktor för simulation
        {
            int xSize = 100;
            int ySize = 25;
            int amountOfThiefs = 20;
            int amountOfCivils = 30;
            int amountOfPolice = 10;

            List<Person> people = Helper.GeneratePeople(amountOfCivils, amountOfPolice, amountOfThiefs, xSize, ySize);
            //int[] dir = { 1, 0 };
            //int[] dir1 = { 0, 0 };
            //int[] dir2 = { -1, 0 };
            //List<Person> people = new List<Person>();
            //people.Add(new Police("Kalle1", 5, 0, dir));
            //people.Add(new Thief("Kalle2", 20, 0, dir2));
            //people.Add(new Civil("Kalle3", 10, 0, dir1));


            City = new City(people, xSize, ySize);
            Prison = new Prison(new List<Person>(), 10, 10);
            StatusFeed = new StatusFeed(amountOfThiefs, amountOfPolice, amountOfCivils);
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
                Prison.AddPeopleToPlace(newPrisoners);
                City.RemovePeopleFromPlace(newPrisoners);

                List<Person> prisonersToRelease = Prison.GetReleasedThiefs();
                for (int i = 0; i < prisonersToRelease.Count; i++) 
                {
                    int[] newPosition = City.GetUniquePosition();

                    prisonersToRelease[i].XPos = newPosition[0];
                    prisonersToRelease[i].YPos = newPosition[1];
                    List<Person> tempList = new List<Person>();
                    tempList.Add(prisonersToRelease[i]);
                    //City.AddPeopleToPlace(tempList);
                    City.UpdateArea();

                }
                // Gör så att tjuvarna får en ny position i staden innan vi lägger in dom

                //Prison.RemovePeopleFromPlace(prisonersToRelease);


                Prison.MovePeople();

                
                
                
                
                City.WriteOutCity();
                Prison.WriteOutPrison();
                StatusFeed.WriteStatus(City.People);
                City.NewsFeed.WriteOutNews();

                if (amountOfEvents > 0)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    Thread.Sleep(1);
                }
                //Console.ReadLine();



            }
        }
    }
}
