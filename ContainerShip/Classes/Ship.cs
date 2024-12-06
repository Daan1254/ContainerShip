namespace ContainerShip.Classes;

public class Ship
{
    public int Lenght { get; set; }
    public int Width { get; set; }
    
    public List<Container> Containers { get; set; }

    public Ship(int length, int width)
    {
        Lenght = length;
        Width = width;
    }
    
    public bool isBalanced()
    {
        return true;
    }

    public bool AddContainer(Container container)
    {
        return true;
    }
    
    
    public void ArrangeContainers()
    {
        
    }
}