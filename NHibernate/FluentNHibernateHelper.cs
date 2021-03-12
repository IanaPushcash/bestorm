using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace BestORM.NHibernate
{
    public static class FluentNHibernateHelper
    {
        public static ISession OpenSession()
        {
            string connectionString = Program.ConnectionString;
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql7
                  .ConnectionString(connectionString).ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                 .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();

        }
    }
}
