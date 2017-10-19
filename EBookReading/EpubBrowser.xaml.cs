using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EBookReading.Epub
{
    /// <summary>
    /// Interaction logic for EpubBrowser.xaml
    /// </summary>
    public partial class EpubBrowser : Window
    {
        string prefix = "<head> <meta http-equiv='Content-Type' content='text/html;charset=UTF-8'> </head>";
        EpubReader eb;
        public EpubBrowser()
        {
            InitializeComponent();
        }
        public void DisplayBook(string FilePath)
        {            
            eb = new EpubReader();
            eb.CreateBook(FilePath);
            string SecContent = prefix;
            List<string> firstsection = eb.GetFirstChapter();
            foreach (string s in firstsection) SecContent += s;
            SectionContent.NavigateToString(SecContent);
            LoadToC();           
        }
        private void LoadToC()
        {
            List<string> ToCList = eb.GetToC();
            ToC.ItemsSource = ToCList;
            ToC.MouseDoubleClick += Open_Chapter;
        }
        private void Open_Chapter(object sender, EventArgs e)
        {
            var clicked = ((ListBox)sender).SelectedValue;
            List<string> sections = eb.GetChapter(clicked.ToString());
            string chapter = prefix;
            foreach(string s in sections) chapter += s;            
            SectionContent.NavigateToString(chapter);
        }
    }
}
