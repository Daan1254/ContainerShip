namespace ContainerShip.Classes;

public class Ship
{
    public int Length { get; set; }
    public int Width { get; set; }

    public List<Row> Rows { get; set; }

    public Ship(int length, int width)
    {
        Length = length;
        Width = width;
        Rows = new List<Row>();
        
        for (int i= 0; i < length; i++)
        {
            Rows.Add(new Row(width));
        }
        
    }

    public bool isBalanced()
    {
        return true;
    }

    public void AddContainer(Container container)
    {
        if (this.Rows.Count == 0)
        {
            this.Rows.Add(new Row());
        }

        foreach (Row row in this.Rows)
        {
            if (row.CanAddContainer(container))
            {
                row.AddContainer(container);
                return;
            }
        }
    }


    public void ArrangeContainers(List<Container> containers)
    {
        foreach (Container container in containers)
        {
            this.AddContainer(container);
        }
    }
}