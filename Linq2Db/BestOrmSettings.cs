using BestORM.Domain;
using LinqToDB;
using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace BestORM.Linq2Db
{    
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class BestOrmSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "BestORM",
                        ProviderName = "SqlServer",
                        ConnectionString = Program.ConnectionString
                    };
            }
        }
    }
    public class DbBestOrm : LinqToDB.Data.DataConnection
    {
        public DbBestOrm() : base("BestORM") { }

        public ITable<Customer> Customers => GetTable<Customer>();
    }
}
