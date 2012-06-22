using System;
using System.ComponentModel.Composition;
using Denapoli.Modules.Data;
using Denapoli.Modules.GUI.CommandScreen.View;
using Denapoli.Modules.GUI.CommandScreen.ViewModel;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.GUI.CommandScreen
{


    [ModuleExport(typeof(CommandScreenModule), DependsOnModuleNames = new[] { "DataModule", "PaymentModule" })]
    public class CommandScreenModule : IModule
    {
        private ILoggerFacade Logger { get; set; }
        public IPaymentService PaymentService { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataProvider _dataProvider;

        [ImportingConstructor]
        public CommandScreenModule(IEventAggregator eventAggregator, IDataProvider dataProvider, ILoggerFacade logger, IPaymentService paymentService, ILocalizationService localizationService)
        {
            Logger = logger;
            PaymentService = paymentService;
            LocalizationService = localizationService;
            _eventAggregator = eventAggregator;
            _dataProvider = dataProvider;
        }

        private CommandScreenView _view;

        [Import]
        private CommandScreenView View
        {
            get { return _view; }
            set
            {
                _view = value;
                var t = new CommandScreenViewModel(_eventAggregator, _dataProvider, PaymentService, LocalizationService) {View = value, IsVisible = false};
                t.Cancel();
            }
        }

        public void Initialize()
        {
            _eventAggregator.GetEvent<NewCommandEvent>().Subscribe(NewCommandEventHandler);
        }

        private void NewCommandEventHandler(object obj)
        {
            var screen = new CommandScreenViewModel(_eventAggregator, _dataProvider, PaymentService,LocalizationService) {View = View};
            _eventAggregator.GetEvent<ScreenChangedEvent>().Publish(screen);
        }
    }
}
