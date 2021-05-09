﻿#pragma checksum "..\..\..\Views\MainView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B181B6F2E9000AAD600F2290CB3E75D6ADFD323148A5AF56627006B7A24E15A4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IGMICloudApplication.ViewModels;
using IGMICloudApplication.Views;
using IGMICloudApplication.Views.Extensions;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
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


namespace IGMICloudApplication.Views {
    
    
    /// <summary>
    /// MainView
    /// </summary>
    public partial class MainView : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 530 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentPresenter mainLayout;
        
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
            System.Uri resourceLocater = new System.Uri("/IGMICloudApplication;component/views/mainview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MainView.xaml"
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
            case 11:
            this.mainLayout = ((System.Windows.Controls.ContentPresenter)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 86 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.TextBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.OnInputFieldFocused);
            
            #line default
            #line hidden
            
            #line 86 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.OnInputFieldFocusedLost);
            
            #line default
            #line hidden
            break;
            case 2:
            
            #line 113 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.PasswordBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.OnInputFieldFocused);
            
            #line default
            #line hidden
            
            #line 113 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.PasswordBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.OnInputFieldFocusedLost);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 118 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Login_Click);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 170 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.TextBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.OnInputFieldFocused);
            
            #line default
            #line hidden
            
            #line 170 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.OnInputFieldFocusedLost);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 277 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Open_Folder_Creation_Popup);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 392 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close_Folder_Creation_Popup);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 423 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FolderPrivacy_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 446 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_WaterMarks_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 451 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_Allow_Downloading_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 474 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close_Folder_Creation_Popup);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

