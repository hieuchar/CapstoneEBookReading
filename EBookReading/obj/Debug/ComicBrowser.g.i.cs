﻿#pragma checksum "..\..\ComicBrowser.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F1EC803F5B13B825A400ECC7304E88DF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EBookReading;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace EBookReading {
    
    
    /// <summary>
    /// ComicBrowser
    /// </summary>
    public partial class ComicBrowser : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrevPage;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image LeftArrow;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel PageNumber;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CurrentPageBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TotalPages;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextPage;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image RightArrow;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\ComicBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageContent;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EBookReading;component/comicbrowser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ComicBrowser.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\ComicBrowser.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.NextPageCommand_CanExecute);
            
            #line default
            #line hidden
            
            #line 10 "..\..\ComicBrowser.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.NextPageCommand_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 11 "..\..\ComicBrowser.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.PrevPageCommand_CanExecute);
            
            #line default
            #line hidden
            
            #line 11 "..\..\ComicBrowser.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.PrevPageCommand_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PrevPage = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\ComicBrowser.xaml"
            this.PrevPage.Click += new System.Windows.RoutedEventHandler(this.PrevPageButton_Pressed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LeftArrow = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.PageNumber = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.CurrentPageBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\ComicBrowser.xaml"
            this.CurrentPageBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            
            #line 26 "..\..\ComicBrowser.xaml"
            this.CurrentPageBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.CurrentPage_KeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TotalPages = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.NextPage = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\ComicBrowser.xaml"
            this.NextPage.Click += new System.Windows.RoutedEventHandler(this.NextPageButton_Pressed);
            
            #line default
            #line hidden
            return;
            case 9:
            this.RightArrow = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.ImageContent = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

