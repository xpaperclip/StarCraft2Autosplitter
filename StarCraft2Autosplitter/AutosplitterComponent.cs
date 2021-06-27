using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            watcher = new BankFileWatcher(this);

            // default settings
            TimesFilename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "times.txt");
        }

        public override void Dispose()
        {
            watcher.Dispose();
            settingsForm.Dispose();
        }

        private LiveSplitState state;
        private TimerModel timer;
        private BankFileWatcher watcher;
        private SettingsForm settingsForm;

        public ITimerModel Timer => timer;

        #region Settings

        public override string ComponentName => "StarCraft II Autosplitter";

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

        public string TimesFilename { get; set; }

        private int timeTakenSoFar = 0;
        private int missionsDoneLast = 0;
        private int? lastGameTime;

        public void ResetState(bool resetTimer)
        {
            if (resetTimer)
                Timer.Reset();

            timeTakenSoFar = 0;
            missionsDoneLast = 0;
        }

        public void UpdateState(string filename, ICampaign campaign)
        {
            settingsForm.SetBankPath(filename);

            var missions = BankReader.Read(filename, campaign);

            var count = missions.Count();
            if (count != missionsDoneLast)
            {
                var last_mission = missions.Last().Id;
                var time_taken = missions.Select(m => m.Time).Sum();

                timeTakenSoFar = time_taken;
                Timer.CurrentState.SetGameTime(TimeSpan.FromSeconds(time_taken));
                Timer.Split();
            }
            missionsDoneLast = count;

            // write mission times to file if requested
            if (!string.IsNullOrWhiteSpace(TimesFilename))
                MissionTimesWriter.WriteMissionTimes(TimesFilename, missions, campaign);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            var time = Sc2ClientApi.GameTime();
            var loading = !time.HasValue;
            state.IsGameTimePaused = loading;
            if (!loading && (time != lastGameTime))
                state.SetGameTime(TimeSpan.FromSeconds(timeTakenSoFar + time.Value));
            lastGameTime = time;
        }
    }
}
