using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WIDA.Utes
{
    //The serializer is used to store parameters for each definition within a file
    public class ObjectSerializer
    {
        public XmlElement SerializeObject(object Object, ref XmlDocument Doc)
        {
            XmlElement DataElement = Doc.CreateElement("SerializedData");

            //Store type as fully qualified name in element for deserilization
            XmlElement TypeElement = Doc.CreateElement("Type");
            TypeElement.InnerText = Convert.ToBase64String(Encoding.UTF8.GetBytes(Object.GetType().AssemblyQualifiedName));

            //Serialize the actual object
            XmlElement ObjectElement = Doc.CreateElement("SerializedObject");
            MemoryStream ObjectStream = new MemoryStream();
            DataContractSerializer ObjectSerializer = new DataContractSerializer(Object.GetType());
            ObjectSerializer.WriteObject(ObjectStream, Object);
            ObjectElement.InnerText = Convert.ToBase64String(ObjectStream.ToArray());
            ObjectStream.Close();
            ObjectStream.Dispose();

            //Append elements and return result
            DataElement.AppendChild(TypeElement);
            DataElement.AppendChild(ObjectElement);

            return DataElement;
        }

        public object DeserializeXML(XmlElement DataElement)
        {
            //Get type from name
            XmlElement TypeElement = (XmlElement)DataElement.GetElementsByTagName("Type")[0];
            Type Type = Type.GetType(Encoding.UTF8.GetString(Convert.FromBase64String(TypeElement.InnerText)));

            //Use type to deserialize the object
            XmlElement ObjectElement = (XmlElement)DataElement.GetElementsByTagName("SerializedObject")[0];
            DataContractSerializer ObjectSerializer = new DataContractSerializer(Type);
            MemoryStream Stream = new MemoryStream();
            byte[] Buffer = Convert.FromBase64String(ObjectElement.InnerText);
            Stream.Write(Buffer, 0, Buffer.Length);
            Stream.Seek(0, SeekOrigin.Begin);
            object ReturnObject = ObjectSerializer.ReadObject(Stream);
            Stream.Close();
            Stream.Dispose();

            return ReturnObject;
        }
    }
}
