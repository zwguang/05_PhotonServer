using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateMySQL.Model;
using NHibernate;
using NHibernate.Criterion;

namespace NHibernateMySQL.Manager
{
    class UserManager : IUserManager
    {
        public void Add(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())//using之后session自动关闭
            {
                using (ITransaction transation = session.BeginTransaction())
                {
                    session.Save(user);
                    transation.Commit();
                }
            }
        }

        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())//using之后session自动关闭
            {
                IList<User> userList = session.CreateCriteria(typeof(User))
                                       .List<User>();
                return userList;
            }
        }

        public User GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())//using之后session自动关闭
            {
                {
                    User tempuser = session.Get<User>(id);
                    return tempuser;
                }
            }
        }

        public User GetByUserName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())//using之后session自动关闭
            {
                {
                    User user = session
                        .CreateCriteria(typeof(User))
                        .Add(Restrictions.Eq("Username", name))
                        .UniqueResult<User>();

                    return user;
                }
            }
        }

        public void Remove(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())//using之后session自动关闭
            {
                using (ITransaction transation = session.BeginTransaction())
                {
                    session.Delete(user);
                    transation.Commit();
                }
            }
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())//using之后session自动关闭
            {
                using (ITransaction transation = session.BeginTransaction())
                {
                    session.Update(user);
                    transation.Commit();
                }
            }
        }

        public bool VerifyUser(string name, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())//using之后session自动关闭
            {
                User user = session
                        .CreateCriteria(typeof(User))
                        .Add(Restrictions.Eq("Username", name))
                        .Add(Restrictions.Eq("Password", password))
                        .UniqueResult<User>();
                if(user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
