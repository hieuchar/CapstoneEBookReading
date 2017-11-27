﻿#pragma checksum "..\..\EpubBrowser.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B77CB14493904F35411440C6D538F049"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EBookReading.Epub;
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


namespace EBookReading.Epub {
    
    
    /// <summary>
    /// EpubBrowser
    /// </summary>
    public partial class EpubBrowser : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ToC;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid BookGrid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser SectionContent;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Controls;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextChapterButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image NextChapterImage;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrevChapterButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\EpubBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PrevChapterImage;
        
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
            System.Uri resourceLocater = new System.Uri("/EBookReading;component/epubbrowser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EpubBrowser.xaml"
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
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.ToC = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.BookGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.SectionContent = ((System.Windows.Controls.WebBrowser)(target));
            return;
            case 5:
            this.Controls = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.NextChapterButton = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\EpubBrowser.xaml"
            this.NextChapterButton.Click += new System.Windows.RoutedEventHandler(this.Next_Chapter);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NextChapterImage = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.PrevChapterButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\EpubBrowser.xaml"
            this.PrevChapterButton.Click += new System.Windows.RoutedEventHandler(this.Prev_Chapter);
            
            #line default
            #line hidden
            return;
            case 9:
            this.PrevChapterImage = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            
            #line 38 "..\..\EpubBrowser.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Zoom_In);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 39 "..\..\EpubBrowser.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Zoom_Out);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

