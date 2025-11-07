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

        public Simulation(int cityXSize, int cityYSize, int prisonXSize, int prisonYSize, int amountOfThiefs, int amountOfCivils, int amountOfPolice) //Konstruktor för simulation
        {

            List<IPerson> people = Helper.GeneratePeople(amountOfCivils, amountOfPolice, amountOfThiefs, cityXSize, cityYSize);

            City = new City(people, cityXSize, cityYSize);
            Prison = new Prison(new List<IPerson>(), prisonXSize, prisonYSize);
            StatusFeed = new StatusFeed(amountOfThiefs, amountOfPolice, amountOfCivils);
        }

        public void BeginSimulation ()
        {
            while (true)
            {
                Console.Clear();

                City.MovePeople();
                int amountOfEvents = City.DetectCollisionAndApplyAction();

                List<IPerson> newPrisoners = City.GetThiefsTaken();
                Prison.AddPeopleToPlace(newPrisoners);
                City.RemovePeopleFromPlace(newPrisoners);
                
                List<IPerson> prisonersToRelease = Prison.GetReleasedThiefs();
                City.AddPeopleToRandomPosition(prisonersToRelease);
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
