﻿using System.ComponentModel.Composition;

namespace Denapoli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [Export]
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
        }

        private ShellPresenter _presenter;

        [Import]
        public ShellPresenter ViewModel
        {
            set
            {
                DataContext = value;
                _presenter = value;
            }
        }
    }
}