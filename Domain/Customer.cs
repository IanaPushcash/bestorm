using Dapper.Contrib.Extensions;
using System;

namespace BestORM.Domain
{
	[LinqToDB.Mapping.Table(Name = "Customers")]
	public class Customer
	{
		[Key] //Dapper
		[LinqToDB.Mapping.PrimaryKey, LinqToDB.Mapping.Identity]
		public virtual int CustomerID { get; set; }
		[LinqToDB.Mapping.Column(Name = "Name"), LinqToDB.Mapping.NotNull]
		public virtual string Name { get; set; }
		[LinqToDB.Mapping.Column(Name = "Description"), LinqToDB.Mapping.NotNull]
		public virtual string Description { get; set; }
		[LinqToDB.Mapping.Column(Name = "IsActive"), LinqToDB.Mapping.NotNull]
		public virtual bool IsActive { get; set; }
	}
}
