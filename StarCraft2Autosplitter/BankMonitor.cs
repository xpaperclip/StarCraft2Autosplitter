using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using StarCraft2Autosplitter.Sc2Campaign;

namespace StarCraft2Autosplitter
{
    public class BankMonitor : IDisposable
    {
        public BankMonitor(AutosplitterComponent component)
        {
            this.component = component;

            // find latest bank
            var root_folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarCraft II");
            var bank_files = new HashSet<string>(Campaigns.All.Select(c => c.BankFilename));
            var filename = (from f in Directory.GetFiles(root_folder, "*.SC2Bank", SearchOption.AllDirectories)
                            where bank_files.Contains(f)
                            select new FileInfo(f))
                            .OrderByDescending(f => f.LastWriteTime)
                            .Select(f => f.FullName)
                            .FirstOrDefault();
            
            campaign = new TCampaign();
            reader = new BankReader(campaign);

            if (!string.IsNullOrWhiteSpace(filename))
                reader.Update(filename);

            reader.MissionDone += reader_MissionDone;

            // start watching
            fsw = new FileSystemWatcher(root_folder);
            fsw.IncludeSubdirectories = true;
            fsw.Created += fsw_Created;
            fsw.Changed += fsw_Changed;
            fsw.Deleted += fsw_Deleted;
            fsw.EnableRaisingEvents = true;
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    fsw.Dispose();
                }

                disposedValue = true;
            }
        }

        ~BankMonitor()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        private void reader_MissionDone(object sender, MissionDoneEventArgs e)
        {
            component.TimeTakenSoFar = e.TotalTimeTaken;
            component.Timer.CurrentState.SetGameTime(TimeSpan.FromSeconds(e.TotalTimeTaken));
            component.Timer.Split();
        }

        private AutosplitterComponent component;
        private FileSystemWatcher fsw;
        private ICampaign campaign;
        private BankReader reader;
        private bool disposedValue;

        private void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            if (Path.GetFileName(e.Name) != campaign.BankFilename)
                return;

            component.SettingsForm.SetBankPath(e.FullPath);
            component.Timer.Reset();
            reader.Reset();
        }

        private void fsw_Created(object sender, FileSystemEventArgs e)
        {
            if (Path.GetFileName(e.Name) != campaign.BankFilename)
                return;

            component.SettingsForm.SetBankPath(e.FullPath);

            while (!Sc2ClientApi.HasGameStarted())
            {
                Thread.Sleep(100);
            }

            component.Timer.Start();
            reader.Reset();
            reader.Update(e.FullPath);
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            if (Path.GetFileName(e.Name) != campaign.BankFilename)
                return;

            component.SettingsForm.SetBankPath(e.Name);
            reader.Update(e.FullPath);
        }
    }
}
