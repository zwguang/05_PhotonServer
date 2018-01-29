using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;


namespace MyGameServer
{
    class NHibernateHelper
    {
        private static ISessionFactory m_sessionFactory = null;
        public static ISessionFactory SessionFactory
        {
            get
            {
                if(m_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();//解析 nhibernate.cfg.xml文件，要求始终复制
                    configuration.AddAssembly("MyGameServer");//程序集  解析映射文件 User.hbm.xml

                    m_sessionFactory = configuration.BuildSessionFactory();
                }
                return m_sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            ISession session = SessionFactory.OpenSession();//打开一个与数据库的会话
            return session;
        }
    }
}
