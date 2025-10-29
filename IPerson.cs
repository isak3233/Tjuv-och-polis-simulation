using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public interface IPerson //Skapar klassen Person
    {
        public string Name { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int[] Directions { get; set; }

        public abstract string PersonInteract(IPerson person);

    }
}
