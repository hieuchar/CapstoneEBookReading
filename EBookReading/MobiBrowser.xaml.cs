using EBookReading.Mobi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        
        MobiReader mb;
        double CurrentZoom = 1.0;
        int ChapterNumber = 0;
        public MobiBrowser()
        {
            InitializeComponent();            
            mb = new MobiReader();
            NextChapterImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "//Icons//rightarrowicon.png"));
            PrevChapterImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "//Icons//leftarrowicon.png"));
        }
        //Create a MobiBook from a file path and navigate to the html file
        public void DisplayBook(string FilePath)
        {
            mb.CreateBook(FilePath);
            this.Title = mb.GetTitle();            
            LoadToC();
            BookContent.Navigate(mb.GetBookPath());
        }
        private void LoadToC()
        {
            List<string> ToCList = mb.GetToC();
            ToC.ItemsSource = ToCList;
            ToC.MouseDoubleClick += Open_Chapter;
        }
        private void Zoom_In(object sender, EventArgs e)
        {                       
            BookContent.Focus();
            SendKeys.SendWait("^{+}");         
        }
        private void Zoom_Out(object sender, EventArgs e)
        {
            BookContent.Focus();
            SendKeys.SendWait("^-");            
        }
        private void Open_Chapter(object sender, EventArgs e)
        {
            var clicked = ((System.Windows.Controls.ListBox)sender).SelectedValue;            
            BookContent.Navigate(mb.GetBookPath() + mb.GetFilePos(clicked.ToString()));
            ChapterNumber = mb.GetPlayOrder(clicked.ToString());
            
        }
        private void Next_Chapter(object sender, EventArgs e)
        {
            string pos = mb.GetChapter(ref ChapterNumber, 1);
            BookContent.Navigate(mb.GetBookPath() + pos);
        }
        private void Prev_Chapter(object sender, EventArgs e)
        {
            if (ChapterNumber >= 1)
            {
                string pos = mb.GetChapter(ref ChapterNumber, -1);
                BookContent.Navigate(mb.GetBookPath() + pos);
            }
        }
        private void RefreshZoom(Object sender, NavigationEventArgs e)
        {
            mshtml.IHTMLDocument2 doc = BookContent.Document as mshtml.IHTMLDocument2;
            if (doc.body != null) doc.parentWindow.execScript("document.body.style.zoom=" + CurrentZoom.ToString().Replace(",", ".") + ";");
        }
    }
}
    
    

   

