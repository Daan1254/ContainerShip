namespace ContainerShip.Classes;

public class Stack
{
    public static readonly int MaxWeight = 120;
    public bool IsFirstRow { get; set; }
    public List<Container> Containers { get; set; }
    public int Weight => Containers.Sum(c => c.Weight);

    public Stack(bool isFirstRow)
    {
        IsFirstRow = isFirstRow;
        Containers = new List<Container>();
    }

    public bool CanAdd(Container container)
    {
        if (container.IsValuable)
            if (Containers.Skip(1).Sum(c => c.Weight) + container.Weight > MaxWeight)
                return false;

        if (!container.IsValuable)
            if (Weight + container.Weight > MaxWeight)
                return false;

        if (container.RequiresCooling && !IsFirstRow)
            return false;


        if (container.IsValuable && Containers.Any(c => c.IsValuable))
            return false;

        return true;
    }

    public bool HasSpaceForCoolable(Container container)
    {
        return this.CanAdd(new CoolableContainer(0));
    }

    public bool AddContainer(Container container)
    {
        if (!CanAdd(container))
            return false;

        if (container.IsValuable)
        {
            // Valuable containers always go on top
            Containers.Add(container);
        }
        else
        {
            // Regular containers go at the bottom
            Containers.Insert(0, container);
        }

        return true;
    }
}