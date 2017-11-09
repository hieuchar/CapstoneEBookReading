using EBookReading.Mobi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EBookReading
{
    /// <summary>
    /// Interaction logic for MobiBrowser.xaml
    /// </summary>
    public partial class MobiBrowser : Window
    {
        string Prefix = "<html> <head> <meta http-equiv='content-type' content='text/html; charset=UTF-8'/> </head>";
        MobiReader mb;
        double CurrentZoom = 1.0;
        int ChapterNumber = 0;
        public MobiBrowser()
        {
            InitializeComponent();
            mb = new MobiReader();
        }
        //Create a MobiBook from a file path and load its contents
        public void DisplayBook(string FilePath)
        {
            mb.CreateBook(FilePath);
            List<string> text = mb.GetChapter(ref ChapterNumber, 0);
            string temp = Prefix;
            foreach(string s in text)
            {
                temp += s;
            }
            temp += "</html>";
            LoadToC();
            BookContent.NavigateToString(temp);
        }
        private void LoadToC()
        {
            List<string> ToCList = mb.GetToC();
            ToC.ItemsSource = ToCList;
            ToC.MouseDoubleClick += Open_Chapter;
        }
        private void Zoom_In(object sender, EventArgs e)
        {
            mshtml.IHTMLDocument2 doc = BookContent.Document as mshtml.IHTMLDocument2;
            CurrentZoom += .1;
            doc.parentWindow.execScript("document.body.style.zoom=" + CurrentZoom.ToString().Replace(",", ".") + ";");
        }
        private void Zoom_Out(object sender, EventArgs e)
        {
            mshtml.IHTMLDocument2 doc = BookContent.Document as mshtml.IHTMLDocument2;
            CurrentZoom -= .1;
            doc.parentWindow.execScript("document.body.style.zoom=" + CurrentZoom.ToString().Replace(",", ".") + ";");
        }
        private void Open_Chapter(object sender, EventArgs e)
        {
            var clicked = ((ListBox)sender).SelectedValue;
            List<string> sections = mb.GetChapter(clicked.ToString());
            LoadChapter(sections);
            ChapterNumber = mb.GetPlayOrder(clicked.ToString());

        }
        private void Next_Chapter(object sender, EventArgs e)
        {
            List<string> sections = mb.GetChapter(ref ChapterNumber, 1);
            LoadChapter(sections);
        }
        private void Prev_Chapter(object sender, EventArgs e)
        {

            if (ChapterNumber > 1)
            {
                List<string> sections = mb.GetChapter(ref ChapterNumber, -1);
                LoadChapter(sections);
            }
        }
        /// <summary>
        /// Loads the chapter into the wmb browser
        /// </summary>
        /// <param name="text"></param>
        private void LoadChapter(List<string> text)
        {
            string chapter = Prefix;
            foreach (string s in text) chapter += s;
            BookContent.NavigateToString(chapter);

        }
        private void RefreshZoom(Object sender, NavigationEventArgs e)
        {
            mshtml.IHTMLDocument2 doc = BookContent.Document as mshtml.IHTMLDocument2;
            if (doc.body != null) doc.parentWindow.execScript("document.body.style.zoom=" + CurrentZoom.ToString().Replace(",", ".") + ";");
        }
    }
}
    
    

   

