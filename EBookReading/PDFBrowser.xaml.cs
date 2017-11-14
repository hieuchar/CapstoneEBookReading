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

namespace EBookReading
{
    /// <summary>
    /// Interaction logic for PDFBrowser.xaml
    /// </summary>
    public partial class PDFBrowser : Window
    {
        public PDFBrowser()
        {
            InitializeComponent();
        }
        public void LoadPDF(string filepath)
        {
            
            PDFContent.Navigate(new Uri(filepath));
            
            //PDFContent.Source = new System.Uri(filepath);
            
        }
    }
}
