using EBookReading.CBR;
using EBookReading.CBZ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EBookReading
{
    /// <summary>
    /// Interaction logic for ComicBrowser.xaml
    /// </summary>
    public partial class ComicBrowser : Window
    {
        private string _filePath;
        ComicBookAbstract CBook;
        int CurrentPage = 0;
        public ComicBrowser()
        {
            InitializeComponent();
            Closing += OnWindowClosing;
        }
        /// <summary>
        /// Loads a Comic Book Reader according to the extension of the filepath
        /// </summary>
        /// <param name="FilePath"></param>
        public void DisplayBook(string FilePath)
        {
            _filePath = FilePath;
            CurrentPage = AppData.GetChapter(FilePath);
            switch (System.IO.Path.GetExtension(FilePath))
            {
                case ".cbr":
                    CBook = new CBRReader();
                    CBook.CreateBook(FilePath);                    
                    ImageContent.Source = ConvertImageToImageSource(CBook.GetPage(CurrentPage));
                    break;
                case ".cbz":
                    CBook = new CBZReader();
                    CBook.CreateBook(FilePath);
                    ImageContent.Source = ConvertImageToImageSource(CBook.GetPage(CurrentPage));
                    break;
            }
            LeftArrow.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "//Icons///leftarrowicon.png"));
            RightArrow.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "//Icons///rightarrowicon.png"));
            CurrentPageBox.Text = "" + (CurrentPage + 1);
            TotalPages.Text = "/" + CBook.GetMaxPage();
        }
        public ImageSource ConvertImageToImageSource(Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();
            ms.Dispose();
            return bitmapImage;
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            AppData.AddBookProgress(_filePath, CurrentPage);
            CBook = null;
        }
        private void NextPageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NextPageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentPage < CBook.GetMaxPage() - 1)
            {
                ImageContent.Source = ConvertImageToImageSource(CBook.GetPage(++CurrentPage));
                CurrentPageBox.Text = "" + (CurrentPage+1);
            }
        }
        private void NextPageButton_Pressed(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < CBook.GetMaxPage() - 1)
            {
                ImageContent.Source = ConvertImageToImageSource(CBook.GetPage(++CurrentPage));
                CurrentPageBox.Text = "" + (CurrentPage +1);
            }
        }
        
        private void PrevPageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void PrevPageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentPage > 0)
            {
                ImageContent.Source = ConvertImageToImageSource(CBook.GetPage(--CurrentPage));
                CurrentPageBox.Text = "" + (CurrentPage+1);
            }
        }
        private void PrevPageButton_Pressed(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 0)
            {
                ImageContent.Source = ConvertImageToImageSource(CBook.GetPage(--CurrentPage));
                CurrentPageBox.Text = "" + (CurrentPage+1);
            }

        }

        private void CurrentPage_KeyDown(object sender, KeyEventArgs e)
        {           
            if(e.Key == Key.Return)
            {
                int page = int.Parse(CurrentPageBox.Text) - 1;
                if(page >= 0 && page < CBook.GetMaxPage() - 1)
                CurrentPage = page;
                ImageContent.Source = ConvertImageToImageSource(CBook.GetPage(page));
                this.Focus();
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
