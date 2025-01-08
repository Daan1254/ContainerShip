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
        if ((this.Height + container.Weight) > this.MaxWeight)
        {
            return false;
        }
        
        // Check if there is already a valuable container in the stack because we can't have more than one
        if (container.IsValuable)
        {
            if (this.Containers.Find(c => c.IsValuable) == null)
            {
                // always place on top 
                this.Containers.Add(container);
                return true;
            }
            else
            {
                return false;
            }
        }
        // add this container to bottom of stack Regular container
        this.Containers.Insert(0, container);
        return true;
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