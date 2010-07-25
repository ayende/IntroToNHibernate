using HibernatingRhinos.Profiler.Appender;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;

namespace IntroToNHibernate
{
	class Program
	{
		static void Main(string[] args)
		{
			// initialize profiler integration
			NHibernateProfiler.Initialize();
			try
			{
				// the session factory is the entry point to NHibernate
				var sessionFactory = new Configuration()
					.Configure("nhibernate.cfg.xml")
					.BuildSessionFactory();

				// the session is what we use to actually 
				using (var session = sessionFactory.OpenSession())
				using (var tx = session.BeginTransaction())
				{

					tx.Commit();
				}

			}
			finally
			{
				// we need this so we wouldn't exit the process
				// before all the work was sent to the profiler
				ProfilerInfrastructure.FlushAllMessages();
			}
		}
	}
}
