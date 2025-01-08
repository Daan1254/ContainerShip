namespace ContainerShip.Classes;

public class Ship
{
    public int Length { get; private set; }
    public int Width { get; private set; }
    
    public IReadOnlyList<Row> Rows => _rows.AsReadOnly();
    private List<Row> _rows { get; set; }

    public Ship(int length, int width)
    {
        Length = length;
        Width = width;
        _rows = new List<Row>();

        for (int i = 0; i < length; i++)
        {
            _rows.Add(new Row(width));
        }
    }

    // TODO
    public bool isBalanced()
    {
        return true;
    }

    private void AddContainer(Container container)
    {
        bool added = false;
        foreach (Row row in this._rows)
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