using ContainerShip.Classes;

Console.WriteLine(("Enter the length of the ship: "));
int length = Convert.ToInt32(Console.ReadLine());
Console.WriteLine(("Enter the width of the ship: "));
int width = Convert.ToInt32(Console.ReadLine());

Console.WriteLine(("How many containers do you want to generate? "));
int containerCount = Convert.ToInt32(Console.ReadLine());

Ship ship = new Ship(length, width);

if (containerCount > 0)
{
    List<Container> containers = new List<Container>();
    Random random = new Random(); // Create Random instance once outside the loop
    for (int i = 0; i < containerCount; i++)
    {
        // Use weighted probability - 55% chance for regular container, 15% for others
        int randomNum = random.Next(1, 101);
        int weight = random.Next(15, 26);

        if (randomNum <= 55) // 55% chance
        {
            containers.Add(new RegularContainer(weight));
        }
        else if (randomNum <= 70) // 15% chance
        {
            containers.Add(new ValuableContainer(weight));
        }
        else if (randomNum <= 85) // 15% chance
        {
            containers.Add(new CoolableContainer(weight));
        }
        else // 15% chance
        {
            containers.Add(new ValuableCoolableContainer(weight));
        }
    }

    ship.ArrangeContainers(containers);

    string url = UrlGenerator.GetUrl(ship);
    TextCopy.ClipboardService.SetText(url);
    Console.WriteLine("[INFO] URL copied to clipboard");
}







