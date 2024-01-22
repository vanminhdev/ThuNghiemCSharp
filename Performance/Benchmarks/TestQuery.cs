using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Filters;
using Microsoft.EntityFrameworkCore;
using Performance.Common;
using Performance.DbContexts;

namespace Performance.Benchmarks
{
    [MemoryDiagnoser]
    public class TestQuery
    {
        [Benchmark]
        public void QueryParam1()
        {
            var dbContext = new ApplicationDbContext();

            int status = ClassroomStatus.Active;
            var test = dbContext.Classrooms.Where(c => c.Status == status).ToList();
        }

        class Filter1
        {
            public int Status { get; set; }
        }

        [Benchmark]
        public void QueryParam2()
        {
            var dbContext = new ApplicationDbContext();

            var filter = new Filter1()
            {
                Status = ClassroomStatus.Active
            };
            var test = dbContext.Classrooms.Where(c => c.Status == filter.Status).ToList();
        }
    }
}
