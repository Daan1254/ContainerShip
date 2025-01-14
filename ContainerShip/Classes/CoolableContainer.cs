namespace ContainerShip.Classes;

public class CoolableContainer : Container
{
    public CoolableContainer(int weight) : base(ContainerType.Coolable, weight)
    {
        RequiresCooling = true;
        Type = ContainerType.Coolable;
    }
}