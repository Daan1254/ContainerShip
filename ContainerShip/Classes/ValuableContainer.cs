namespace ContainerShip.Classes;

public class ValuableContainer : Container
{
    public override bool CanSupport(Container container)
    {
        Console.WriteLine("Dikke vette huts");
        return base.CanSupport(container);
    }

    public ValuableContainer(int width, int length, int height)
    {
        IsValuable = true;
    }
}