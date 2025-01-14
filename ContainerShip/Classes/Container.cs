namespace ContainerShip.Classes;

public enum ContainerType
{
    Regular = 1,
    Valuable = 2,
    Coolable = 3,
    ValuableCoolable = 4
}

public abstract class Container
{
    public int Weight { get; set; } = 4;
    public static int MaxWeight { get; set; } = 30;
    public bool IsValuable { get; set; }
    public bool RequiresCooling { get; set; }

    public ContainerType Type { get; set; } = ContainerType.Regular;

    public Container(ContainerType type, int weight)
    {
        Type = type;

        if (weight > (MaxWeight - Weight))
        {
            throw new Exception("Container is too heavy");
        }

        Weight += weight;

        if (type is ContainerType.Valuable or ContainerType.ValuableCoolable)
        {
            IsValuable = true;
        }

        if (type is ContainerType.Coolable or ContainerType.ValuableCoolable)
        {
            RequiresCooling = true;
        }
    }


}