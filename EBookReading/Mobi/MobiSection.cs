using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading.Mobi
{
    class MobiSection
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
            get { return _playOrder; }
            set { _playOrder = value; }
        }
        public MobiSection()
        {
            Paragraph = new List<string>();
        }
    }
}

