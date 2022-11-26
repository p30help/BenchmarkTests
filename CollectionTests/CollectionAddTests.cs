using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;

namespace CollectionBenchmark
{
    [MemoryDiagnoser]
    public class CollectionAddTests
    {
        const int RecurtiveTime = 1_000_000;
        static Guid Value_OrderId = Guid.NewGuid();
        static int Value_OrderCode = 100;

        public CollectionAddTests()
        {

        }

        [Benchmark]
        public void AddToList()
        {
            var list = new List<Order>();

            for (int i = 0; i < RecurtiveTime; i++)
            {
                var id = Guid.NewGuid();
                list.Add(new Order()
                {
                    Id = Value_OrderId,
                    Code = Value_OrderCode
                });
            }
        }

        [Benchmark]
        public void AddToConcurrentBag()
        {
            var list = new ConcurrentBag<Order>();

            for (int i = 0; i < RecurtiveTime; i++)
            {
                var id = Guid.NewGuid();
                list.Add(new Order()
                {
                    Id = Value_OrderId,
                    Code = Value_OrderCode
                });
            }
        }

        [Benchmark]
        public void AddToDictionary()
        {
            var dic = new Dictionary<Guid, Order>();

            for (int i = 0; i < RecurtiveTime; i++)
            {

                var id = Guid.NewGuid();
                dic.Add(id, new Order()
                {
                    Id = Value_OrderId,
                    Code = Value_OrderCode
                });
            }
        }

        [Benchmark]
        public void AddToConcurrentDictionary()
        {
            var dic = new ConcurrentDictionary<Guid, Order>();

            for (int i = 0; i < RecurtiveTime; i++)
            {

                var id = Guid.NewGuid();
                dic.TryAdd(id, new Order()
                {
                    Id = Value_OrderId,
                    Code = Value_OrderCode
                });
            }
        }

        [Benchmark]
        public void AddToStack()
        {
            var list = new Stack<Order>();

            for (int i = 0; i < RecurtiveTime; i++)
            {

                var id = Guid.NewGuid();
                list.Push(new Order()
                {
                    Id = Value_OrderId,
                    Code = Value_OrderCode
                });
            }
        }

        [Benchmark]
        public void AddToQueue()
        {
            var list = new Queue<Order>();

            for (int i = 0; i < RecurtiveTime; i++)
            {
                var id = Guid.NewGuid();
                list.Enqueue(new Order()
                {
                    Id = Value_OrderId,
                    Code = Value_OrderCode
                });
            }
        }

        public class Order
        {
            public Guid Id { get; set; }
            public int Code { get; set; }
        }

    }

    
}
