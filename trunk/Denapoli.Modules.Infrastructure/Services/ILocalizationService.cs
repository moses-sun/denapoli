using System.Collections.Generic;

namespace Denapoli.Modules.Infrastructure.Services
{
    public interface ILocalizationService
    {
        IEnumerable<Langage> AvailableLangages { get; }
        string Localize(string key);
        string Localize(string key, Langage langage);
        Langage CurrentLangage { get; set; }
        Dico Dico { get; }
    }

    public class Langage
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string DictURL
        {
            get { return Code + ".txt"; }
        }
        public string ImageURL
        {
            get { return Code + ".png"; }
        }
        public string IsDownloaded { get; set; }
    }
}
