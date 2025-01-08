namespace ContainerShip.Classes;

public class Stack
{
    public int MaxWeight { get; set; }
    public List<Container> Containers { get; set; }

    public Stack()
    {
        MaxWeight = 120;
        Containers = new List<Container>();
    }

    public bool AddContainer(Container container)
    {
        bool success = false;
        if ((this.Height + container.Weight) < this.MaxWeight)
        {
            this.Containers.Add(container);
            success = true;
        }
        return success;
    }
    
    
    private int Height
    {
        get
        {
            int height = 0;
            foreach (Container container in this.Containers)
            {
                height += container.Weight;
            }
            return height;
        }
    }
}