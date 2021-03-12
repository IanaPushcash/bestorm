using BestORM.Domain;
using FluentNHibernate.Mapping;

namespace BestORM.NHibernate
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.CustomerID);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.IsActive);

            Table("Customers");
        }
    }
}
