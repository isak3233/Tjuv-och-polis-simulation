﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class Helper
    {
        static private string[] listOfNames = { //Lista med namn
            "Andersson",
            "Johansson",
            "Nilsson",
            "Eriksson",
            "Larsson",
            "Karlsson",
            "Lindberg",
            "Bergström",
            "Holm",
            "Lundgren",
            "Sandberg",
            "Sjöberg",
            "Nyström",
            "Forsberg",
            "Öberg",
            "Hellström",
            "Norén",
            "Ekström",
            "Dahl",
            "Strömberg",
            "Björk",
            "Håkansson",
            "Berglund",
            "Viklund",
            "Alström",
            "Melin",
            "Sundqvist",
            "Blomgren",
            "Falk",
            "Åkesson"
};
        static public List<Person> GeneratePeople(int civilAmount, int policeAmount, int thiefAmount, int xSize, int ySize) //Genererar personer med unika positioner, samt riktning
        {
            int totalIndex = 0;
            List<Person> listOfPeople = new List<Person>();

            for (int i = 0; i < civilAmount; i++) //civil
            {
                int[] randomDirection = { Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2) };
                int[] randomPositions = GetPosition(listOfPeople, xSize, ySize);
                listOfPeople.Add(new Civil(listOfNames[totalIndex], randomPositions[0], randomPositions[1], randomDirection));
                totalIndex++; 
            }
            for (int i = 0; i < policeAmount; i++) //polis
            {
                int[] randomDirection = { Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2) };
                int[] randomPositions = GetPosition(listOfPeople, xSize, ySize);
                listOfPeople.Add(new Police(listOfNames[totalIndex], randomPositions[0], randomPositions[1], randomDirection));
                totalIndex++;
            }
            for (int i = 0; i < thiefAmount; i++) //Tjuv
            {
                int[] randomDirection = { Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2) };
                int[] randomPositions = GetPosition(listOfPeople, xSize, ySize);
                listOfPeople.Add(new Thief(listOfNames[totalIndex], randomPositions[0], randomPositions[1], randomDirection));
                totalIndex++;
            }

            return listOfPeople;
        }
        static bool CheckIfPosistionTaken(List<Person> listOfPeople, int xPos, int yPos) //kollar om positionen är tagen
        {
            foreach(Person person in listOfPeople)
            {
                if(person.XPos == xPos && person.YPos == yPos)
                {
                    return true;
                }
            }
            return false;
        }
        static int[] GetPosition(List<Person> listOfPeople, int xSize, int ySize) //genererar en unik position
        {
            int randomXPos = Random.Shared.Next(0, xSize);
            int randomYPos = Random.Shared.Next(0, ySize);
            bool positionTaken = CheckIfPosistionTaken(listOfPeople, randomXPos, randomYPos);
            while (positionTaken)
            {
                randomXPos = Random.Shared.Next(0, xSize);
                randomYPos = Random.Shared.Next(0, ySize);
                positionTaken = CheckIfPosistionTaken(listOfPeople, randomXPos, randomYPos);
            }
            int[] returnArray = { randomXPos, randomYPos };
            return returnArray;
        }
    }
}
