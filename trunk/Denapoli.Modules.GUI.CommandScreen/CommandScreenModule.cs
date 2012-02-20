using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Denapoli.Modules.Data;
using Denapoli.Modules.GUI.CommandScreen.ViewModel;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataProvider _dataProvider;

        [ImportingConstructor]
        public CommandScreenModule(IEventAggregator eventAggregator, IDataProvider dataProvider, ILoggerFacade logger, IPaymentService paymentService)
        {
            Logger = logger;
            PaymentService = paymentService;
            _eventAggregator = eventAggregator;
            _dataProvider = dataProvider;
        }

        public void Initialize()
        {
            _eventAggregator.GetEvent<NewCommandEvent>().Subscribe(NewCommandEventHandler);
        }

       

        private void NewCommandEventHandler(object obj)
        {
            _eventAggregator.GetEvent<ScreenChangedEvent>().Publish(new CommandScreenViewModel(_eventAggregator,_dataProvider, PaymentService));
        }
    }
}
