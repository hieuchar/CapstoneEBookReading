using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Compression;

namespace EBookReading.Epub
{
    class Container
    {
        private Content _contentOPF;
        private string _fullPath;
        private Book _fullBook;

        public Content ContentOPF
        {
            get { return _contentOPF; }
            set { _contentOPF = value; }
        }

        public string FullPath
        {
            get { return _fullPath; }
            set { _fullPath = value; }
        }

        public Book FullBook
        {
            get { return _fullBook; }
            set { _fullBook = value; }
        }

        public Container()
        {
            ContentOPF = new Content();
            FullBook = new Book();
        }

        public static void GetContentFromContainer(ref Container container, ref ZipArchiveEntry xmlfile)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlNodeList;
            XmlAttributeCollection xmlAttributeCollection;
            var Stream = xmlfile.Open();
            xmldoc.Load(Stream);
            //First, open the container XML document for parsing. This file should always be contained
            //in the META-INF subfolder.

            //xmldoc.Load(Path.Combine(path, "META-INF/container.xml"));
            
            //We know that the content file is defined within the <rootfile> tag, as an attribute entitled
            //"full-path". This will cycle through and assign that value to the container's FullPath property.

            xmlNodeList = xmldoc.GetElementsByTagName("rootfile");
            xmlAttributeCollection = xmlNodeList.Item(0).Attributes;
            foreach (XmlAttribute attribute in xmlAttributeCollection)
            {
                switch (attribute.Name)
                {
                    case "full-path":
                        container.FullPath = attribute.Value;
                        break;
                    default:
                        continue;
                }
                break;
            }            
        }    
    }
}
