namespace ContainerShip.Classes;

public class Row
{
    private List<Stack> _stacks { get; set; }

    public IReadOnlyList<Stack> Stacks => _stacks.AsReadOnly();

    public int TotalWeight => _stacks.Sum(stack => stack.Weight);

    public Row(int length)
    {
        _stacks = new List<Stack>();
        for (int i = 0; i < length; i++)
        {
            _stacks.Add(new Stack(i == 0));
        }
    }

    public bool HasSpaceFor(Container container)
    {
        return _stacks.Any(stack => stack.CanAdd(container));
    }

    public bool HasAvailableCooledSpace(Container container)
    {
        return _stacks.First().HasSpaceForCoolable(container); // First stack is always cooled
    }

    public bool AddContainer(Container container)
    {
        if (container.IsValuable)
        {
            List<Stack> eligibleStacks = _stacks
                .Where((s, index) =>
                {
                    if (!s.CanAdd(container)) return false;

                    // Only check reachability for valuable containers
                    if (!IsStackReachable(index)) return false;

                    return true;
                })
                .OrderBy(s => s.Containers.Count)
                .ToList();

            if (eligibleStacks.Any() && eligibleStacks.First().AddContainer(container))
                return true;

            return false;
        }

        // For regular containers, check if adding them would block any valuable containers
        List<Stack> sortedStacks = _stacks
            .Where((s, index) =>
            {
                if (!s.CanAdd(container)) return false;

                // Temporarily add the container to check if it blocks any valuable containers
                s.Containers.Insert(0, container);

                // Check if any valuable containers in adjacent stacks become unreachable
                bool blocksValuable = false;
                if (index > 0 && HasValuableContainers(_stacks[index - 1]))
                {
                    blocksValuable = !IsStackReachable(index - 1);
                }
                if (index < _stacks.Count - 1 && HasValuableContainers(_stacks[index + 1]))
                {
                    blocksValuable = blocksValuable || !IsStackReachable(index + 1);
                }

                // Remove the temporary container
                s.Containers.RemoveAt(0);

                return !blocksValuable;
            })
            .OrderBy(s => s.Containers.Count)
            .ToList();

        foreach (var stack in sortedStacks)
        {
            if (stack.AddContainer(container))
                return true;
        }

        return false;
    }

    private bool IsStackReachable(int index)
    {
        return index == 0 || index == _stacks.Count - 1;
    }

    private bool HasValuableContainers(Stack stack)
    {
        return stack.Containers.Any(c => c.IsValuable);
    }

    public bool IsEmpty()
    {
        return _stacks.All(stack => stack.Containers.Count == 0);
    }
}