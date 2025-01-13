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
        if (Weight + container.Weight > MaxWeight)
            return false;

        if (container.RequiresCooling && !IsFirstRow)
            return false;

        if (container.IsValuable && Containers.Any(c => c.IsValuable))
            return false;

        return true;
    }

    public bool HasSpaceForWeight(int weight)
    {
        return (Weight + weight) <= MaxWeight;
    }

    public bool IsAccessibleAtHeight(int height, Stack? frontNeighbor, Stack? backNeighbor)
    {
        // If we have a neighbor and they have no container at this height, it's accessible
        bool frontClear = frontNeighbor == null || frontNeighbor.Containers.Count <= height;
        bool backClear = backNeighbor == null || backNeighbor.Containers.Count <= height;

        return frontClear || backClear;
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