using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Services;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class FamillesAdminViewModel
    {
        public IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        protected ObservableCollection<Famille> Failles { get; set; }

        [ImportingConstructor]
        public FamillesAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            Failles = new ObservableCollection<Famille>();
        }

    }
}