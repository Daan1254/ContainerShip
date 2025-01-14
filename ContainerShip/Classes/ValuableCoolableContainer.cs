namespace ContainerShip.Classes;

public class ValuableCoolableContainer : Container
{
    public ValuableCoolableContainer(int weight) : base(ContainerType.ValuableCoolable, weight)
    {
        Type = ContainerType.ValuableCoolable;
    }
}