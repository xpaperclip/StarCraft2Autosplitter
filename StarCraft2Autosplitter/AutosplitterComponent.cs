using System;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
namespace StarCraft2Autosplitter
{
    public class AutosplitterComponent : LogicComponent
    {
        public AutosplitterComponent(LiveSplitState state)
        {
            this.state = state;
            state.IsGameTimeInitialized = true;

            settingsForm = new SettingsForm();

            timer = new TimerModel();
            timer.CurrentState = state;

            monitor = new BankMonitor(this);
        }

        public override void Dispose()
        {
            monitor.Dispose();
        }

        private TimerModel timer;
        private LiveSplitState state;
        private BankMonitor monitor;
        private SettingsForm settingsForm;

        public ITimerModel Timer => timer;

        #region Settings

        public override string ComponentName => "StarCraft II Autosplitter";
        public SettingsForm SettingsForm => settingsForm;

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return settingsForm;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public override void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            //UseSegmentTimes = SettingsHelper.ParseBool(element["AutoSplit"]);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0");
            //^ SettingsHelper.CreateSetting(document, parent, "AutoSplit", AutoSplit);
        }

        #endregion

        public int TimeTakenSoFar { get; set; } = 0;

        private int? lastGameTime;

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            var time = Sc2ClientApi.GameTime();
            var loading = !time.HasValue;
            state.IsGameTimePaused = loading;
            if (!loading && (time != lastGameTime))
                state.SetGameTime(TimeSpan.FromSeconds(TimeTakenSoFar + time.Value));
            lastGameTime = time;
        }
    }
}
