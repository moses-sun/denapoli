using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class BornesAdminViewModel: NotifyPropertyChanged
    {
        private IDataProvider DataProvider { get; set; }
        public ObservableCollection<BorneVm> Bornes { get; set; }

        [ImportingConstructor]
        public BornesAdminViewModel(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
            Bornes = new ObservableCollection<BorneVm>();
            UpdateBornes();
            Bornes.CollectionChanged += OnBorneschanged;
        }

        private void OnBorneschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MessageBox.Show("add borne");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    MessageBox.Show("Remove borne");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateBornes()
        {
            Bornes.CollectionChanged -= OnBorneschanged;
            Bornes.Clear();
            var bornes = DataProvider.GetAllBornes();
            bornes.ForEach(item => Bornes.Add(new BorneVm(item, DataProvider)));
            SelectedBorne = Bornes.FirstOrDefault();
            Bornes.CollectionChanged += OnBorneschanged;
        }

        private BorneVm _selectedBorne;
        public BorneVm SelectedBorne
        {
            get { return _selectedBorne; }
            set
            {
                _selectedBorne = value;
                NotifyChanged("SelectedBorne");
            }
        }
    }
}