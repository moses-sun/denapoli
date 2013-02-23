using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class LanguagesAdminViewModel : NotifyPropertyChanged, IEditableObject, IUpdatebale
    {
        public IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public IWebService WEBService { get; set; }
        public ObservableCollection<LangageVm> Langues { get; set; }
        public static List<string> Keys { get; set; }

        [ImportingConstructor]
        public LanguagesAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService, IWebService webService)
        {
            LangageVm.Parent = this;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            WEBService = webService;
            LangageVm.DataProvider = DataProvider;
            LangageVm.SettingsService = SettingsService;
            Langues = new ObservableCollection<LangageVm>();
            Langues.CollectionChanged += OnLangueschanged;
            Keys = new List<string>();
            UpdateLangues();
        }

        private void OnLangueschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove :
                    var deletedLangue = (LangageVm)e.OldItems[0];
                    DataProvider.Delete(deletedLangue.Langue);
                    DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(this);
                    var newSelectedIndex = e.OldStartingIndex-1;
                    newSelectedIndex = newSelectedIndex < 0 ? 0 : newSelectedIndex;
                    SelectedLangue = Langues.Count > 0 ? Langues[newSelectedIndex] : null;
                    break;
            }
        }

        private void MergeKeys()
        {
            foreach (var l in LocalizationService.AvailableLangages)
            {
                LocalizationService.Keys.ForEach(key =>
                {
                    if(!Keys.Contains(key))
                        Keys.Add(key);
                });
            }
        }

        private void UpdateLangues()
        {
            var old = SelectedLangue == null ? -1 : SelectedLangue.Langue.IDLang;
            Langues.CollectionChanged -= OnLangueschanged;
            Langues.Clear();
            MergeKeys();
            foreach (var l in LocalizationService.AvailableLangages)
            {
                var langageVm = new LangageVm(l, DataProvider, SettingsService, WEBService);
                Keys.ForEach(key => langageVm.Dico.Add(new DicoEntry { Key = key, Value = LocalizationService.Localize(key, langageVm.Langage) }));
                Langues.Add(langageVm);
            }
            SelectedLangue = Langues.FirstOrDefault(item => item.Langue.IDLang == old);
            SelectedLangue = SelectedLangue ?? Langues.FirstOrDefault();
            Langues.CollectionChanged += OnLangueschanged;
        }

        private LangageVm _selectedLangue;
        public LangageVm SelectedLangue
        {
            get { return _selectedLangue; }
            set
            {
                _selectedLangue = value;
                NotifyChanged("SelectedLangue");
            }
        }

        public void BeginEdit()
        {
            Langues.ForEach(item => item.BeginEdit());
        }

        public void EndEdit()
        {
            Langues.ForEach(item => item.EndEdit());
            DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(null);

        }

        public void CancelEdit()
        {
            Langues.ForEach(item => item.CancelEdit());
        }

        public void Update()
        {
           UpdateLangues();
        }
    }
}