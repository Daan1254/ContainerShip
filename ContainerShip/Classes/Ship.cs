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

        for (int i = 0; i < length; i++)
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
        bool added = false;
        foreach (Row row in this.Rows)
        {
            if (row.AddContainer(container))
            {
                added = true;
                break;
            }
        }

        if (!added)
        {
            Console.WriteLine($"[WARNING] Container {container.Type} could not be added to ship");
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