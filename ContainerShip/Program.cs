// See https://aka.ms/new-console-template for more information

using ContainerShip.Classes;

Console.WriteLine(("Enter the length of the ship: "));
int length = Convert.ToInt32(Console.ReadLine());
Console.WriteLine(("Enter the width of the ship: "));
int width = Convert.ToInt32(Console.ReadLine());

Ship ship = new Ship(length, width);



// calculate howmany chinese people can fit in the ship
int chinesePeople = ship.Lenght * ship.Width;
Console.WriteLine($"The ship can fit {chinesePeople} chinese people");


// calculate howmany refugees can fit in the ship (0.2m2 per person)
double refugees = (ship.Lenght * ship.Width) / 0.2;
Console.WriteLine($"The ship can fit {refugees} refugees");

// please calculate the avarage woman wage and men wage 
