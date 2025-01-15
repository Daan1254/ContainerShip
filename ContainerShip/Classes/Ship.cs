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

        for (int i = 0; i < width; i++)
        {
            _rows.Add(new Row(length));
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

        return difference <= 20;
    }

    private void AddContainer(Container container)
    {
        // Find all rows that can potentially accept this container
        List<Row> eligibleRows = _rows.Where(row =>
            row.HasSpaceFor(container) &&
            (!container.RequiresCooling || row.HasAvailableCooledSpace(container))
        ).ToList();

        if (!eligibleRows.Any())
        {
            Console.WriteLine($"[WARNING] Container {container.Type} could not be added to ship - no eligible rows");
            return;
        }

        // Sort rows by their total weight to find the least loaded rows
        List<Row> sortedRows = eligibleRows.OrderBy(row => row.TotalWeight).ToList();

        // Try to add to the least loaded row first
        foreach (Row row in sortedRows)
        {
            if (row.AddContainer(container))
                return;
        }

        Console.WriteLine($"[WARNING] Container {container.Type} could not be added to ship despite finding eligible rows");
    }

    private void IsProperlyLoaded()
    {
        bool isProperlyLoaded = true;
        int maxWeight = Length * Width * (Stack.MaxWeight + Container.MaxWeight);
        int totalWeight = _rows.Sum(row => row.TotalWeight);

        if (totalWeight <= 0.5 * maxWeight)
        {
            Console.WriteLine("[WARNING] Ship is not properly loaded - less than 50% of the ship is filled");
            isProperlyLoaded = false;
        }


        if (!this.isBalanced())
        {
            Console.WriteLine("[WARNING] Ship is not properly loaded - not balanced");
            isProperlyLoaded = false;
        }

        if (isProperlyLoaded)
        {
            Console.WriteLine("[INFO] Ship is properly loaded");
        }
    }


    public void ArrangeContainers(List<Container> containers)
    {
        containers = containers
            .OrderBy(container => container.Type != ContainerType.Coolable)
            .ThenBy(container => container.Type != ContainerType.ValuableCoolable)
            .ThenBy(container => container.Type != ContainerType.Regular)
            .ThenBy(container => container.Type != ContainerType.Valuable)
            .ToList();

        Console.WriteLine($"[INFO] Arranging {containers.Count} containers");
        foreach (Container container in containers)
        {
            this.AddContainer(container);
        }

        this.IsProperlyLoaded();
    }
}