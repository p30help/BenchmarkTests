using BenchmarkDotNet.Running;
using HashBenchmark;

Console.WriteLine("Starting Benchmark...");

var summary = BenchmarkRunner.Run<HashTests>();
