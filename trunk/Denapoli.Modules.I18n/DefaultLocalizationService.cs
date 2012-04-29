using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.I18n
{
    [Export(typeof(ILocalizationService))]
    public class DefaultLocalizationService : NotifyPropertyChanged, ILocalizationService
    {
        private Dictionary<string, string> _currentdict = new Dictionary<string, string>();
        private Dictionary<string, Dictionary<string, string>> _loadedDicts = new Dictionary<string, Dictionary<string, string>>(); 
        private const string HostName = "http://127.0.0.1:8080/i18n/";

        public DefaultLocalizationService()
        {
            AvailableLangages = new List<Langage>
                                    {
                                        new Langage{Code="FR",Name = "Français"},
                                        new Langage{Code="EN",Name = "English"},
                                        new Langage{Code="SP",Name = "Español"}
                                    };
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


        private static Dictionary<string, string> DownloadDico(string language)
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
