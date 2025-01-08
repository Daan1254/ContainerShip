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
        foreach (Stack stack in _stacks)
        {
            if (stack.AddContainer(container))
            {
                return true;
            }
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