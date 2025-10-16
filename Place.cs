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

                    

                    if ( 
                        (person1NewXPos == person2.XPos && person1NewYPos == person2.YPos) ||
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
        public void AddPrisoners(List<Person> thiefs)
        {
            
            
            People.AddRange(thiefs);
        }
        public override void MovePeople()
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
                        PrisonersToRelease.Add(person);

                    }
                }

            }
            return PrisonersToRelease;
        }
    }

}
