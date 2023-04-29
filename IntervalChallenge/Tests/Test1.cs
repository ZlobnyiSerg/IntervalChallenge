using System.Buffers;
using FluentAssertions;
using IntervalChallenge.Implementations;
using IntervalChallenge.Models;
using Xunit;

namespace IntervalChallenge.Tests;

public class Test1
{
    [Fact]
    public void TestLegacy()
    {
        // Arrange

        // Act
        var result = Legacy.Project(TestData.Intervals).ToArray();

        // Assert
        result.Count().Should().Be(271);
        result[0].StartDate.Should().Be(new DateTime(2022, 10, 1, 15, 0, 0));
        result[0].EndDate.Should().Be(new DateTime(2022, 10, 2, 12, 0, 0));
        result[0].Items.Count.Should().Be(1);

        result[12].Items.Count().Should().Be(4);
        result[13].Items.Count().Should().Be(9);
        result[14].Items.Count().Should().Be(9);
        result[15].Items.Count().Should().Be(25);
        result[16].Items.Count().Should().Be(25);
    }

    [Fact]
    public void TestSimpleImprover()
    {
        // Arrange

        // Act
        var result = Improver.Project(new [] {
            new Interval(new DateTime(2023, 1, 1), new DateTime(2023, 1, 3)),
            new Interval(new DateTime(2023, 1, 2), new DateTime(2023, 1, 5)),
            new Interval(new DateTime(2023, 1, 4), new DateTime(2023, 1, 6))
            }
            ).ToArray();

        // Assert
        result.Count().Should().Be(5);
        result[0].Items.Length.Should().Be(1);
        result[1].Items.Length.Should().Be(2);
        result[2].Items.Length.Should().Be(1);
        result[3].Items.Length.Should().Be(2);
        result[4].Items.Length.Should().Be(1);
    }

    [Fact]
    public void TestEmptyImprover()
    {
        // Arrange

        // Act
        var result = Improver.Project<Interval>().ToArray();

        // Assert
        result.Length.Should().Be(0);
    }

    [Fact]
    public void TestSingleItemImprover()
    {
        // Arrangek

        // Act
        var result = Improver.Project(new Interval(new DateTime(2023, 1, 1), new DateTime(2023, 1, 3))).ToArray();

        // Assert
        result.Length.Should().Be(1);
        result[0].StartDate.Should().Be(new DateTime(2023, 1, 1));
        result[0].EndDate.Should().Be(new DateTime(2023, 1, 3));
        result[0].Items.Length.Should().Be(1);

    }

    [Fact]
    public void TestImprover()
    {
        // Arrange

        // Act
        var result = Improver.Project(TestData.Intervals).ToArray();

        // Assert
        result.Count().Should().Be(271);
        result[0].StartDate.Should().Be(new DateTime(2022, 10, 1, 15, 0, 0));
        result[0].EndDate.Should().Be(new DateTime(2022, 10, 2, 12, 0, 0));
        result[0].Items.Length.Should().Be(1);

        result[12].Items.Length.Should().Be(4);
        result[13].Items.Length.Should().Be(9);
        result[14].Items.Length.Should().Be(9);
        result[15].Items.Length.Should().Be(25);
        result[16].Items.Length.Should().Be(25);
    }
}