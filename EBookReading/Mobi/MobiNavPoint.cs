using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading.Mobi
{
    class MobiNavPoint
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

        //Unique identifier for the Mobi document
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string Src
        {
            get { return _src; }
            set { _src = value; }
        }

        public MobiNavPoint()
        {
        }

        public MobiNavPoint(int playOrder, string id, string text, string src)
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