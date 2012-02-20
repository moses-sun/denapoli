using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;

namespace Denapoli.Modules.Infrastructure.Behavior
{
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        public void OnImportsSatisfied()
        {
            AddRegisteredViews();
        }

        private void AddRegisteredViews()
        {
            if (Region != null)
            {
                foreach (var viewEntry in RegisteredViews)
                {
                    if (viewEntry.Metadata.RegionName == Region.Name)
                    {
                        var view = viewEntry.Value;
                        if (!Region.Views.Contains(view))
                        {
                            Region.Add(view);
                        }
                    }
                }
            }
        }

        [ImportMany(AllowRecomposition = true)]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }
    }
}