using BestORM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestORM
{
	public interface IOrmService
	{
		List<Customer> AddRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers);
		void Filtering(List<BenchmarkResult> benchmarkResults, List<Customer> customers);
		void RemoveRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers);
		List<Customer> UpdateRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers);
	}
}
