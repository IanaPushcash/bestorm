using BestORM.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BestORM.Ef
{
	

	public class EfService : IOrmService
	{
		public List<Customer> AddRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
		{
			var clock = new Stopwatch();
			using (var context = new EntityContext())
			{
				clock.Start();
				context.Customers.AddRange(customers);
				context.SaveChanges();
				clock.Stop();

				benchmarkResults.Add(new BenchmarkResult() { Action = "Ef AddRange\t", Entities = customers.Count, Performance = clock.ElapsedMilliseconds + " ms" });
			}
			return customers;
		}
		public void Filtering(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
		{
			var clock = new Stopwatch();
			using (var context = new EntityContext())
			{
				clock.Start();
				var x = context.Customers.AsNoTracking().Where(c => c.IsActive).ToList();
				clock.Stop();

				benchmarkResults.Add(new BenchmarkResult() { Action = "Ef Where\t", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
			}
		}
		public List<Customer> UpdateRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
		{
			var clock = new Stopwatch();
			using (var context = new EntityContext())
			{
				foreach (var c in customers) c.IsActive = true;
				clock.Start();
				context.UpdateRange(customers);
				context.SaveChanges();
				clock.Stop();

				benchmarkResults.Add(new BenchmarkResult() { Action = "Ef UpdateRange\t", Entities = customers.Count, Performance = clock.ElapsedMilliseconds + " ms" });
			}
			return customers;
		}
		public void RemoveRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
		{
			var clock = new Stopwatch();
			using (var context = new EntityContext())
			{
				clock.Start();
				context.Customers.RemoveRange(customers);
				context.SaveChanges();
				clock.Stop();

				benchmarkResults.Add(new BenchmarkResult() { Action = "Ef RemoveRange\t", Entities = customers.Count, Performance = clock.ElapsedMilliseconds + " ms" });
			}
		}
	}
}
