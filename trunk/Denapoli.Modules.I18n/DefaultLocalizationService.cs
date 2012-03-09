using System.Collections.Generic;
using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.I18n
{
    [Export(typeof(ILocalizationService))]
    public class DefaultLocalizationService : NotifyPropertyChanged, ILocalizationService
    {
        private Dictionary<string, string> _currentdict = new Dictionary<string, string>();

        public DefaultLocalizationService()
        {
            AvailableLangages = new List<Langage>
                                    {
                                        new Langage{Code="FR",Name = "Français"},
                                        new Langage{Code="EN",Name = "English"},
                                        new Langage{Code="ES",Name = "Español"}
                                    };
        }

        

        public IEnumerable<Langage> AvailableLangages { get; private set; }

        private Langage _currentLangage;
        public Langage CurrentLangage
        {
            get { return _currentLangage; }
            set
            {
                _currentLangage = value;
                Load();
                NotifyChanged("CurrentLangage");
            }
        }

        public string Localize(string key)
        {
            return _currentdict.ContainsKey(key) ? _currentdict[key] : "";
        }

        private void Load()
        {
            _currentdict = new Dictionary<string, string>();
        }
      
    }
}
