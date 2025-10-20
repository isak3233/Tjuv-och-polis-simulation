using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class Place //skapar klassen Place
    {   
        public List<Person> People { get; set; }
        public Person[,] Area { get; set; }
        public Place(List<Person> people, int sizeX, int sizeY) //skapar en instans av klassen place 
        {
            Area = new Person[sizeY, sizeX];
            People = people;
            
            UpdateArea();
        }
        public string GetStringPlace() //Skapar en string som innehåller en grafisk representation av platsen
        {
            string returnString = "";
            for(int i = 0; i < Area.GetLength(0); i++)
            {
                returnString += "||";
                for (int j = 0; j < Area.GetLength(1); j++)
                {
                    Person person = Area[i, j];
                    if(person == null)
                    {
                        returnString += " ";
                    } else
                    {
                        if(person is Police)
                        {
                            returnString += "\u001b[34mP\u001b[0m";
                        }
                        if (person is Civil)
                        {
                            returnString += "\u001b[32mC\u001b[0m";
                        }
                        if (person is Thief)
                        {
                            returnString += "\u001b[31mT\u001b[0m";
                        } 
                    }
                }

                returnString += "||\n";
            }
            return returnString;
        }

        public void UpdateArea()
        {
            Area = new Person[Area.GetLength(0), Area.GetLength(1)];
            foreach (Person person in People)
            {
                Area[person.YPos, person.XPos] = person;
            }
            
        }
        public int[] GetUniquePosition()
        {
            //Skapar en slumpmässig position
            int randomXPos = Random.Shared.Next(0, Area.GetLength(1));
            int randomYPos = Random.Shared.Next(0, Area.GetLength(0));

            while (true)
            {
                //Kollar om den positionen är tagen
                if (Area[randomYPos, randomXPos] == null)
                {
                    return new int[] { randomXPos, randomYPos };
                }
                else //Skapar en ny position om den är tagen
                {
                     randomXPos = Random.Shared.Next(0, Area.GetLength(1));
                     randomYPos = Random.Shared.Next(0, Area.GetLength(0));
                }

            }


        }
        public virtual void MovePeople()
        {
            foreach (Person person in People)
            {
                if (person.XPos + person.Directions[0] >= 0 && person.XPos + person.Directions[0] <= Area.GetLength(1) - 1)
                {
                    person.XPos += person.Directions[0];
                }
                else
                {
                    if (!(person.XPos + person.Directions[0] >= 0))
                    {
                        person.XPos = Area.GetLength(1) - 1;
                    }
                    else if (!(person.XPos + person.Directions[0] <= Area.GetLength(1) - 1))
                    {
                        person.XPos = 0;
                    }
                }
                if (person.YPos + person.Directions[1] >= 0 && person.YPos + person.Directions[1] <= Area.GetLength(0) - 1)
                {
                    person.YPos += person.Directions[1];
                }
                else
                {
                    if (!(person.YPos + person.Directions[1] >= 0))
                    {
                        person.YPos = Area.GetLength(0) - 1;
                    }
                    else if (!(person.YPos + person.Directions[1] <= Area.GetLength(0) - 1))
                    {
                        person.YPos = 0;
                    }
                }
            }
            UpdateArea();
        }
        public void AddPeopleToPlace(List<Person> peopleToAdd)
        {
            People.AddRange(peopleToAdd);
        }
        public void RemovePeopleFromPlace(List<Person> peopleToRemove)
        {
            for(int i = 0; i < peopleToRemove.Count; i++)
            {
                People.Remove(peopleToRemove[i]);
            }
        }
    }
    public class City : Place //skapar subklassen City
    {
        public NewsFeed NewsFeed { get; set; }

        public City(List<Person> people, int sizeX, int sizeY) : base(people, sizeX, sizeY)
        {
            NewsFeed = new NewsFeed();
        }
        public void WriteOutCity()
        {
            Console.Write(Helper.cityString + this.GetStringPlace());
        }
        public void AddPeopleToRandomPosition(List<Person> peopleToAdd)
        {
            for (int i = 0; i < peopleToAdd.Count; i++)
            {
                int[] newPosition = GetUniquePosition();

                peopleToAdd[i].XPos = newPosition[0];
                peopleToAdd[i].YPos = newPosition[1];
                List<Person> tempList = new List<Person>();
                tempList.Add(peopleToAdd[i]);
                AddPeopleToPlace(tempList);
                UpdateArea();

            }
        }

        public int DetectCollisionAndApplyAction()
        {
            List<string> collisionEvents = new List<string>();
            int amountOfEvents = 0;
            for(int i = 0; i < People.Count; i++)
            {
                for(int j = i + 1; j < People.Count; j++)
                {
                    Person person1 = People[i];
                    Person person2 = People[j];
                    int person1NewXPos = person1.XPos + person1.Directions[0];
                    int person1NewYPos = person1.YPos + person1.Directions[1];
                    int person2NewXPos = person2.XPos + person2.Directions[0];
                    int person2NewYPos = person2.YPos + person2.Directions[1];


                    // Dom två första if satserna kollar om personerna har gått igenom varandra (En kollsion har hänt) 
                    // Den sista kollar om personerna är i varandra (En kollison har hänt)
                    if ( 
                        (person1NewXPos == person2.XPos && person1NewYPos == person2.YPos) && 
                        (person2NewXPos == person1.XPos && person2NewYPos == person1.YPos) ||
                        (person1.XPos == person2.XPos && person1.YPos == person2.YPos)
                        )
                    {
                        
                        string collisionEvent = person1.PersonInteract(person2);
                        if(collisionEvent != "")
                        {
                            collisionEvents.Add(collisionEvent);
                            amountOfEvents++;
                        }
                        
                    }
                }
            }
            NewsFeed.News.AddRange(collisionEvents);
            return amountOfEvents;
        }
        public List<Person> GetThiefsTaken()
        {
            List<Person> theifsTaken = new List<Person>();
            for(int i = 0; i < People.Count;i++)
            {
                Person person = People[i];
                
                if(person is Thief)
                {
                    Thief thief = (Thief)person;
                    if(thief.TakenByPolice)
                    {
                        theifsTaken.Add(person);

                    }
                }
                
            }
            return theifsTaken;
        }
        
        
    }
    public class Prison : Place //skapar subklassen Prison
    {
        public Prison(List<Person> people, int sizeX, int sizeY) : base(people, sizeX, sizeY)
        {

        }
        public void WriteOutPrison()
        {
            Console.Write(Helper.prisonString + this.GetStringPlace());
        }
       
        public override void MovePeople()
        {
            base.MovePeople();
            for (int i = 0; i < People.Count;i++)
            {
                Person person = People[i];
                if(person is Thief)
                {
                    Thief thief = (Thief)person;
                    
                    thief.TimeInPrison--;
                }
                
            }

        }
        public List<Person> GetReleasedThiefs()
        {
            List<Person> PrisonersToRelease = new List<Person>();
            for (int i = 0; i < People.Count; i++)
            {
                Person person = People[i];
                if (person is Thief)
                {
                    Thief thief = (Thief)person;
                    if(thief.TimeInPrison <= 0)
                    {
                        thief.TakenByPolice = false;
                        PrisonersToRelease.Add(person);

                    }
                }

            }
            return PrisonersToRelease;
        }
    }

}
