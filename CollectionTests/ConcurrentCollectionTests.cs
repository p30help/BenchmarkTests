using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;

namespace CollectionBenchmark
{
    [MemoryDiagnoser]
    public class ConcurrentCollectionTests
    {
        ConcurrentBag<Order> List1 = new();
        CustomConcurrentList<Order> List2 = new();
        static Random random = new Random();

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < 1000; i++)
            {
                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    Code = random.Next(1000),
                };

                List1.Add(order);
                List2.Add(order);
            }
        }

        [Benchmark]
        public async Task ConcurrentBag_Performance()
        {
            var tasks = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => Task1()));
            }

            await Task.WhenAll(tasks);
        }

        [Benchmark]
        public async Task CustomConcurrentList_Performance()
        {
            var tasks = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => Task2()));
            }

            await Task.WhenAll(tasks);

            var items = List2;
        }

        public void Task1()
        {
            for (int i = 0; i < 100; i++)
            {
                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    Code = (i != 25) ? random.Next(2000, 3000) : -1,
                };
                List1.Add(order);
            }

            var item = List1.FirstOrDefault(x => x.Code == -1);
            if (item != null)
            {
                List1.TakeWhile(x => x.Id == item.Id);
            }
        }

        public void Task2()
        {
            for (int i = 0; i < 100; i++)
            {
                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    Code = (i != 25) ? random.Next(2000, 3000) : -1,
                };
                List2.Add(order);
            }

            var item = List2.FirstOrDefault(x => x.Code == -1);
            if (item != null)
            {
                List2.Remove(item);
            }
        }


        public class Order
        {
            public Guid Id { get; set; }
            public int Code { get; set; }
        }

    }


}
