namespace ContainerShip.Classes;

public class CoolableContainer : Container
{
    public CoolableContainer(int weight) : base(ContainerType.Coolable, weight) 
    {
        RequiresCooling = true;
        Type = ContainerType.Coolable;
    }

    public override bool CanSupport(Container container)
    {
        Console.WriteLine("Dikke vette huts");
        return base.CanSupport(container);
    }
}