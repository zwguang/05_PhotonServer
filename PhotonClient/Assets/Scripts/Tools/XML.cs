using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

class XML
{
    //反序列化
    public static T Serializer<T>(string XMLstr)
    {
        using (StringReader reader = new StringReader(XMLstr))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T usernameList = (T)serializer.Deserialize(reader);

            return usernameList;
        }
    }

    //序列化
    public static string SerializerToString<T>(T obj)
    {
        StringWriter sw = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(sw, obj);
        sw.Close();
        return sw.ToString();
    }
}
