using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class LanguagesAdminViewModel : NotifyPropertyChanged
    {
        public IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ObservableCollection<LangageVm> Langues { get; set; }
        public ObservableCollection<DicoEntry> SelectedDico { get; set; }

        [ImportingConstructor]
        public LanguagesAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            Langues = new ObservableCollection<LangageVm>();
            SelectedDico = new ObservableCollection<DicoEntry>();
            UpdateLangues();
        }

        private void OnLangueschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MessageBox.Show("add Langage");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    MessageBox.Show("Remove Langage");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void UpdateLangues()
        {
            Langues.CollectionChanged -= OnLangueschanged;
            Langues.Clear();
            foreach (var l in LocalizationService.AvailableLangages) Langues.Add(new LangageVm(l));
            SelectedLangue = Langues.FirstOrDefault();
            Langues.CollectionChanged += OnLangueschanged;
        }

        private LangageVm _selectedLangue;
        public LangageVm SelectedLangue
        {
            get { return _selectedLangue; }
            set
            {
                _selectedLangue = value;
                UpdateDico();
            }
        }

        private void UpdateDico()
        {
            SelectedDico.Clear();
            LocalizationService.Keys.ForEach(key => SelectedDico.Add(new DicoEntry{Key = key, Value = LocalizationService.Localize(key,SelectedLangue.Langage)}));
        }
    }

    public class DicoEntry : NotifyPropertyChanged,  IEditableObject
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                NotifyChanged("Key");
            }
        }

        private string _oldValue;
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyChanged("Value");
            }
        }

        public void BeginEdit()
        {
            _oldValue = Value;
        }

        public void EndEdit()
        {
           //TODO
        }

        public void CancelEdit()
        {
            Value = _oldValue;
        }
    }
}