using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EBookReading.Mobi
{
    class MobiContent
    {
        private string _title;
        private string _author;
        private string _language;
        private string _id;
        private string _date;
        private string _publisher;
        private string _subject;
        private string _source;
        private string _rights;
        private MobiManifest _contentManifest;
        private MobiSpine _contentSpine;
        private MobiToC _contentToC;
        

        public MobiContent()
        {
            ContentManifest = new MobiManifest();
            ContentSpine = new MobiSpine();
            ContentToC = new MobiToC();
            
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        public string Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }
        public MobiManifest ContentManifest
        {
            get { return _contentManifest; }
            set { _contentManifest = value; }
        }
        public MobiSpine ContentSpine
        {
            get { return _contentSpine; }
            set { _contentSpine = value; }
        }
        public MobiToC ContentToC
        {
            get { return _contentToC; }
            set { _contentToC = value; }
        }
        public static void GetMetadataFromContent(ref MobiContainer container, string FilePath)
        {
            XmlNodeList xmlNodeList;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.XmlResolver = null;
            //Replace all <br> tags which cause the xml reader to break with <br/> tags
            File.WriteAllText(FilePath, File.ReadAllText(FilePath).Replace("<br>", "<br/>"));
            XmlReader xmlReader = XmlReader.Create(FilePath, settings);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);

            xmlNodeList = xmlDocument.GetElementsByTagName("*");
            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.ToLower().Contains("metadata"))
                {                
                    xmlNodeList = element.GetElementsByTagName("*");
                    break;
                }
            }

            foreach (XmlElement element in xmlNodeList)
            {
                var contentOPF = container.ContentOPF;
                if (element.Name.ToLower().Contains("title"))
                {
                    contentOPF.Title = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("creator"))
                {
                    contentOPF.Author = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("language"))
                {
                    contentOPF.Language = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("identifier"))
                {
                    contentOPF.ID = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("date"))
                {
                    contentOPF.Date = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("publisher"))
                {
                    contentOPF.Publisher = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("subject"))
                {
                    contentOPF.Subject = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("source"))
                {
                    contentOPF.Source = element.InnerText;
                    continue;
                }
                if (element.Name.ToLower().Contains("rights"))
                {
                    contentOPF.Rights = element.InnerText;
                    continue;
                }

            }
        }

    }
}

