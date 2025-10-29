using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToPSimulation
{
   
    public class Police : IPerson //Skapar subklassen Polis
    {
        public string Name { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int[] Directions { get; set; }
        public List<Item> Confiscated {  get; set; }
        public Police(string name, int xPos, int yPos, int[]directions) 
        {
            Name = name;
            XPos = xPos;
            YPos = yPos;
            Directions = directions;
            Confiscated = new List<Item>();
        }
        public string PersonInteract(IPerson personCollidedWith)
        {
            string collisionEventString = "";
            if (personCollidedWith is Civil)
            {
                collisionEventString = personCollidedWith.PersonInteract(this);
            } else if(personCollidedWith is Thief)
            {
                collisionEventString = personCollidedWith.PersonInteract(this);
            } else if (personCollidedWith is Police)
            {
                //collisionEventString = $"Konstapeln {this.Name} hälsade på konstapeln {personCollidedWith.Name}";
            }
                return collisionEventString;
        }
    }
    public class Civil : IPerson //Skapar subklassen Civil
    {
        public string Name { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int[] Directions { get; set; }
        public List<Item> Belongings { get; set; }
        public Civil(string name, int xPos, int yPos, int[] directions)
        {
            Name = name;
            XPos = xPos;
            YPos = yPos;
            Directions = directions;
            Belongings = new List<Item>();
            string[] itemsToAdd = { "Nycklar", "Mobil", "Pengar", "Klocka" };
            foreach(string itemName in itemsToAdd)
            {
                Belongings.Add(new Item(itemName));
            }
        }
        public string PersonInteract(IPerson personCollidedWith)
        {
            string collisionEventString = "";
            if(personCollidedWith is Police)
            {
                collisionEventString = $"Medborgaren {this.Name} hälsar på konstapeln {personCollidedWith.Name}";
            }
            else if(personCollidedWith is Civil)
            {
                //collisionEventString = $"Medborgaren {this.Name} hälsar på medborgaren {personCollidedWith.Name}";
            }
            else if (personCollidedWith is Thief)
            {
                if(Belongings.Count > 0)
                {
                    int randomItemIndex = Random.Shared.Next(0, this.Belongings.Count);
                    Item randomItem = Belongings[randomItemIndex];
                    Thief thief = (Thief)personCollidedWith;
                    thief.Stolen.Add(randomItem);
                    collisionEventString = $"Tjuven {personCollidedWith.Name} stal {randomItem.ItemName} från medborgaren {this.Name}";
                    this.Belongings.RemoveAt(randomItemIndex);
                } else
                {
                    collisionEventString = $"{personCollidedWith.Name} försökte ta saker från medborgaren {this.Name} men hen hade inget";
                }
                    
                
            }
            return collisionEventString;
        }
    }
    public class Thief : IPerson //Skapar subklassen Tjuv
    {
        public string Name { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int[] Directions { get; set; }
        public List<Item> Stolen { get; set; }
        public bool TakenByPolice = false;
        public DateTime TimeInPrison;

        public Thief(string name, int xPos, int yPos, int[] directions)
        {
            Name = name;
            XPos = xPos;
            YPos = yPos;
            Directions = directions;
            Stolen = new List<Item>();
        }
        public string PersonInteract(IPerson personCollidedWith)
        {
            string collisionEventString = "";
            if (personCollidedWith is Civil)
            {
                collisionEventString = personCollidedWith.PersonInteract(this);
            } else if(personCollidedWith is Police)
            {
                if(this.Stolen.Count > 0)
                {
                    TimeInPrison = DateTime.Now.AddSeconds(10 * this.Stolen.Count);

                    Police police = (Police)personCollidedWith;
                    police.Confiscated.AddRange(this.Stolen);
                    this.Stolen.Clear();
                    collisionEventString = $"Tjuven {this.Name} blev tagen av polisen {police.Name} och tog alla hans stulna saker";
                    TakenByPolice = true;
                } else
                {
                    collisionEventString = $"Tjuven {this.Name} träffade polisen {personCollidedWith.Name}. Men har inte stulit något";
                }
            }
            if(personCollidedWith is Thief)
            {
                //collisionEventString = $"Tjuven {this.Name} träffade tjuven {personCollidedWith.Name}";
            }
            return collisionEventString;
        }
    }
    public class Item //Skapar klassen för items
    {
        public string ItemName { get; set;}
        public Item(string itemName)
        {
            ItemName = itemName;
        }
    }
}
