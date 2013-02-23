using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.I18n
{
    [Export(typeof (ILocalizationService))]
    public class DefaultLocalizationService : NotifyPropertyChanged, ILocalizationService
    {
        public IDataProvider DataProvider { get; set; }
        public ISettingsService SettingsService { get; set; }
        public IWebService WEBService { get; set; }
        private Dictionary<string, string> _currentdict = new Dictionary<string, string>();

        private readonly Dictionary<string, Dictionary<string, string>> _loadedDicts =
            new Dictionary<string, Dictionary<string, string>>();

        [ImportingConstructor]
        public DefaultLocalizationService(IDataProvider dataProvider, ISettingsService settingsService,
                                          IWebService webService)
        {
            DataProvider = dataProvider;
            SettingsService = settingsService;
            WEBService = webService;

            AvailableLangages = new ObservableCollection<Langage>();
            DataProvider.GetAvailableLanguages().ForEach(
                item => AvailableLangages.Add(new Langage {Code = item.Code, Name = item.NoM}));
            Dico = new Dico();
            CurrentLangage = AvailableLangages.FirstOrDefault();
        }

        public void Reset()
        {
            //SendDocs();
            _loadedDicts.Clear();
            AvailableLangages.Clear();
            DataProvider.GetAvailableLanguages().ForEach(
                item => AvailableLangages.Add(new Langage {Code = item.Code, Name = item.NoM}));
            Dico.Notify();
        }

        public ObservableCollection<Langage> AvailableLangages { get; private set; }

        private Langage _currentLangage;


        public void ModifyLocaLization(string key, string traduction, Langage langage)
        {
            Load(langage);
            var dict = _loadedDicts[langage.Name];
            if (string.IsNullOrEmpty(key)) return;
            dict[key] = traduction;
        }

        public void SendDocs()
        {
            foreach (var langage in AvailableLangages)
            {
                if (_loadedDicts.ContainsKey(langage.Name))
                {
                    var fileName = langage.Code + ".txt";
                    DumpFile(_loadedDicts[langage.Name], fileName);
                    WEBService.UploadFile(SettingsService.GetDataRepositoryRootPath() + "i18n/upload.php", fileName);
                    File.Delete(fileName);
                }
                else
                {
                    Load(langage);
                    foreach (string key in Keys)
                    {
                        _loadedDicts[langage.Name][key] = Localize(key, langage);
                    }
                    var fileName = langage.Code + ".txt";
                    DumpFile(_loadedDicts[langage.Name], fileName);
                    WEBService.UploadFile(SettingsService.GetDataRepositoryRootPath() + "i18n/upload.php", fileName);
                    File.Delete(fileName);
                }
            }
        }

        private void DumpFile(Dictionary<string, string> dico, string fileName)
        {
            var lines = new string[dico.Count];
            var i = 0;
            foreach (var entry in dico)
                lines[i++] = entry.Key + "=" + entry.Value;
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        public Langage CurrentLangage
        {
            get { return _currentLangage; }
            set
            {
                if (value == null) return;
                _currentLangage = value;
                Load(_currentLangage);
                _currentdict = _loadedDicts[_currentLangage.Name];
                Dico.Notify();
                NotifyChanged("CurrentLangage");
            }
        }

        public Dico Dico { get; private set; }

        public List<string> Keys
        {
            get { return new List<string>(_currentdict.Keys); }
        }

        public string Localize(string key)
        {
            if (string.IsNullOrEmpty(key)) return "";
            var r = _currentdict.ContainsKey(key) ? _currentdict[key] : ""; // _currentLangage.Code;
            if (string.IsNullOrEmpty(r))
            {
                _currentdict[key] = "";
                // Console.WriteLine("--------------------to traduce----------- :" + _currentLangage.Code + " : " + key);
            }
            return string.IsNullOrEmpty(r) ? key : r;
        }

        public string Localize(string key, Langage langage)
        {
            Load(langage);
            var dict = _loadedDicts[langage.Name];
            if (string.IsNullOrEmpty(key)) return "";
            var r = dict.ContainsKey(key) ? dict[key] : ""; // langage.Code;
            if (string.IsNullOrEmpty(r))
            {
                dict[key] = "";
                //Console.WriteLine("--------------------to traduce----------- :" + langage.Code + " : " + key);
            }
            return string.IsNullOrEmpty(r) ? key : r;
        }

        private void Load(Langage langage)
        {
            if (!_loadedDicts.ContainsKey(langage.Name))
            {
                var dict = DownloadDico(langage.DictURL);
                _loadedDicts[langage.Name] = dict;
            }
        }


        private Dictionary<string, string> DownloadDico(string language)
        {
            if (string.IsNullOrEmpty(language)) return new Dictionary<string, string>();
            var dico = new Dictionary<string, string>();
            var stream = WEBService.DownloadFile(SettingsService.GetDataRepositoryRootPath() + "i18n/" + language);
            if (stream != null)
            {
                var responseStream = new StreamReader(stream, Encoding.UTF8);
                while (!responseStream.EndOfStream)
                {
                    var line = responseStream.ReadLine().Split(new[] {'='});
                    if (line.Length > 1)
                        dico[line[0]] = line[1];
                    else if (line.Length > 0)
                        dico[line[0]] = "";
                }
            }
            return dico;
        }
    }

}
