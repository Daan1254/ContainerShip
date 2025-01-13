namespace ContainerShip.Classes;

public class Row
{
    private List<Stack> _stacks { get; set; }

    public IReadOnlyList<Stack> Stacks => _stacks.AsReadOnly();

    public int TotalWeight => _stacks.Sum(stack => stack.Weight);

    public Row(int width)
    {
        _stacks = new List<Stack>();
        for (int i = 0; i < width; i++)
        {
            _stacks.Add(new Stack(i == 0));
        }
    }

    public bool HasSpaceFor(Container container)
    {
        return _stacks.Any(stack => stack.CanAdd(container));
    }

    public bool HasAvailableCooledSpace()
    {
        return _stacks.First().HasSpaceForWeight(0); // First stack is always cooled
    }

    public bool AddContainer(Container container)
    {
        // For valuable containers, try to distribute them evenly across stacks
        if (container.IsValuable)
        {
            var eligibleStacks = _stacks
                .Where(s => s.CanAdd(container))
                .OrderBy(s => s.Containers.Count)
                .ToList();

            if (eligibleStacks.Any() && eligibleStacks.First().AddContainer(container))
                return true;

            return false;
        }

        // For regular containers, try to fill stacks evenly
        var sortedStacks = _stacks
            .Where(s => s.CanAdd(container))
            .OrderBy(s => s.Containers.Count)
            .ToList();

        foreach (var stack in sortedStacks)
        {
            if (stack.AddContainer(container))
                return true;
        }

        return false;
    }

    public bool IsEmpty()
    {
        return _stacks.All(stack => stack.Containers.Count == 0);
    }
}