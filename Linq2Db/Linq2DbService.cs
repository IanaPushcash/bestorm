using BestORM.Domain;
using System;
using LinqToDB;
using LinqToDB.Common;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using LinqToDB.Data;

namespace BestORM.Linq2Db
{
    public class Linq2DbService : IOrmService
    {
        public Linq2DbService()
        {
            LinqToDB.Data.DataConnection.DefaultSettings = new BestOrmSettings();
        }

        public List<Customer> AddRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var x = new List<Customer>();
            var clock = new Stopwatch();
            using (var db = new DbBestOrm())
            {
                clock.Start();
                db.BulkCopy(customers);
                clock.Stop();
                x = (from c in db.Customers
                         select c).ToList();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Linq2Db AddRange", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
            return x;
        }

        public void Filtering(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            using (var db = new DbBestOrm())
            {
                clock.Start();
                var query = from c in db.Customers
                            where c.IsActive
                            select c;
                var x = query.ToList();
                clock.Stop();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Linq2Db Where\t", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
        }

        public void RemoveRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            var ids = customers.Select(x => x.CustomerID);
            using (var db = new DbBestOrm())
            {
                clock.Start();
                db.Customers.Where(c => ids.Contains(c.CustomerID)).Delete();
                clock.Stop();
                var x = (from c in db.Customers
                     select c).ToList();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Linq2Db RemoveRange", Entities = customers.Count - x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
        }

        public List<Customer> UpdateRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var x = new List<Customer>();
            var clock = new Stopwatch();
            using (var db = new DbBestOrm())
            {
                clock.Start();
                db.Customers.Set(p => p.IsActive, true).Update();
                clock.Stop();
                x = (from c in db.Customers
                     select c).ToList();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Linq2Db UpdateRange", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
            return x;
        }
    }
}
