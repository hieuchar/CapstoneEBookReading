using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EBookReading.Epub
{
    class Spine
    {
        private List<string> _itemRef;
        private string _toc;

        public List<string> ItemRef
        {
            get { return _itemRef; }
            set { _itemRef = value; }
        }

        public string toc
        {
            get { return _toc; }
            set { _toc = value; }
        }

        public Spine()
        {
            ItemRef = new List<string>();
        }

        public void Initialize(List<string> itemRef)
        {
            foreach (string ir in itemRef)
            {
                ItemRef.Add(ir);
            }
        }
       
        public void ClearSpine()
        {
            ItemRef.Clear();
        }

        public static void BuildSpine(ref Container container, string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlAttributeCollection xmlAttributeCollection;
            XmlNodeList xmlNodeList;

            xmlDocument.Load(Path.Combine(path, container.FullPath));

            //As before, we're searching all the tags for one containing "spine". We're going to take the toc attribute of that
            //tag and assign it to the Spine.toc property. Then we'll get all the tags within the <spine> tag

            xmlNodeList = xmlDocument.GetElementsByTagName("*");
            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.Contains("spine"))
                {
                    xmlAttributeCollection = element.Attributes;
                    foreach (XmlAttribute attribute in xmlAttributeCollection)
                    {
                        if (attribute.Name == "toc")
                        {
                            container.ContentOPF.ContentSpine.toc = attribute.InnerText;
                            break;
                        }
                    }
                    xmlNodeList = element.GetElementsByTagName("*");
                    break;
                }
            }

            //Now, look through these subtags within <spine>, and we're going to pull the idref attribute out of all
            //the <itemref> tags, and add it to the List<string> within Spine.

            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.Contains("itemref"))
                {
                    xmlAttributeCollection = element.Attributes;
                    foreach (XmlAttribute attribute in xmlAttributeCollection)
                    {
                        if (attribute.Name == "idref")
                        {
                            container.ContentOPF.ContentSpine.ItemRef.Add(attribute.InnerText);
                            continue;
                        }
                    }
                }
            }
        }
    }
}
