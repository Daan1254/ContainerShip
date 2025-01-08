namespace ContainerShip.Classes;

public class Row
{
    public List<Stack> Stacks { get; set; }

    public Row(int width)
    {
        Stacks = new List<Stack>();
        for (int i = 0; i < width; i++)
        {
            Stacks.Add(new Stack());
        }
    }

    public bool CanAddContainer(Container container)
    {
        return true;
    }

    public void AddContainer(Container container)
    {
        if (this.Stacks.Count == 0)
        {
            this.Stacks.Add(new Stack());
        }

        foreach (Stack stack in this.Stacks)
        {
            if (stack.AddContainer(container))
            {
                return;
            }
        }
    }

    public bool IsEmpty()
    {
        return !this.Stacks.Any();
    }
}