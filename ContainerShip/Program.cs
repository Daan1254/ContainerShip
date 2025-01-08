﻿using ContainerShip.Classes;

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
    for (int i = 0; i < containerCount; i++)
    {
        Random random = new Random();
        int containerType = random.Next(3);
        int weight = random.Next(1, 26);
        switch (containerType)
        {
            case 0:
                containers.Add(new CoolableContainer(weight));
                break;
            case 1:
                containers.Add(new ValuableContainer(weight));
                break;
            case 2:
                containers.Add(new RegularContainer(weight));
                break;
        }
    }

    ship.ArrangeContainers(containers);

    string url = UrlGenerator.GetUrl(ship);
    Console.WriteLine(url);
}







