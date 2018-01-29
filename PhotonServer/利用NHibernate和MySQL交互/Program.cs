using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernateMySQL.Model;
using NHibernateMySQL.Manager;

namespace NHibernateMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager userManager = new UserManager();
            //User user = new User() { Username = "程咬金", Password = "diaochan" };
            //userManager.Add(user);
              User user = new User() { ID = 2, Username = "齐天大圣", Password = "sunhouzi" };
            //  userManager.Update(user);
            //  User user = new User() { ID = 3 };
              userManager.Remove(user);
            //User temp = userManager.GetById(user.ID);
            //User temp = userManager.GetByUserName("孙猴子");
            //Console.WriteLine("user.id = " + temp.ID);
            //Console.WriteLine("user.name = " + temp.Username);
            if(userManager.VerifyUser("程咬金", "diaochan"))
            {
                Console.WriteLine("true");
            }
            Console.ReadKey();
        }
       
    }
}
