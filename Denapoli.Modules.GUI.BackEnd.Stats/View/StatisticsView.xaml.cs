﻿using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using Denapoli.Modules.GUI.BackEnd.Stats.ViewModel;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.Stats.View
{
    /// <summary>
    /// Interaction logic for StatsView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class StatisticsView
    {
        public StatisticsView()
        {
            InitializeComponent();
        }

        [Import]
        public StatsViewModel ViewModel
        {
            set
            {
                DataContext = value;
                value.View = this;
                value.Window = this;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }
    }
}
