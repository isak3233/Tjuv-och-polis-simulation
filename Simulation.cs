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
            int cityXSize = 100;
            int cityYSize = 25;
            int prisonXSize = 10;
            int prisonYSize = 10;
            int amountOfThiefs = 20;
            int amountOfCivils = 30;
            int amountOfPolice = 10;

            List<Person> people = Helper.GeneratePeople(amountOfCivils, amountOfPolice, amountOfThiefs, cityXSize, cityYSize);


            City = new City(people, cityXSize, cityYSize);
            Prison = new Prison(new List<Person>(), prisonXSize, prisonYSize);
            StatusFeed = new StatusFeed(amountOfThiefs, amountOfPolice, amountOfCivils);
        }

        public void BeginSimulation ()
        {


            while (true)
            {

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
                    City.AddPeopleToPlace(tempList);
                    City.UpdateArea();

                }

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
