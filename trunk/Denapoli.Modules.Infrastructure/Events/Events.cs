using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.Infrastructure.Events
{
    public class ScreenChangedEvent : CompositePresentationEvent<AbstractScreenViewModel> { }
    public class NewCommandEvent : CompositePresentationEvent<object> { }
    public class EndCommandEvent : CompositePresentationEvent<AbstractScreenViewModel> { }

    public class ImprimerEvent : CompositePresentationEvent<object> { }
    public class PreparerEvent : CompositePresentationEvent<object> { }
    public class PreteEvent : CompositePresentationEvent<object> { }
    public class LivrerEvent : CompositePresentationEvent<object> { }
}