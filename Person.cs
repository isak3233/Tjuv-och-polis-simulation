using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class Person //Skapar klassen Person
    {
        public string Name { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int[] Directions { get; set; }
        public Person(string name, int xPos, int yPos, int[] directions)
        {
            Name = name;
            XPos = xPos;
            YPos = yPos;
            Directions = directions;
        }
    }
    public class Police : Person //Skapar subklassen Polis
    {
        List<Item> Confiscated {  get; set; }
        public Police(string name, int xPos, int yPos, int[]directions) : base(name, xPos, yPos, directions)
        {
            Confiscated = new List<Item>();
        }
    }
    public class Civil : Person //Skapar subklassen Civil
    {
        List<Item> Belongings { get; set; }
        public Civil(string name, int xPos, int yPos, int[] directions) : base(name, xPos, yPos, directions)
        {
            Belongings = new List<Item>();
        }
    }
    public class Thief : Person //Skapar subklassen Tjuv
    {
        List<Item> Stolen { get; set; }
        public Thief(string name, int xPos, int yPos, int[] directions) : base(name, xPos, yPos, directions)
        {
            Stolen = new List<Item>();
        }
    }
    public class Item //Skapar klassen för items
    {
        public string Name { get; set;}
        public Item(string name)
        {
            Name = name;
        }
    }
}
