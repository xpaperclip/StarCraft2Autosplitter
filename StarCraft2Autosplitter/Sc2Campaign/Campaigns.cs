namespace StarCraft2Autosplitter.Sc2Campaign
{
    public static class Campaigns
    {
        private static ICampaign TCampaign = new TCampaign();
        //private static ICampaign ZCampaign = new ZCampaign();
        //private static ICampaign PCampaign = new PCampaign();

        public static ICampaign[] All = new ICampaign[] { TCampaign };
    }

    public interface ICampaign
    {
        string BankFilename { get; }
        string MissionCompletedAge { get; }
        string MissionBestTime { get; }

        string GetMissionName(string id);
    }
}
