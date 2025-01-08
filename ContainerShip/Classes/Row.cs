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
    
    public bool AddContainer(Container container)
    {
        // TODO: Implement better logic for a more balanced ship
        foreach (Stack stack in this.Stacks)
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
        foreach (Stack stack in this.Stacks)
        {
            if (stack.Containers.Count > 0)
            {
                return false;
            }
        }
        return true;
    }
}