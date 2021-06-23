using System;
using LiveSplit.UI.Components;
using LiveSplit.Model;

[assembly: ComponentFactory(typeof(StarCraft2Autosplitter.AutosplitterComponentFactory))]

namespace StarCraft2Autosplitter
{
    public class AutosplitterComponentFactory : IComponentFactory
    {
        public ComponentCategory Category => ComponentCategory.Control;

        public string ComponentName => "StarCraft II Autosplitter";

        public string Description => "";

        public string UpdateName => "StarCraft II";

        public string UpdateURL => "";

        public Version Version => Version.Parse("1.0");

        public string XMLURL => UpdateURL + "updates.xml";

        public IComponent Create(LiveSplitState state)
        {
            return new AutosplitterComponent(state);
        }
    }
}
