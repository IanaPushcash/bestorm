using BestORM.Domain;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BestORM.NHibernate
{
    public class NHibernateService : IOrmService
    {
        public List<Customer> AddRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            using (var context = FluentNHibernateHelper.OpenSession())
            {
                clock.Start();
                foreach(var c in customers)
                {
                    context.SaveOrUpdate(c);
                }
            }
            clock.Stop();
            benchmarkResults.Add(new BenchmarkResult() { Action = "NH Fluent AddRange", Entities = customers.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            return customers;
        }

        public void Filtering(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            List<Customer> x;    
            using (var context = FluentNHibernateHelper.OpenSession())
            {
                clock.Start();
                x = context.CreateCriteria<Customer>()
                    .Add(Restrictions.Eq("IsActive", true))
                    .List<Customer>().ToList();                
            }
            clock.Stop();
            benchmarkResults.Add(new BenchmarkResult() { Action = "NH Fluent Where HQL", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            ///////////////////////////////////////////////////////////////////////////
            clock = new Stopwatch();
            using (var context = FluentNHibernateHelper.OpenSession())
            {
                clock.Start();
                x = context.Query<Customer>().Where(x => x.IsActive).ToList();
            }
            clock.Stop();
            benchmarkResults.Add(new BenchmarkResult() { Action = "NH Fluent Where LINQ", Entities = x.Count, Performance = clock.ElapsedMilliseconds + " ms" });
        }

        public void RemoveRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            List<Customer> x;
            using (var context = FluentNHibernateHelper.OpenSession())
            {
                clock.Start();
                context.CreateQuery("delete Customer c where c.CustomerID in (:deleteIds)")
                    .SetParameterList("deleteIds", customers.Select(x => x.CustomerID))
                    .ExecuteUpdate();
            }
            clock.Stop();
            using (var context = FluentNHibernateHelper.OpenSession())
            {
                x = context.Query<Customer>().ToList();
            }
            benchmarkResults.Add(new BenchmarkResult() { Action = "NH Fluent RemoveRange", Entities = customers.Count, Performance = clock.ElapsedMilliseconds + " ms" });
        }

        public List<Customer> UpdateRange(List<BenchmarkResult> benchmarkResults, List<Customer> customers)
        {
            var clock = new Stopwatch();
            List<Customer> x;
            using (var context = FluentNHibernateHelper.OpenSession())
            {
                foreach (var c in customers) c.IsActive = true;
                
                clock.Start();                
                foreach (var c in customers)
                {
                    context.Update(c);
                }
            }
            clock.Stop();
            using (var context = FluentNHibernateHelper.OpenSession())
            {
                x = context.Query<Customer>().ToList();
            }
            benchmarkResults.Add(new BenchmarkResult() { Action = "NH Fluent UpdateRange", Entities = customers.Count, Performance = clock.ElapsedMilliseconds + " ms" });
            return x;
        }
    }
}
