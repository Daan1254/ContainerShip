namespace ContainerShip.Classes;

public class Stack
{
    private readonly int _maxWeight = 120;
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
        if (Weight + container.Weight > _maxWeight)
            return false;

        if (container.RequiresCooling && !IsFirstRow)
            return false;

        if (container.IsValuable && Containers.Any(c => c.IsValuable))
            return false;

        return true;
    }

    public bool HasSpaceForWeight(int weight)
    {
        return (Weight + weight) <= _maxWeight;
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