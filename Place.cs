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
                            returnString += "P";
                        }
                        if (person is Civil)
                        {
                            returnString += "C";
                        }
                        if (person is Thief)
                        {
                            returnString += "T";
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
        
        public void MovePeople()
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
    }
    public class City : Place //skapar subklassen City
    {
        public City(List<Person> people, int sizeX, int sizeY) : base(people, sizeX, sizeY)
        {

        }

    }
    public class Prison : Place //skapar subklassen Prison
    {
        public Prison(List<Person> people, int sizeX, int sizeY) : base(people, sizeX, sizeY)
        {

        }
    }

}
