using System.Collections.Generic;
using System.Linq;

namespace StarCraft2Autosplitter
{
    public static class Campaigns
    {
        static Campaigns()
        {
            _campaigns = new ICampaign[] {
                new TCampaign(), 
                new ZCampaign(),
            };

            _filenames = _campaigns.ToDictionary(c => c.BankFilename.ToLower());
        }

        private static ICampaign[] _campaigns;
        private static Dictionary<string, ICampaign> _filenames;

        public static IEnumerable<ICampaign> All => _campaigns;
        public static bool TryGetCampaignForBankFilename(string filename, out ICampaign campaign)
        {
            return _filenames.TryGetValue(filename.ToLower(), out campaign);
        }
    }

    public interface ICampaign
    {
        string BankFilename { get; }
        string MissionCompletedAge { get; }
        string MissionBestTime { get; }

        string GetMissionName(string id);
    }
}
