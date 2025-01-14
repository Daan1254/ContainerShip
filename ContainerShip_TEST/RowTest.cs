using ContainerShip.Classes;
using NUnit.Framework;

namespace ContainerShip_TEST;

[TestFixture]
public class RowTest
{
    [Test]
    public void Constructor_CreatesCorrectNumberOfStacks()
    {
        // Arrange & Act
        Row row = new Row(3);

        // Assert
        Assert.That(row.Stacks.Count, Is.EqualTo(3));
        Assert.That(row.Stacks[0].IsFirstRow, Is.True);
        Assert.That(row.Stacks[1].IsFirstRow, Is.False);
        Assert.That(row.Stacks[2].IsFirstRow, Is.False);
    }

    [Test]
    public void TotalWeight_EmptyRow_ReturnsZero()
    {
        // Arrange
        Row row = new Row(3);

        // Assert
        Assert.That(row.TotalWeight, Is.EqualTo(0));
    }

    [Test]
    public void TotalWeight_WithContainers_ReturnsCorrectSum()
    {
        // Arrange
        Row row = new Row(3);
        row.AddContainer(new RegularContainer(16));
        row.AddContainer(new RegularContainer(16));
        row.AddContainer(new RegularContainer(16));
        row.AddContainer(new RegularContainer(6));

        // Assert
        Assert.That(row.TotalWeight, Is.EqualTo(70));
    }

    [Test]
    public void HasSpaceFor_RegularContainer_ReturnsTrue()
    {
        // Arrange
        Row row = new Row(3);
        RegularContainer container = new RegularContainer(15);

        // Act & Assert
        Assert.That(row.HasSpaceFor(container), Is.True);
    }

    [Test]
    public void HasAvailableCooledSpace_FirstStackEmpty_ReturnsTrue()
    {
        // Arrange
        Row row = new Row(3);
        CoolableContainer coolableContainer = new CoolableContainer(15);

        // Act & Assert
        Assert.That(row.HasAvailableCooledSpace(coolableContainer), Is.True);
    }

    [Test]
    public void AddContainer_ValuableContainerInAccessibleStack_ReturnsTrue()
    {
        // Arrange
        Row row = new Row(3);
        ValuableContainer valuableContainer = new ValuableContainer(15);

        // Act
        bool result = row.AddContainer(valuableContainer);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(row.Stacks[0].Containers.Contains(valuableContainer), Is.True);
    }

    [Test]
    public void AddContainer_ValuableContainerInMiddleStack_ReturnsFalse()
    {
        // Arrange
        Row row = new Row(3);
        List<Container> containers = new List<Container>(){
            new ValuableContainer(26),
            new ValuableContainer(26),
        };

        // Fill first stack to force trying middle stack
        containers.ForEach(c => row.AddContainer(c));

        // Act
        bool result = row.AddContainer(new ValuableContainer(26));

        // Assert
        Assert.That(result, Is.False);
    }


    [Test]
    public void AddContainer_RegularContainerNotBlockingValuable_ReturnsTrue()
    {
        // Arrange
        Row row = new Row(3);
        RegularContainer regularContainer = new RegularContainer(26);

        // Act
        bool result = row.AddContainer(regularContainer);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsEmpty_NewRow_ReturnsTrue()
    {
        // Arrange
        Row row = new Row(3);

        // Act & Assert
        Assert.That(row.IsEmpty(), Is.True);
    }

    [Test]
    public void IsEmpty_WithContainers_ReturnsFalse()
    {
        // Arrange
        Row row = new Row(3);
        row.AddContainer(new RegularContainer(15));

        // Act & Assert
        Assert.That(row.IsEmpty(), Is.False);
    }

    [Test]
    public void AddContainer_PrefersShorterStacks()
    {
        // Arrange
        Row row = new Row(3);
        RegularContainer firstContainer = new RegularContainer(15);
        RegularContainer secondContainer = new RegularContainer(15);

        // Add container to first stack
        row.Stacks[0].AddContainer(new RegularContainer(15));

        // Act
        row.AddContainer(firstContainer);
        row.AddContainer(secondContainer);

        // Assert
        Assert.That(row.Stacks[1].Containers.Count, Is.EqualTo(1));
        Assert.That(row.Stacks[2].Containers.Count, Is.EqualTo(1));
    }
}
