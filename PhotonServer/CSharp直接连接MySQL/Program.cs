using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/// <summary>
/// 引入动态连接库  C:\Program Files (x86)\MySQL\Connector.NET 6.9\Assemblies\v4.5   MySql.Data.dll
/// </summary>
namespace CSharp直接连接MySQL
{
    class Program
    {
        static void Main(string[] args)
        {

            Deal();
            Console.ReadKey();
        }

        static void Deal()
        {
            string connectStr = "server=127.0.0.1; port=3306; database=mygamedb; user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();//建立连接
                Console.WriteLine("已经建立与数据库的连接");

                //查询
                string sql = "select * from users";
                MySqlCommand cmd = new MySqlCommand(sql, conn);//获取sql命令
                //cmd.ExecuteReader();//查询
                //cmd.ExecuteNonQuery();//增删改
                //cmd.ExecuteScalar();//查询，返回一个值
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())//读取下一页数据，成功返回true，失败返回false；
                {
                    //for (int i = 0; i < reader.FieldCount; i++)
                    //{
                    //    Console.WriteLine(reader[i].ToString());
                    //}
                    Console.WriteLine(reader.GetInt32("id") + "  " + reader.GetString("username"));
                }

                ////查询一列
                //string sql = "select count(*) from users";
                //MySqlCommand cmd = new MySqlCommand(sql, conn);//获取sql命令
                //object o = cmd.ExecuteScalar();
                //int count = Convert.ToInt32(o.ToString());
                //Console.WriteLine(count);

                ////插入
                //string sql = "insert into users(username, password, registedate) values('沙僧', '2048','" + DateTime.Now + "')";
                //MySqlCommand cmd = new MySqlCommand(sql, conn);
                //int result = cmd.ExecuteNonQuery();//返回值为受影响的数据的行数

                ////更新
                //string sql = "update users set username = '猪无能' where id = 4";
                //MySqlCommand cmd = new MySqlCommand(sql, conn);
                //int result = cmd.ExecuteNonQuery();//返回值为受影响的数据的行数

                ////删除
                //string sql = "delete from users where id = 5";
                //MySqlCommand cmd = new MySqlCommand(sql, conn);
                //int result = cmd.ExecuteNonQuery();//返回值为受影响的数据的行数

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally//必然执行的语句
            {
                conn.Close();//关闭连接
                Console.WriteLine("已经关闭与数据库的连接");
            }
        }
        
    }
}
