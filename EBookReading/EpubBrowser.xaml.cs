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
        string prefix = "<head> <meta http-equiv='Content-Type' content='text/html;charset=UTF-8'> <style> stylereplace </style> </head>";
        EpubReader eb;
        public EpubBrowser()
        {
            InitializeComponent();
        }
        public void DisplayBook(string FilePath)
        {            
            eb = new EpubReader();
            eb.CreateBook(FilePath);
            prefix =  LoadCSS(prefix);
            string SecContent = prefix;
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
            
            string chapter = prefix;
            foreach(string s in sections) chapter += s;            
            SectionContent.NavigateToString(chapter);
        }
    }
}
