using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class LanguagesAdminViewModel : NotifyPropertyChanged, IEditableObject
    {
        public IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public ObservableCollection<LangageVm> Langues { get; set; }
        public ObservableCollection<DicoEntry> SelectedDico { get; set; }

        [ImportingConstructor]
        public LanguagesAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            Langues = new ObservableCollection<LangageVm>();
            Langues.CollectionChanged += OnLangueschanged;
            SelectedDico = new ObservableCollection<DicoEntry>();
            UpdateLangues();
        }

        private void OnLangueschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {

                case NotifyCollectionChangedAction.Remove:
                    var deletedLangue = (LangageVm)e.OldItems[0];
                    DataProvider.Delete(deletedLangue.Langue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void UpdateLangues()
        {
            Langues.CollectionChanged -= OnLangueschanged;
            Langues.Clear();
            foreach (var l in LocalizationService.AvailableLangages)
            {
                var langageVm = new LangageVm(l, DataProvider, SettingsService);
                LocalizationService.Keys.ForEach(key => langageVm.Dico.Add(new DicoEntry { Key = key, Value = LocalizationService.Localize(key, langageVm.Langage) }));
                Langues.Add(langageVm);
            }
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
                SelectedDico = value.Dico;
            }
        }

        public void BeginEdit()
        {
            Langues.ForEach(item => item.BeginEdit());
        }

        public void EndEdit()
        {
            Langues.ForEach(item => item.EndEdit());
        }

        public void CancelEdit()
        {
            Langues.ForEach(item => item.CancelEdit());
        }
    }
}