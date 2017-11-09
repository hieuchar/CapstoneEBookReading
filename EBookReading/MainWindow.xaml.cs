


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
namespace EBookReading
{
    public partial class MainWindow : Window
    {
        private List<BookInfo> Library = new List<BookInfo>();
        public MainWindow()
        {
            InitializeComponent();
            AppData.LoadData("EBookPaths.sav");
            CreateGrid();
            RefreshList();
            //LoadMobiTest();
        }
        public void LoadMobiTest()
        {
            
        }
        #region File Loading

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
        private void Delete_Book(object sender, EventArgs e)
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
            DataGridTextColumn FileTypeColumn = new DataGridTextColumn();
            FileTypeColumn.Header = "Extension";
            FileTypeColumn.Binding = new Binding("Extension");
            LibraryDataGrid.Columns.Add(FileTypeColumn);
        }
        //Opens a new window with the content of the book
        #endregion
    }
}
