using ContainerShip.Classes;

namespace ContainerShip_TEST;

public class ContainerTest
{
    [Test]
    public void Constructor_WithValidWeight_ShouldCreateContainer()
    {
        // Arrange & Act
        var container = new RegularContainer(10);

        // Assert
        Assert.That(container.Weight, Is.EqualTo(14)); // 4 (base weight) + 10
        Assert.That(container.Type, Is.EqualTo(ContainerType.Regular));
        Assert.False(container.IsValuable);
        Assert.False(container.RequiresCooling);
    }

    [Test]
    public void Constructor_WithExcessiveWeight_ShouldThrowException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<Exception>(() => new RegularContainer(27));
        Assert.That(exception.Message, Is.EqualTo("Container is too heavy"));
    }

    [Test]
    public void Constructor_RegularContainer_ShouldSetPropertiesCorrectly()
    {
        // Arrange & Act
        Container container = new RegularContainer(10);

        // Assert
        Assert.That(container.Type, Is.EqualTo(ContainerType.Regular));
        Assert.False(container.IsValuable);
        Assert.False(container.RequiresCooling);
    }

    [Test]
    public void Constructor_ValuableContainer_ShouldSetPropertiesCorrectly()
    {
        // Arrange & Act
        Container container = new ValuableContainer(10);

        // Assert
        Assert.That(container.Type, Is.EqualTo(ContainerType.Valuable));
        Assert.True(container.IsValuable);
        Assert.False(container.RequiresCooling);
    }

    [Test]
    public void Constructor_CoolableContainer_ShouldSetPropertiesCorrectly()
    {
        // Arrange & Act
        Container container = new CoolableContainer(10);

        // Assert
        Assert.That(container.Type, Is.EqualTo(ContainerType.Coolable));
        Assert.False(container.IsValuable);
        Assert.True(container.RequiresCooling);
    }

    [Test]
    public void Constructor_ValuableCoolableContainer_ShouldSetPropertiesCorrectly()
    {
        // Arrange & Act
        Container container = new ValuableCoolableContainer(10);

        // Assert
        Assert.That(container.Type, Is.EqualTo(ContainerType.ValuableCoolable));
        Assert.True(container.IsValuable);
        Assert.True(container.RequiresCooling);
    }
}