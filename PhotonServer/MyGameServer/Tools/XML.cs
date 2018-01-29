using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyGameServer.Tools
{
    class XML
    {
        public static string Serializer<T>(T obj)
        {
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(sw, obj);
            sw.Close();
            return sw.ToString();
        }
    }
}