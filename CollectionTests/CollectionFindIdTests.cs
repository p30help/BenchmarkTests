using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;

namespace CollectionBenchmark
{
    [MemoryDiagnoser]
    public class CollectionFindIdTests
    {
        static Guid Value_OrderId = Guid.NewGuid();
        static int Value_OrderCode = 100;

        public CollectionFindIdTests()
        {

        }

        [Params(100, 1_000, 10_000, 100_000)]
        public int N;

        List<Order> List = new();
        Dictionary<Guid, Order> Dic = new();
        ConcurrentDictionary<Guid, Order> ConDic = new();
        Guid TargetId = Guid.NewGuid();

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < N; i++)
            {
                var id = Guid.NewGuid();
                if (i == N - 2)
                {
                    id = TargetId;
                }
                var order = new Order()
                {
                    Id = id,
                    Code = new Random().Next(100, 1000)
                };

                List.Add(order);
                Dic.Add(id, order);
                ConDic.TryAdd(id, order);
            }
        }

        [Benchmark]
        public void List_SingleOrDefault()
        {
            var item = List.SingleOrDefault(x => x.Id == TargetId);
        }

        [Benchmark]
        public void List_Single()
        {
            var item = List.SingleOrDefault(x => x.Id == TargetId);
        }

        [Benchmark]
        public void Dictionary_Get()
        {
            var item = Dic[TargetId];
        }

        [Benchmark]
        public void Dictionary_GetWithContainsKey()
        {
            var item = (Dic.ContainsKey(TargetId)) ? Dic[TargetId] : null;
        }

        [Benchmark]
        public void ConcurrentDictionary_Get()
        {
            var item = ConDic[TargetId];
        }

        [Benchmark]
        public void ConcurrentDictionary_GetWithContainsKey()
        {
            var item = (ConDic.ContainsKey(TargetId)) ? ConDic[TargetId] : null;
        }

        public class Order
        {
            public Guid Id { get; set; }
            public int Code { get; set; }
        }

    }


}
