using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernateMySQL.Model;


namespace NHibernateMySQL
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
                    configuration.Configure("nhibernate.cfg.xml");//解析 nhibernate.cfg.xml文件，要求此文件要打包到.exe中
                    configuration.AddAssembly("NHibernateMySQL");//解析映射文件 User.hbm.xml

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
