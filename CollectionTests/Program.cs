using BenchmarkDotNet.Running;
using CollectionBenchmark;

Console.WriteLine("Starting Benchmark...");

//var summary = BenchmarkRunner.Run<CollectionAddTests>();
//var summary = BenchmarkRunner.Run<CollectionFindIdTests>();
var summary = BenchmarkRunner.Run<ConcurrentCollectionTests>();