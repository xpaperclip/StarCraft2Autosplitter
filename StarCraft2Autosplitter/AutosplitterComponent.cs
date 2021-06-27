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

            settingsForm = new SettingsForm();
            settingsForm.Component = this;

            timer = new TimerModel();
            timer.CurrentState = state;

            watcher = new BankFileWatcher(this);
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
            IgtUpdate = SettingsHelper.ParseBool(element["IgtUpdate"], true);
            WriteIgtFile = SettingsHelper.ParseBool(element["WriteIgtFile"], true);
            IgtFile = SettingsHelper.ParseString(element["IgtFile"], Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "StarCraft II Speedrun Times.txt"));
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0")
                ^ SettingsHelper.CreateSetting(document, parent, "IgtUpdate", IgtUpdate)
                ^ SettingsHelper.CreateSetting(document, parent, "WriteIgtFile", WriteIgtFile)
                ^ SettingsHelper.CreateSetting(document, parent, "IgtFile", IgtFile);
        }


        private bool _igtUpdate = true;
        public bool IgtUpdate
        {
            get { return _igtUpdate; }
            set
            {
                if (_igtUpdate != value)
                {
                    _igtUpdate = value;
                    settingsForm.Update_IgtUpdate(value);
                    state.IsGameTimeInitialized = value;
                }
            }
        }

        private bool _writeIgtFile = true;
        public bool WriteIgtFile
        {
            get { return _writeIgtFile; }
            set
            {
                if (_writeIgtFile != value)
                {
                    _writeIgtFile = value;
                    settingsForm.Update_WriteIgtFile(value);
                }
            }
        }

        private string _igtFile;
        public string IgtFile
        {
            get { return _igtFile; }
            set
            {
                if (_igtFile != value)
                {
                    _igtFile = value;
                    settingsForm.Update_IgtFile(value);
                }
            }
        }

        #endregion

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
            settingsForm.SetCampaign(campaign);
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
            if (WriteIgtFile && !string.IsNullOrWhiteSpace(IgtFile))
                MissionTimesWriter.WriteMissionTimes(IgtFile, missions, campaign);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (!IgtUpdate)
                return;

            var time = Sc2ClientApi.GameTime();
            var loading = !time.HasValue;
            state.IsGameTimePaused = loading;
            if (!loading && (time != lastGameTime))
                state.SetGameTime(TimeSpan.FromSeconds(timeTakenSoFar + time.Value));
            lastGameTime = time;
        }
    }
}
