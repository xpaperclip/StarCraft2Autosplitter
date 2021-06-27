using System.Collections.Generic;

namespace StarCraft2Autosplitter
{
    public class ZCampaign : ICampaign
    {
        public ZCampaign()
        {
            _missions = LoadMissionList();
        }

        private readonly Dictionary<string, string> _missions;

        public string Name => "Heart of the Swarm";

        public string BankFilename => "ZCampaign.SC2Bank";
        public string MissionCompletedAge => "ZCampaign|MissionCompletedAge";
        public string MissionBestTime => "ZCampaign|MissionBestTime";

        public string GetMissionName(string id)
        {
            return _missions.GetValueOrDefault(id);
        }

        private static Dictionary<string, string> LoadMissionList()
        {
            var missions = new Dictionary<string, string>();
            missions["ZLab01"] = "Lab Rat";
            missions["ZLab02"] = "Back in the Saddle";
            missions["ZLab03"] = "Rendezvous";
            missions["ZKaldir01"] = "Harvest of Screams";
            missions["ZKaldir02"] = "Shoot the Messenger";
            missions["ZKaldir03"] = "Enemy Within";
            missions["ZChar01"] = "Domination";
            missions["ZChar02"] = "Fire in the Sky";
            missions["ZChar03"] = "Old Soldiers";
            missions["ZZerus01"] = "Waking the Ancient";
            missions["ZZerus02"] = "The Crucible";
            missions["ZZerus03"] = "Supreme";
            missions["ZHybrid01"] = "Infested";
            missions["ZHybrid02"] = "Hand of Darkness";
            missions["ZHybrid03"] = "Phantoms of the Void";
            missions["ZSpace01"] = "With Friends Like These";
            missions["ZSpace02"] = "Conviction";
            missions["ZExpedition01"] = "Planetfall";
            missions["ZExpedition02"] = "Death From Above";
            missions["ZExpedition03"] = "The Reckoning";
            return missions;
        }
    }
}
