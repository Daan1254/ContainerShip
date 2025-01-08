namespace ContainerShip.Classes;

public class Stack
{
    private readonly int _maxWeight = 120;
    public List<Container> Containers { get; set; }

    public Stack()
    {
        Containers = new List<Container>();
    }

    public bool AddContainer(Container container)
    {
        if ((this.Weight + container.Weight) > this._maxWeight)
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
    
    
    private int Weight
    {
        get
        {
            int weight = 0;
            foreach (Container container in this.Containers)
            {
                weight += container.Weight;
            }
            return weight;
        }
    }
}