﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EBookReading
{
    public abstract class ComicBookAbstract
    {        
        public abstract void CreateBook(string FilePath);
        public abstract Image GetPage(int PageNumber);
        public abstract int GetMaxPage();
        public abstract string GetExtension();
    }
}
