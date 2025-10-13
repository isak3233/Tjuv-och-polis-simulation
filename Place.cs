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
            foreach (Person person in people)
            {
                Area[person.YPos, person.XPos] = person;
            }
            People = people;
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
