namespace ToPSimulation;

public class StatusFeed
{

    public int StartingAmountThief { get; set; }
    public int StartingAmountCivil { get; set; }
    public int StartingAmountPolice { get; set; }
    public StatusFeed(int startingAmountThief, int startingAmountPolice, int startingAmountCivil)
    {
        StartingAmountThief = startingAmountThief;
        StartingAmountPolice = startingAmountPolice;
        StartingAmountCivil = startingAmountCivil;
    }
    public void WriteStatus(List<Person> persons)
    {
        int amountOfThief = 0;
        foreach (Person person in persons)
        {
            if (person is Thief)
            {
                amountOfThief++;
            }
        }
        Console.WriteLine(Helper.statusString);
        Console.WriteLine($"Av {StartingAmountThief} tjuvar är det nu {amountOfThief} kvar");
        Console.WriteLine($"Av {StartingAmountCivil} medborgare är det nu {StartingAmountCivil} kvar");
        Console.WriteLine($"Av {StartingAmountPolice} poliser är det nu {StartingAmountPolice} kvar");
    }
}