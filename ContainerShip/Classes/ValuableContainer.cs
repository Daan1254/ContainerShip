namespace ContainerShip.Classes;

public class ValuableContainer : Container
{

    public ValuableContainer(int weight) : base(ContainerType.Valuable, weight)
    {
        Type = ContainerType.Valuable;
    }
}