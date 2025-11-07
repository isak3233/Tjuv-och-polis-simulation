namespace ToPSimulation
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            int cityXSize = 100;
            int cityYSize = 25;
            int prisonXSize = 10;
            int prisonYSize = 10;
            int amountOfThiefs = 10;
            int amountOfCivils = 30;
            int amountOfPolice = 10;
            // MÅSTE FINNAS TILLRÄCKLIGT MED UNIKA NAMN I HELPER OM MAN LÄGGER TILL FLER PERSONER
            Simulation simulation = new Simulation(cityXSize, cityYSize, prisonXSize, prisonYSize, amountOfThiefs, amountOfCivils, amountOfPolice);
            simulation.BeginSimulation();
        }
    }
}
