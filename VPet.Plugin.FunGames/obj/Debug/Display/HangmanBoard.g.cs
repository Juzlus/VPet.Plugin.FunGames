﻿#pragma checksum "..\..\..\Display\HangmanBoard.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1C3F1CE07BDEA67CDDCA36DB316A72FFF9F6DE39D95466D76395CBBA22B9EB85"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using LinePutScript.Localization.WPF;
using Panuon.WPF.UI;
using Panuon.WPF.UI.Configurations;
using Panuon.WPF.UI.Resources;
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
using VPet.Plugin.FunGames;


namespace VPet.Plugin.FunGames {
    
    
    /// <summary>
    /// HangmanBoard
    /// </summary>
    public partial class HangmanBoard : Panuon.WPF.UI.WindowX, System.Windows.Markup.IComponentConnector {
        
        
        #line 87 "..\..\..\Display\HangmanBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Hook_phase;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Display\HangmanBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTalk;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Display\HangmanBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SendButton;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Display\HangmanBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RestartButton;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Display\HangmanBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitButton;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Display\HangmanBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ChosenWordLabel;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\Display\HangmanBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel inCorrectWordLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/VPet.Plugin.FunGames;component/display/hangmanboard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Display\HangmanBoard.xaml"
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
            
            #line 10 "..\..\..\Display\HangmanBoard.xaml"
            ((VPet.Plugin.FunGames.HangmanBoard)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.WindowMouseDown);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\Display\HangmanBoard.xaml"
            ((VPet.Plugin.FunGames.HangmanBoard)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.WindowMouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Hook_phase = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.tbTalk = ((System.Windows.Controls.TextBox)(target));
            
            #line 92 "..\..\..\Display\HangmanBoard.xaml"
            this.tbTalk.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.tbTalk_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SendButton = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\Display\HangmanBoard.xaml"
            this.SendButton.Click += new System.Windows.RoutedEventHandler(this.SendMessage_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RestartButton = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\Display\HangmanBoard.xaml"
            this.RestartButton.Click += new System.Windows.RoutedEventHandler(this.Restart_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ExitButton = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\Display\HangmanBoard.xaml"
            this.ExitButton.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ChosenWordLabel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.inCorrectWordLabel = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

