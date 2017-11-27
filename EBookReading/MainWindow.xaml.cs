


using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EBookReading
{
    public partial class MainWindow : Window
    {
        private List<BookInfo> Library = new List<BookInfo>();
        public Theme theme;
        public MainWindow()
        {
            InitializeComponent();
            AppData.LoadData("EBookPaths.sav");
            LoadIcons();
            LoadTheme();
            CreateGrid();
            RefreshList();            
        }
        public void LoadTheme()
        {        
            theme = AppData.GetTheme();
            switch (theme)
            {
                case Theme.DARK:                    
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("pack://application:,,,/Selen.Wpf.SystemStyles;component/Styles.xaml", UriKind.RelativeOrAbsolute)
                    });
                    MainPanel.Background = (Brush)FindResource("BackgroundNormal");
                    break;
                case Theme.DEFAULT:
                    Application.Current.Resources.MergedDictionaries.Clear();
                    MainPanel.Background = Brushes.White;
                    break;
            }
        }
        public void LoadIcons()
        {                        
            AddBookImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Icons/AddBookicon.png"));
            AddFolderImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Icons/AddFolderIcon.png"));
            RemoveListingImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Icons/DeleteIcon.png"));
        }
        #region File Loading

        private void AddFolderCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void AddFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFolderDialog();            
        }
        private void AddFolderButton_Pressed(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog();
        }
        private void OpenFolderDialog()
        {
            //Open up a file explorer to find a folder with ebooks within it and add all of them
            //Only searches that directory, will not search farther
            var AddFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = AddFolderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AppData.AddFolderPath(AddFolderDialog.SelectedPath);
                List<string> FilePaths = FileManipulator.FindFilesFromFolder(AddFolderDialog.SelectedPath);
                AppData.AddBookPath(FilePaths);
                Library.AddRange(FileManipulator.CreateBooksFromPaths(FilePaths));
                Console.WriteLine("Added a folder");
                RefreshList();
            }
        }
        private void AddBookCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddBookCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenBookDialog();
        }
        private void AddBookButton_Pressed(object sender, RoutedEventArgs e)
        {
            OpenBookDialog();
        }
        private void OpenBookDialog()
        {
            //Open up a file explorer to add a specific ebook            
            var AddBookDialog = new System.Windows.Forms.OpenFileDialog();
            AddBookDialog.Filter = "Ebook Files | *.mobi; *.pdf; *.epub; *.cbr; *.cbz";
            AddBookDialog.InitialDirectory = @"C:\";
            System.Windows.Forms.DialogResult result = AddBookDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AppData.AddBookPath(AddBookDialog.FileName);
                RefreshList();
            }
        }
        private void Open_Book(object sender, EventArgs e)
        {
            BookInfo b = ((FrameworkElement)sender).DataContext as BookInfo;
            FileManipulator.LoadBook(b);
        }
        #endregion
        #region Deleting
        private void Delete_Book(object sender, RoutedEventArgs e)
        {
            var DeleteList = LibraryDataGrid.SelectedItems;
            foreach (BookInfo bi in DeleteList)
            {
                AppData.DeleteBook(bi.FilePath);
            }
            RefreshList();
        }
        #endregion
        #region List Display
        //Gets list of books from App Data and refreshes the DataGrid on the main window
        public void RefreshList()
        {
            var BookPath = AppData.GetBookPaths();
            List<string> MissingFiles = new List<string>();
            Library.Clear();
            while (BookPath.MoveNext())
            {
                if (System.IO.File.Exists(BookPath.Current.ToString()))
                {
                    if (!Library.Any(x => x.FilePath == BookPath.Current.ToString()))
                    {
                        Library.Add(FileManipulator.CreateBookFromPath(BookPath.Current.ToString()));
                    }
                }
                else MissingFiles.Add(BookPath.Current.ToString());
            }
            foreach(string s in MissingFiles)
            {
                AppData.DeleteBook(s);
            }            
            LibraryDataGrid.Items.Clear();            
            foreach (BookInfo b in Library)
            {
                LibraryDataGrid.Items.Add(b);
            }
        }
        public void RefreshSearch(List<BookInfo> SearchResults)
        {
            var BookPath = AppData.GetBookPaths();
            LibraryDataGrid.Items.Clear();
            foreach (BookInfo b in SearchResults)
            {
                LibraryDataGrid.Items.Add(b);
            }
        }
        private void SearchKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LibraryDataGrid.Items.Clear();
                TextBox tb = (TextBox)sender;
                List<BookInfo> result = new List<BookInfo>();
                if (tb.Text.Length > 0)
                {
                    Console.WriteLine(tb.Text);
                    foreach (BookInfo b in Library)
                    {
                        if (b.Contains(tb.Text))
                        {
                            result.Add(b);
                        }
                    }
                }
                else
                {
                    result = Library;
                }
                RefreshSearch(result);
            }
        }
        //Sets up the data grid
        public void CreateGrid()
        {
            DataGridTextColumn TitleColumn = new DataGridTextColumn();
            TitleColumn.Header = "Title";
            TitleColumn.Binding = new Binding("Title");
            LibraryDataGrid.Columns.Add(TitleColumn);

            DataGridTextColumn AuthorColumn = new DataGridTextColumn();
            AuthorColumn.Header = "Author";
            AuthorColumn.Binding = new Binding("Author");
            LibraryDataGrid.Columns.Add(AuthorColumn);           

            DataGridTextColumn PublishDateColumn = new DataGridTextColumn();
            PublishDateColumn.Header = "Date";
            PublishDateColumn.Binding = new Binding("Date");
            LibraryDataGrid.Columns.Add(PublishDateColumn);

            DataGridTextColumn PublisherColumn = new DataGridTextColumn();
            PublisherColumn.Header = "Publisher";
            PublisherColumn.Binding = new Binding("Publisher");
            LibraryDataGrid.Columns.Add(PublisherColumn);

            DataGridTextColumn FileTypeColumn = new DataGridTextColumn();
            FileTypeColumn.Header = "Extension";
            FileTypeColumn.Binding = new Binding("Extension");
            LibraryDataGrid.Columns.Add(FileTypeColumn);
        }
        //Opens a new window with the content of the book
        #endregion

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBar.Text = "";
        }

        private void SwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            switch (theme)
            {
                case Theme.DARK:
                    AppData.ChangeTheme(Theme.DEFAULT);
                    LoadTheme();
                    break;
                case Theme.DEFAULT:
                    AppData.ChangeTheme(Theme.DARK);
                    LoadTheme();
                    break;
            }
        }
    }
}
