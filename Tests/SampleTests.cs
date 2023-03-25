using NUnit.Framework;

namespace dotnet_angular_starter.Tests;

public class SampleTeststs
{
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public void SampleTest()
    {
        // Arrange
        var value1 = 2;
        var value2 = 3;

        // Act
        var result = value1 + value2;

        // Assert
        Assert.That(result, Is.EqualTo(5));
    }
}