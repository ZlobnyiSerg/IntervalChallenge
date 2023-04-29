// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using IntervalChallenge;
using IntervalChallenge.Implementations;


BenchmarkRunner.Run<Benchmarks>();

public class Benchmarks
{
    [Benchmark]
    public void BenchmarkLegacy()
    {
        var result = Legacy.Project(TestData.Intervals);
        result.ToList();
    }
}