using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EBookReading.Epub
{
    class NavPoint
    {
        private int _playOrder;
        private string _id;
        private string _text;
        private string _src;

        //Order in which the navpoint appears in the Table of Contents
        public int PlayOrder
        {
            get { return _playOrder; }
            set { _playOrder = value; }
        }

        //Unique identifier for the ePub document
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        //aka navLabel in XML document. Title of the navpoint in the Table of Contents
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        //the path/name of the file referenced
        public string Src
        {
            get { return _src; }
            set 
            {
                value = value.Contains('#') ? value.Substring(0, value.LastIndexOf('#')) : value;
                //value = (value.Contains('/') || value.Contains('\\')) ? value.Substring(value.LastIndexOfAny(new char[] {'\\', '/'}) + 1, value.Length - value.LastIndexOfAny(new char[] {'\\', '/'})) : value;
                if (value.Contains('/'))
                {
                    string[] split = value.Split('/');
                    value = split.Last<string>();
                }
                else if(value.Contains('\\'))
                {
                    string[] split = value.Split('\\');
                    value = split.Last<string>();
                }
                _src = value; 
            }
        }

        public NavPoint()
        {
        }

        public NavPoint(int playOrder, string id, string text, string src)
        {
            PlayOrder = playOrder;
            ID = id;
            Text = text;
            Src = src;
        }

        public void Initialize(int playOrder, string id, string text, string src)
        {
            PlayOrder = playOrder;
            ID = id;
            Text = text;
            Src = src;
        }
    }
}
