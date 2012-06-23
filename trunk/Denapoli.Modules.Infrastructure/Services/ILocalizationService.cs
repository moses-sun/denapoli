using System.Collections.Generic;

namespace Denapoli.Modules.Infrastructure.Services
{
    public interface ILocalizationService
    {
        IEnumerable<Langage> AvailableLangages { get; }
        string Localize(string key);
        string Localize(string key, Langage langage);
        void ModifyLocaLization(string key, string traduction, Langage language);
        void SendDocs();
        Langage CurrentLangage { get; set; }
        Dico Dico { get; }
        List<string> Keys { get; }
        void Reset();


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
