using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.I18n
{
    [Export(typeof(ILocalizationService))]
    public class DefaultLocalizationService : NotifyPropertyChanged, ILocalizationService
    {
        public IDataProvider DataProvider { get; set; }
        public ISettingsService SettingsService { get; set; }
        private Dictionary<string, string> _currentdict = new Dictionary<string, string>();
        private readonly Dictionary<string, Dictionary<string, string>> _loadedDicts = new Dictionary<string, Dictionary<string, string>>();
        private string HostName { set; get; }

        [ImportingConstructor]
        public DefaultLocalizationService(IDataProvider dataProvider, ISettingsService settingsService)
        {
            DataProvider = dataProvider;
            SettingsService = settingsService;
            HostName = SettingsService.GetDataRepositoryRootPath() + "i18n/";
            var list = new List<Langage>();
            DataProvider.GetAvailableLanguages().ForEach(item=>list.Add(new Langage{Code = item.Code, Name = item.NoM}));
            AvailableLangages = list;
            Dico = new Dico();
            CurrentLangage = AvailableLangages.First();
        }

        public IEnumerable<Langage> AvailableLangages { get; private set; }

        private Langage _currentLangage;
       

        public Langage CurrentLangage
        {
            get { return _currentLangage; }
            set
            {
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
            var r = _currentdict.ContainsKey(key) ? _currentdict[key] : _currentLangage.Code;
            if(string.IsNullOrEmpty(r))
                Console.WriteLine("--------------------to traduce----------- :"+_currentLangage.Code+" : "+key);
            return r;
        }

        public string Localize(string key, Langage langage)
        {
            Load(langage);
            var dict = _loadedDicts[langage.Name];
            if (string.IsNullOrEmpty(key)) return "";
            var r = dict.ContainsKey(key) ? dict[key] : langage.Code;
            if (string.IsNullOrEmpty(r))
                Console.WriteLine("--------------------to traduce----------- :" + langage.Code + " : " + key);
            return r;
        }

        private void Load(Langage langage)
        {
            if(!_loadedDicts.ContainsKey(langage.Name))
            {
                var dict = DownloadDico(langage.DictURL);
                _loadedDicts[langage.Name] = dict;
            }
        }


        private  Dictionary<string, string> DownloadDico(string language)
        {
            if (string.IsNullOrEmpty(language)) return new Dictionary<string, string>();

            var request = WebRequest.Create(new Uri(HostName +language, UriKind.Absolute));
            request.Timeout = -1;
            WebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
            var dico = new Dictionary<string, string>();

            var responseStream = new StreamReader(response.GetResponseStream(),Encoding.Default);
            while (!responseStream.EndOfStream)
            {
                var line = responseStream.ReadLine().Split(new[] { '=' });
                dico[line[0]] = line[1];
            }
            return dico;
        }
    }
}
