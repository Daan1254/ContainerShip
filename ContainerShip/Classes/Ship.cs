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
        // If ship is empty, it's balanced
        if (_rows.All(row => row.IsEmpty()))
            return true;

        double leftWeight = 0;
        double rightWeight = 0;
        double totalWeight = 0;

        // Calculate weights for left and right side of the ship
        int midPoint = _rows.Count / 2;
        bool hasMiddleRow = _rows.Count % 2 == 1;

        for (int rowIndex = 0; rowIndex < _rows.Count; rowIndex++)
        {
            var row = _rows[rowIndex];
            double rowWeight = row.Stacks.Sum(stack =>
                stack.Containers.Sum(container => container.Weight));

            totalWeight += rowWeight;

            // Skip middle row for odd-numbered ships
            if (hasMiddleRow && rowIndex == midPoint)
                continue;

            if (rowIndex < midPoint)
            {
                leftWeight += rowWeight;
            }
            else if (!hasMiddleRow || rowIndex > midPoint)
            {
                rightWeight += rowWeight;
            }
        }

        // Skip if ship is empty
        if (totalWeight == 0) return true;

        // Calculate difference percentage based on total weight
        double difference = Math.Abs(leftWeight - rightWeight) / totalWeight * 100;

        Console.WriteLine($"Ship balance - Left: {leftWeight}, Right: {rightWeight}, Difference: {difference}%");

        return difference <= 20;
    }

    private void AddContainer(Container container)
    {
        // Find the row with the lowest height
        Row? lowestRow = null;
        int lowestHeight = int.MaxValue;

        foreach (Row row in _rows)
        {
            int currentHeight = 0;
            foreach (Stack stack in row.Stacks)
            {
                currentHeight = Math.Max(currentHeight, stack.Containers.Count);
            }

            if (currentHeight < lowestHeight)
            {
                lowestHeight = currentHeight;
                lowestRow = row;
            }
        }

        if (lowestRow != null && lowestRow.AddContainer(container))
        {
            return;
        }

        // If we couldn't add to the lowest row, try all rows as fallback
        foreach (Row row in _rows)
        {
            if (row != lowestRow && row.AddContainer(container))
            {
                return;
            }
        }

        Console.WriteLine($"[WARNING] Container {container.Type} could not be added to ship because of lack of space");
    }


    public void ArrangeContainers(List<Container> containers)
    {
        foreach (Container container in containers)
        {
            this.AddContainer(container);
        }

        if (!this.isBalanced())
        {
            Console.WriteLine("[WARNING] Ship is not balanced");
        }
    }
}