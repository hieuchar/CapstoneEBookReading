
using EBookReading.Epub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
namespace EBookReading
{
   
    public partial class MainWindow : Window
    {
        private FileManipulator FileManip;
        private List<BookInfo> Library = new List<BookInfo>();
        public MainWindow()
        {
            InitializeComponent();
            FileManip = new FileManipulator();
            AppData.LoadData("EBookPaths.sav");
            CreateGrid();
            RefreshList();
            
            //EpubReader.Read("C:\\Users\\Hieu\\Desktop\\Tawny Man Trilogy by Robin Hobb\\Tawny Man 1 - Fool's Errand.epub");
        }
        #region Menu/File Loading

        private void AddFolderCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void AddFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Open up a file explorer to find a folder with ebooks within it and add all of them
            //Only searches that directory, will not search farther
            var AddFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = AddFolderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AppData.AddFolderPath(AddFolderDialog.SelectedPath);
                List<string> FilePaths = FileManip.FindFilesFromFolder(AddFolderDialog.SelectedPath);
                AppData.AddBookPath(FilePaths);
                Library.AddRange(FileManip.CreateBooksFromPaths(FilePaths));
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
            //Open up a file explorer to add a specific ebook
            var AddBookDialog = new System.Windows.Forms.OpenFileDialog();
            AddBookDialog.Filter = "Ebook Files | *.mobi; *.pdf; *.epub";
            AddBookDialog.InitialDirectory = @"C:\";
            System.Windows.Forms.DialogResult result = AddBookDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AppData.AddBookPath(AddBookDialog.FileName);                
                RefreshList();
            }
        }
        #endregion
        #region List Display
        //Gets list of books from App Data and refreshes the DataGrid on the main window
        public void RefreshList()
        {
            var BookPath = AppData.GetBookPaths();
            while (BookPath.MoveNext())
            {
                if (!Library.Any(x => x.FilePath == BookPath.Current.ToString()))
                {
                    Library.Add(FileManip.CreateBookFromPath(BookPath.Current.ToString()));
                }
            }
            LibraryDataGrid.Items.Clear();
            foreach (BookInfo b in Library)
            {
                LibraryDataGrid.Items.Add(b);
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
            DataGridTextColumn FileTypeColumn = new DataGridTextColumn();
            FileTypeColumn.Header = "Extension";
            FileTypeColumn.Binding = new Binding("Extension");
            LibraryDataGrid.Columns.Add(FileTypeColumn);
        }
        //Opens a new window with the content of the book
        private void Open_Book(object sender, EventArgs e)
        {
            BookInfo b = ((FrameworkElement)sender).DataContext as BookInfo;
            FileManip.LoadBook(b);
        }
        #endregion 
    }
}
