using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace DungeonCrawler
{
    public class XMLManager<T>
    {
        public XMLManager()
        {

        }

        [XmlIgnore]
        public Type Type { get;  set; }

        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T) xml.Deserialize(reader);
            }

            return instance;
        }

        public void Save (string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(Type);
                    xml.Serialize(writer, obj);
                }

                catch(Exception e)
                {
                    Exception iE = e.InnerException;
                }
            }
        }
    }
}
