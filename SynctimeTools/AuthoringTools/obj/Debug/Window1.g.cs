﻿#pragma checksum "..\..\Window1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "76B39F80F4BEB038400C7E457A3FE982"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3603
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace AuthoringTools {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\Window1.xaml"
        internal System.Windows.Controls.TextBlock position;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button refButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Window1.xaml"
        internal System.Windows.Controls.TextBlock fileName;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Window1.xaml"
        internal System.Windows.Controls.TextBox syncPointList;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button startButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button syncPointButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button stopButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Window1.xaml"
        internal System.Windows.Controls.TextBox rename;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Window1.xaml"
        internal System.Windows.Controls.ListBox namelist;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button accept;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button cance;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\Window1.xaml"
        internal System.Windows.Controls.TextBox textBox2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AuthoringTools;component/window1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 5 "..\..\Window1.xaml"
            ((AuthoringTools.Window1)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.position = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.refButton = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Window1.xaml"
            this.refButton.Click += new System.Windows.RoutedEventHandler(this.refButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.fileName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.syncPointList = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.startButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\Window1.xaml"
            this.startButton.Click += new System.Windows.RoutedEventHandler(this.startButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.syncPointButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\Window1.xaml"
            this.syncPointButton.Click += new System.Windows.RoutedEventHandler(this.syncPointButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.stopButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\Window1.xaml"
            this.stopButton.Click += new System.Windows.RoutedEventHandler(this.stopButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.rename = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\Window1.xaml"
            this.rename.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.rename_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.namelist = ((System.Windows.Controls.ListBox)(target));
            
            #line 33 "..\..\Window1.xaml"
            this.namelist.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.namelist_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.accept = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\Window1.xaml"
            this.accept.Click += new System.Windows.RoutedEventHandler(this.accept_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.cance = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\Window1.xaml"
            this.cance.Click += new System.Windows.RoutedEventHandler(this.cance_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.textBox2 = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
