namespace ToPSimulation;

public class StatusFeed
{
    public static void WriteStatus(List<Person> persons)
    {
        int amountOfPolice = 0;
        int amountOfThief = 0;
        int amountOfCitizen = 0;
        
        foreach (Person person in persons)
        {
            if (person is Police)
            {
                amountOfPolice++;
            }
            if (person is Thief)
            {
                amountOfThief++;
            }
            if (person is Civil)
            {
                amountOfCitizen++;
            }
        }
        Console.WriteLine(Helper.statusString);
        Console.WriteLine($"Av {amountOfThief} tjuvar är det nu (FIXA SENARE) kvar");
        Console.WriteLine($"Av {amountOfCitizen} medborgare är det nu {amountOfCitizen} kvar");
        Console.WriteLine($"Av {amountOfPolice} poliser är det nu {amountOfPolice} kvar");
    }
}