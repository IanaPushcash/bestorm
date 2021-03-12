using BestORM.Dapper;
using BestORM.Domain;
using BestORM.Ef;
using BestORM.Linq2Db;
using BestORM.NHibernate;
using System;
using System.Collections.Generic;

namespace BestORM
{
    class Program
    {
		public static string ConnectionString = @"Server=.;Database=BestORM;Trusted_Connection=True;MultipleActiveResultSets=true";
		public static List<BenchmarkResult> BenchmarkResults = new List<BenchmarkResult>();
        static void Main(string[] args)
        {
            var customers = GenerateCustomers(1000);
			var efService = new EfService();

			efService.AddRange(BenchmarkResults, customers);
			efService.Filtering(BenchmarkResults, customers);
			efService.UpdateRange(BenchmarkResults, customers);
			efService.RemoveRange(BenchmarkResults, customers);

			customers = GenerateCustomers(1000);
			var dapperService = new DapperService();

			customers = dapperService.AddRange(BenchmarkResults, customers);
			dapperService.Filtering(BenchmarkResults, customers);
			customers = dapperService.UpdateRange(BenchmarkResults, customers);
			dapperService.RemoveRange(BenchmarkResults, customers);

			customers = GenerateCustomers(1000);
			var linq2DbService = new Linq2DbService();

			customers = linq2DbService.AddRange(BenchmarkResults, customers);
			linq2DbService.Filtering(BenchmarkResults, customers);
			customers = linq2DbService.UpdateRange(BenchmarkResults, customers);
			linq2DbService.RemoveRange(BenchmarkResults, customers);

			customers = GenerateCustomers(1000);
			var nhService = new NHibernateService();

			customers = nhService.AddRange(BenchmarkResults, customers);
			nhService.Filtering(BenchmarkResults, customers);
			customers = nhService.UpdateRange(BenchmarkResults, customers);
			nhService.RemoveRange(BenchmarkResults, customers);

			Console.Clear();
			foreach (var result in BenchmarkResults)
			{
				Console.WriteLine($"{result.Action}\t {result.Entities}\t {result.Performance}");
			}
		}
		public static List<Customer> GenerateCustomers(int count)
		{
			var list = new List<Customer>();

			for (int i = 0; i < count; i++)
			{
				list.Add(new Customer() { Name = "Customer_" + i, Description = "Description_" + i, IsActive = i % 2 == 0 });
			}

			return list;
		}
	}
}
