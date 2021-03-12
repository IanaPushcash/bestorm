using BestORM.Domain;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BestORM.Dapper
{
    public class DapperService : IOrmService
    {
        public List<Customer> AddRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var x = new List<Customer>();
            var clock = new Stopwatch();
            using (var conn = new SqlConnection(Program.ConnectionString))
            {
                conn.Open();
                clock.Start();
                conn.Insert(customers);
                clock.Stop();
                x = conn.GetAll<Customer>().ToList();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Dapper AddRange\t", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
            return x;
        }

        public void Filtering(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            using (var conn = new SqlConnection(Program.ConnectionString))
            {
                string sql = "select * from Customers where IsActive = 1";
                conn.Open();
                clock.Start();
                var x = (List<Customer>)conn.Query<Customer>(sql);
                clock.Stop();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Dapper Where\t", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
        }

        public void RemoveRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            using (var conn = new SqlConnection(Program.ConnectionString))
            {
                conn.Open();
                clock.Start();
                conn.Delete(customers);
                clock.Stop();
                var x = conn.GetAll<Customer>().ToList();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Dapper RemoveRange", Entities = customers.Count - x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
        }

        public List<Customer> UpdateRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var x = new List<Customer>();
            var clock = new Stopwatch();
            using (var conn = new SqlConnection(Program.ConnectionString))
            {
                foreach (var c in customers) c.IsActive = true;
                //string sql = "update Customers set IsActive = @IsActive where CustomerID = @CustomerID";
                //conn.Open();
                //conn.Execute(sql, customers);
                conn.Open();
                clock.Start();
                conn.Update(customers);
                clock.Stop();
                x = conn.GetAll<Customer>().ToList();
                benchmarkResults.Add(new BenchmarkResult() { Action = "Dapper UpdateRange", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            }
            return x;
        }
    }
}
