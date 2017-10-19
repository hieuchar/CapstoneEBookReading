using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EBookReading.Epub
{
    class Section
    {
        private List<string> _paragraph;
        private int _playOrder;
        public List<string> Paragraph
        {
            get { return _paragraph; }
            set { _paragraph = value; }
        }
        public int PlayOrder
        {
            get { return _playOrder;  }
            set { _playOrder = value; }
        }
        public Section()
        {
            Paragraph = new List<string>();
        }                      
    }
}
