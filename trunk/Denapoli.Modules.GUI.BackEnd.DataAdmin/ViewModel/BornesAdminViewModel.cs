using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class BornesAdminViewModel : NotifyPropertyChanged, IEditableObject
    {
        private IDataProvider DataProvider { get; set; }
        public ObservableCollection<BorneVm> Bornes { get; set; }

        private string _oldMessage;
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyChanged("Message");
            }
        }

        private bool _oldIOuvert;
        private bool _isOuvert;
        public bool IsOuvert
        {
            get { return _isOuvert; }
            set
            {
                _isOuvert = value;
                NotifyChanged("IsOuvert");
            }
        }

        private DateTime _oldHeureOuvertureJour;
        private DateTime _heureOuvertureJour;
        public DateTime HeureOuvertureJour
        {
            get { return _heureOuvertureJour; }
            set
            {
                _heureOuvertureJour = value;
                NotifyChanged("HeureOuvertureJour");
            }
        }

        private DateTime _oldHeureOuvertureSoir;
        private DateTime _heureOuvertureSoir;
        public DateTime HeureOuvertureSoir
        {
            get { return _heureOuvertureSoir; }
            set
            {
                _heureOuvertureSoir = value;
                NotifyChanged("HeureOuvertureSoir");
            }
        }

        private DateTime _oldHeureFermetureJour;
        private DateTime _heureFermetureJour;
        public DateTime HeureFermetureJour
        {
            get { return _heureFermetureJour; }
            set
            {
                _heureFermetureJour = value;
                NotifyChanged("HeureFermetureJour");
            }
        }

        private DateTime _oldHeureFermetureSoir;
        private DateTime _heureFermetureSoir;
        public DateTime HeureFermetureSoir
        {
            get { return _heureFermetureSoir; }
            set
            {
                _heureFermetureSoir = value;
                NotifyChanged("HeureFermetureSoir");
            }
        }


        [ImportingConstructor]
        public BornesAdminViewModel(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
            BorneVm.DataProvider = DataProvider;
            Bornes = new ObservableCollection<BorneVm>();
            UpdateBornes();
            Bornes.CollectionChanged += OnBorneschanged;
        }

        private void OnBorneschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    var deletedBorne = (BorneVm)e.OldItems[0];
                    DataProvider.Delete(deletedBorne.Borne);
                    break;
            }
        }

        private void UpdateBornes()
        {
            var old = SelectedBorne == null ? -1 : SelectedBorne.Borne.IDBorn;
            Bornes.CollectionChanged -= OnBorneschanged;
            Bornes.Clear();
            var bornes = DataProvider.GetAllBornes();
            bornes.ForEach(item => Bornes.Add(new BorneVm(item, DataProvider)));
            SelectedBorne = Bornes.FirstOrDefault(item => item.Borne.IDBorn == old);
            SelectedBorne = SelectedBorne ?? Bornes.FirstOrDefault();
            Bornes.CollectionChanged += OnBorneschanged;

            var borne = bornes.FirstOrDefault();
            if (borne == null) return;
            Message = borne.Message;
            HeureOuvertureJour = borne.HeureOuvertureJour;
            HeureFermetureJour = borne.HeureFermetureJour;
            HeureOuvertureSoir = borne.HeureOuvertureSoir;
            HeureFermetureSoir = borne.HeureFermetureSoir;
            IsOuvert = borne.IsOuvert;
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



        public void Update()
        {
            UpdateBornes();
        }

        public void BeginEdit()
        {
            _oldMessage = Message;
            _oldIOuvert = IsOuvert;
            _oldHeureOuvertureJour = HeureOuvertureJour;
            _oldHeureFermetureJour = HeureFermetureJour;
            _oldHeureOuvertureSoir = HeureOuvertureSoir;
            _oldHeureFermetureSoir = HeureFermetureSoir;
        }

        public void EndEdit()
        {
            var bornes = new List<Borne>();
            Bornes.ForEach(item =>
                               {
                                   item.Message = Message;
                                   item.IsOuvert = IsOuvert;
                                   item.HeureOuvertureJour = HeureOuvertureJour;
                                   item.HeureFermetureJour = HeureFermetureJour;
                                   item.HeureOuvertureSoir = HeureOuvertureSoir;
                                   item.HeureFermetureSoir = HeureFermetureSoir;

                                   item.Borne.Message = Message;
                                   item.Borne.IsOuvert = IsOuvert;
                                   item.Borne.HeureOuvertureJour = HeureOuvertureJour;
                                   item.Borne.HeureFermetureJour = HeureFermetureJour;
                                   item.Borne.HeureOuvertureSoir = HeureOuvertureSoir;
                                   item.Borne.HeureFermetureSoir = HeureFermetureSoir;

                                   bornes.Add(item.Borne);
                               });
            DataProvider.UpdateBornes(bornes);
        }

        public void CancelEdit()
        {
            Message = _oldMessage;
            IsOuvert = _oldIOuvert;
            HeureOuvertureJour = _oldHeureOuvertureJour;
            HeureFermetureJour = _oldHeureFermetureJour;
            HeureOuvertureSoir = _oldHeureOuvertureSoir;
            HeureFermetureSoir = _oldHeureFermetureSoir;
        }
    }
}