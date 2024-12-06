namespace ContainerShip.Classes;

public class CoolableContainer : Container
{
    public CoolableContainer()
    {
        RequiresCooling = true;
    }

    public override bool CanSupport(Container container)
    {
        Console.WriteLine("Dikke vette huts");
        return base.CanSupport(container);
    }
}