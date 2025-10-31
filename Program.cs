namespace ToPSimulation
{
    internal class Program
    {
        static void Main(string[] args) //skapar en ny simulation
        {
            int cityXSize = 100;
            int cityYSize = 25;
            int prisonXSize = 10;
            int prisonYSize = 10;
            int amountOfThiefs = 10;
            int amountOfCivils = 30;
            int amountOfPolice = 10;
            Simulation simulation = new Simulation(cityXSize, cityYSize, prisonXSize, prisonYSize, amountOfThiefs, amountOfCivils, amountOfPolice);
            simulation.BeginSimulation();
        }
    }
}
