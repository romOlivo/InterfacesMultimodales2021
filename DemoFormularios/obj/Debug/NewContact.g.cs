﻿#pragma checksum "..\..\NewContact.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B35EB5095CC1D832EA816DACDA41B3A456B4EF070E5FCEE5ECD15B0740AB6EDC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using DemoFormularios;
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


namespace DemoFormularios {
    
    
    /// <summary>
    /// NewContact
    /// </summary>
    public partial class NewContact : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\NewContact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform ApplicationScaleTransform;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\NewContact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CajaNombre;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\NewContact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CajaApellidos;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\NewContact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckFriend;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\NewContact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdMan;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\NewContact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdWoman;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\NewContact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addB;
        
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
            System.Uri resourceLocater = new System.Uri("/DemoFormularios;component/newcontact.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewContact.xaml"
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
            this.ApplicationScaleTransform = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 2:
            this.CajaNombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.CajaApellidos = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.CheckFriend = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 5:
            this.rdMan = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.rdWoman = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.addB = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\NewContact.xaml"
            this.addB.Click += new System.Windows.RoutedEventHandler(this.addB_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

