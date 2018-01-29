using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 对应数据库中表的对象  要求变量为virtual
/// </summary>
namespace MyGameServer.Model
{
    class User
    {
        public virtual int ID { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Registedate { get; set; }

    }
}
