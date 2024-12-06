namespace ContainerShip.Classes;

public abstract class Container
{
    public int Weight { get; set; } = 4;
    public int MaxWeight { get; set; } = 30;
    public bool IsValuable { get; set; } 
    public bool RequiresCooling { get; set; }

    public int posititionX { get; set; }
    
    public int posititionY { get; set; }
    
    public int posititionZ { get; set; }


    public virtual bool CanSupport(Container container)
    {
        return true;
    }
}