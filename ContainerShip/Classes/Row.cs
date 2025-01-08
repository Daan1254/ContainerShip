namespace ContainerShip.Classes;

public class Row
{
    private List<Stack> _stacks { get; set; }


    public IReadOnlyList<Stack> Stacks => _stacks.AsReadOnly();

    public Row(int width)
    {
        _stacks = new List<Stack>();
        for (int i = 0; i < width; i++)
        {
            _stacks.Add(new Stack(i == 0));
        }
    }

    public bool AddContainer(Container container)
    {


        // Find stack with acceptable height deviation
        Stack? targetStack = null;
        int avgHeight = 0;

        // Calculate average height
        foreach (Stack stack in _stacks)
        {
            avgHeight += stack.Containers.Count;
        }
        avgHeight /= _stacks.Count;

        // Find stack with smallest height deviation that can accept the container
        double smallestDeviation = double.MaxValue;
        foreach (Stack stack in _stacks)
        {
            if (stack.AddContainer(container))
            {
                // Undo the add since we're just checking
                stack.Containers.RemoveAt(container.IsValuable ? stack.Containers.Count - 1 : 0);

                double deviation = Math.Abs(stack.Containers.Count - avgHeight);
                if (deviation < smallestDeviation)
                {
                    smallestDeviation = deviation;
                    targetStack = stack;
                }
            }
        }

        // Add to the chosen stack if found
        if (targetStack != null)
        {
            return targetStack.AddContainer(container);
        }

        return false;
    }

    public bool IsEmpty()
    {
        foreach (Stack stack in this._stacks)
        {
            if (stack.Containers.Count > 0)
            {
                return false;
            }
        }
        return true;
    }
}