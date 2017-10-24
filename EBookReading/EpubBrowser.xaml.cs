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
using mshtml;

namespace EBookReading.Epub
{
    /// <summary>
    /// Interaction logic for EpubBrowser.xaml
    /// </summary>
    public partial class EpubBrowser : Window
    {
        string Prefix = "<head> <meta http-equiv='Content-Type' content='text/html;charset=UTF-8'> <style> stylereplace </style> </head>";
        EpubReader eb;
        int ChapterNumber;
        public EpubBrowser()
        {
            InitializeComponent();
        }
        public void DisplayBook(string FilePath)
        {            
            eb = new EpubReader();
            eb.CreateBook(FilePath);
            Prefix =  LoadCSS(Prefix);
            string SecContent = Prefix;
            List<string> firstsection = eb.GetFirstChapter();
            foreach (string s in firstsection) SecContent += s;
            SectionContent.NavigateToString(SecContent);
            
            LoadToC();           
        }
        private string LoadCSS(string input)
        {          
            string res = "";
            var z = eb.GetCSS();
            foreach(string s in z)
            {
                res += s;
            }
            input = input.Replace("stylereplace", res);
            return input;
            //HTMLDocument CurrentDocument = x.DomDocument;
            //IHTMLStyleSheet styleSheet = CurrentDocument.createStyleSheet("", 0);
            //StreamReader streamReader = new StreamReader(@"..\browser.css"); //browser.css is Stylesheet file
            //string text = streamReader.ReadToEnd();
            //streamReader.Close();
            //styleSheet.cssText = text;
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
            LoadChapter(sections);
            ChapterNumber = eb.GetPlayOrder(clicked.ToString());
        }
        private void Next_Chapter(object sender, EventArgs e)
        {
            List<string> sections = eb.GetChapter(ref ChapterNumber, 1);
            LoadChapter(sections);            
        }
        private void Prev_Chapter(object sender, EventArgs e)
        {            
            if (ChapterNumber > 1)
            {
                List<string> sections = eb.GetChapter(ref ChapterNumber, -1 );
                LoadChapter(sections);
            }
        }
        /// <summary>
        /// Loads the chapter into the web browser
        /// </summary>
        /// <param name="text"></param>
        private void LoadChapter(List<string> text)
        {
            string chapter = Prefix;
            foreach (string s in text) chapter += s;
            SectionContent.NavigateToString(chapter);
        }
    }
}
