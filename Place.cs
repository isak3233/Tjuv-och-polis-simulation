using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class Place
    {
        public List<Person> People { get; set; }
        public Person[,] Area { get; set; }
        public Place(List<Person> people, int sizeX, int sizeY)
        {
            Area = new Person[sizeY, sizeX];
            foreach (Person person in people)
            {
                Area[person.YPos, person.XPos] = person;
            }
            People = people;
        }
        public string GetStringPlace()
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
    public class City : Place
    {
        public City(List<Person> people, int sizeX, int sizeY) : base(people, sizeX, sizeY)
        {

        }

    }
    public class Prison : Place
    {
        public Prison(List<Person> people, int sizeX, int sizeY) : base(people, sizeX, sizeY)
        {

        }
    }

}
