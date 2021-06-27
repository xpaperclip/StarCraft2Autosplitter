using System;
using System.IO;
using System.Threading;

namespace StarCraft2Autosplitter
{
    public class BankFileWatcher : IDisposable
    {
        public BankFileWatcher(AutosplitterComponent component)
        {
            this.component = component;

            var root_folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarCraft II");

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

        #region IDisposable

        private bool disposedValue;

        ~BankFileWatcher()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion


        private AutosplitterComponent component;
        private FileSystemWatcher fsw;

        private void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            ICampaign campaign;
            if (!Campaigns.TryGetCampaignForBankFilename(Path.GetFileName(e.Name), out campaign))
                return;

            component.ResetState(true);
        }

        private void fsw_Created(object sender, FileSystemEventArgs e)
        {
            ICampaign campaign;
            if (!Campaigns.TryGetCampaignForBankFilename(Path.GetFileName(e.Name), out campaign))
                return;

            component.UpdateState(e.FullPath, campaign);

            while (!Sc2ClientApi.HasGameStarted())
            {
                Thread.Sleep(100);
            }
            component.ResetState(false);
            component.Timer.Start();
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            ICampaign campaign;
            if (!Campaigns.TryGetCampaignForBankFilename(Path.GetFileName(e.Name), out campaign))
                return;

            component.UpdateState(e.FullPath, campaign);
        }
    }
}
