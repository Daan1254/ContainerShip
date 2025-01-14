using ContainerShip.Classes;
using NUnit.Framework;

namespace ContainerShip_TEST;

[TestFixture]
public class StackTest
{
    [Test]
    public void Weight_EmptyStack_ReturnsZero()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);

        // Assert
        Assert.That(stack.Weight, Is.EqualTo(0));
    }

    [Test]
    public void Weight_WithContainers_ReturnsCorrectSum()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);
        stack.AddContainer(new RegularContainer(15));
        stack.AddContainer(new RegularContainer(15));

        // Assert
        // add 4 for the base weight of the containers
        Assert.That(stack.Weight, Is.EqualTo(38));
    }

    [Test]
    public void CanAdd_ExceedsMaxWeight_ReturnsFalse()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);
        stack.AddContainer(new RegularContainer(16));
        stack.AddContainer(new RegularContainer(16));
        stack.AddContainer(new RegularContainer(16));
        stack.AddContainer(new RegularContainer(16));
        stack.AddContainer(new RegularContainer(18));

        // Act & Assert
        Assert.That(stack.CanAdd(new RegularContainer(15)), Is.False);
    }

    [Test]
    public void CanAdd_CoolableContainerInFirstRow_ReturnsTrue()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);
        var coolableContainer = new CoolableContainer(15);

        // Act & Assert
        Assert.That(stack.CanAdd(coolableContainer), Is.True);
    }

    [Test]
    public void CanAdd_CoolableContainerNotInFirstRow_ReturnsFalse()
    {
        // Arrange
        var stack = new Stack(isFirstRow: false);
        var coolableContainer = new CoolableContainer(15);

        // Act & Assert
        Assert.That(stack.CanAdd(coolableContainer), Is.False);
    }

    [Test]
    public void CanAdd_ValuableContainer_AddsOnTop()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);
        stack.AddContainer(new RegularContainer(15));
        var valuableContainer = new ValuableContainer(20);

        // Act
        var result = stack.AddContainer(valuableContainer);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(stack.Containers.Last(), Is.EqualTo(valuableContainer));
    }

    [Test]
    public void CanAdd_MultipleValuableContainers_ReturnsFalse()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);
        stack.AddContainer(new ValuableContainer(20));
        var secondValuableContainer = new ValuableContainer(20);

        // Act & Assert
        Assert.That(stack.CanAdd(secondValuableContainer), Is.False);
    }

    [Test]
    public void AddContainer_RegularContainer_AddsAtBottom()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);
        var firstContainer = new RegularContainer(15);
        var secondContainer = new RegularContainer(15);

        // Act
        stack.AddContainer(firstContainer);
        stack.AddContainer(secondContainer);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(stack.Containers[0], Is.EqualTo(secondContainer));
            Assert.That(stack.Containers[1], Is.EqualTo(firstContainer));
        });
    }

    [Test]
    public void HasSpaceForCoolable_FirstRow_ReturnsTrue()
    {
        // Arrange
        var stack = new Stack(isFirstRow: true);

        // Act & Assert
        Assert.That(stack.HasSpaceForCoolable(new CoolableContainer(15)), Is.True);
    }

    [Test]
    public void HasSpaceForCoolable_NotFirstRow_ReturnsFalse()
    {
        // Arrange
        var stack = new Stack(isFirstRow: false);

        // Act & Assert
        Assert.That(stack.HasSpaceForCoolable(new CoolableContainer(15)), Is.False);
    }
}
