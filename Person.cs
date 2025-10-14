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
        public virtual string PersonInteract(Person person)
        {
            return "";
        }
    }
    public class Police : Person //Skapar subklassen Polis
    {
        public List<Item> Confiscated {  get; set; }
        public Police(string name, int xPos, int yPos, int[]directions) : base(name, xPos, yPos, directions)
        {
            Confiscated = new List<Item>();
        }
        public override string PersonInteract(Person personCollidedWith)
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
                collisionEventString = $"Konstapeln {this.Name} hälsade på konstapeln {personCollidedWith.Name}";
            }
                return collisionEventString;
        }
    }
    public class Civil : Person //Skapar subklassen Civil
    {
        public List<Item> Belongings { get; set; }
        public Civil(string name, int xPos, int yPos, int[] directions) : base(name, xPos, yPos, directions)
        {
            Belongings = new List<Item>();
            string[] itemsToAdd = { "Nycklar", "Mobil", "Pengar", "Klocka" };
            foreach(string itemName in itemsToAdd)
            {
                Belongings.Add(new Item(itemName));
            }
        }
        public override string PersonInteract(Person personCollidedWith)
        {
            string collisionEventString = "";
            if(personCollidedWith is Police)
            {
                collisionEventString = $"Medborgaren {this.Name} hälsar på konstapeln {personCollidedWith.Name}";
            }
            else if(personCollidedWith is Civil)
            {
                collisionEventString = $"Medborgaren {this.Name} hälsar på medborgaren {personCollidedWith.Name}";
            }
            else if (personCollidedWith is Thief)
            {
                int randomItemIndex = Random.Shared.Next(0, this.Belongings.Count);
                Item randomItem = Belongings[randomItemIndex];
                Thief thief = (Thief)personCollidedWith;
                thief.Stolen.Add(randomItem);
                collisionEventString = $"{personCollidedWith.Name} stal {randomItem.ItemName} från medborgaren {this.Name}";
                this.Belongings.RemoveAt(randomItemIndex);
                
            }
            return collisionEventString;
        }
    }
    public class Thief : Person //Skapar subklassen Tjuv
    {
        public List<Item> Stolen { get; set; }
        public Thief(string name, int xPos, int yPos, int[] directions) : base(name, xPos, yPos, directions)
        {
            Stolen = new List<Item>();
        }
        public override string PersonInteract(Person personCollidedWith)
        {
            string collisionEventString = "";
            if (personCollidedWith is Civil)
            {
                collisionEventString = personCollidedWith.PersonInteract(this);
            } else if(personCollidedWith is Police)
            {
                if(this.Stolen.Count > 0)
                {
                    Police police = (Police)personCollidedWith;
                    police.Confiscated.AddRange(this.Stolen);
                    this.Stolen.Clear();
                    collisionEventString = $"Tjuven {this.Name} blev tagen av polisen {police.Name} och tog alla hans stulna saker";
                } else
                {
                    collisionEventString = $"Tjuven {this.Name} träffade polisen {personCollidedWith.Name}. Men har inte stulit något";
                }
            }
            if(personCollidedWith is Thief)
            {
                collisionEventString = $"Tjuven {this.Name} träffade tjuven {personCollidedWith.Name}";
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
