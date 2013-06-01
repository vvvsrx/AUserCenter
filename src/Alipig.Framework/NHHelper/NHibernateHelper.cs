using NHibernate.Cfg;
using NHibernate;
namespace Alipig.Framework.NHHelper
{
    public class NHibernateHelper
    {
        private static object locker = new object();
        private static Configuration configuration = null;
        private static ISessionFactory sessionFactory = null;
        public static ISessionStorage sessionStorage { get; set; }





        public static Configuration Configuration
        {
            get
            {
                lock (locker)
                {
                    if (configuration == null)
                    {
                        //CreateConfiguration();
                        configuration = new ConfigurationBuilder().Build();
                    }
                    return configuration;
                }
            }
            set { configuration = value; }
        }


        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    //if (Configuration == null)
                    //{
                    //    CreateConfiguration();
                    //}
                    lock (locker)
                    {
                        sessionFactory = Configuration.BuildSessionFactory();
                    }
                    //var nhConfig = new ConfigurationBuilder().Build();
                    //sessionFactory = nhConfig.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        public static ISession CreateSession()
        {
            ISession s = sessionStorage.Get();
            if (s == null)
            {
                s = SessionFactory.OpenSession();
                sessionStorage.Set(s);
            }
            return s;
        }
    }
}
