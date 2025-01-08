namespace ContainerShip.Classes;

public class ValuableContainer : Container
{

    public ValuableContainer(int weight) : base(ContainerType.Valuable, weight)
    {
        IsValuable = true;
        Type = ContainerType.Valuable;
    }
    public override bool CanSupport(Container container)
    {
        Console.WriteLine("Dikke vette huts");
        return base.CanSupport(container);
    }
    
}