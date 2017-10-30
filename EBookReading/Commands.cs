using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EBookReading
{
    public static class Commands
    {
        public static readonly RoutedUICommand NextPage = new RoutedUICommand
          (
              "Next Page",
              "NextPage",
              typeof(Commands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.Right)
              }
          );
        public static readonly RoutedUICommand PrevPage = new RoutedUICommand
            (
                "Previous Page",
                "PrevPage",
                typeof(Commands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.Left)
                }
            );
        public static readonly RoutedUICommand AddFolder = new RoutedUICommand
            (
                "Add a folder",
                "AddFolder",
                typeof(Commands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F, ModifierKeys.Control)
                }
            );
        public static readonly RoutedUICommand AddBook = new RoutedUICommand
            (
                "Add a book",
                "AddBook",
                typeof(Commands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.O, ModifierKeys.Control)
                }
            );
       
    }
}
