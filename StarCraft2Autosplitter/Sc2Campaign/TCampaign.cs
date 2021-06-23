using System.Collections.Generic;

namespace StarCraft2Autosplitter.Sc2Campaign
{
    public class TCampaign : ICampaign
    {
        public TCampaign()
        {
            _missions = LoadMissionList();
        }

        private readonly Dictionary<string, string> _missions;

        public string BankFilename => "TCampaign.SC2Bank";
        public string MissionCompletedAge => "MissionCompletedAge";
        public string MissionBestTime => "MissionBestTime";

        public string GetMissionName(string id)
        {
            return _missions.GetValueOrDefault(id);
        }

        private static Dictionary<string, string> LoadMissionList()
        {
            var missions = new Dictionary<string, string>();
            missions["1"] = "Liberation Day";
            missions["2"] = "The Outlaws";
            missions["3"] = "Zero Hour";
            missions["4"] = "The Evacuation";
            missions["5"] = "Outbreak";
            missions["6"] = "Safe Haven";
            missions["7"] = "Haven's Fall";
            missions["8"] = "The Devil's Playground";
            missions["9"] = "Welcome to the Jungle";
            missions["10"] = "Breakout";
            missions["11"] = "Ghost of a Chance";
            missions["12"] = "The Great Train Robbery";
            missions["13"] = "Cutthroat";
            missions["14"] = "Engine of Destruction";
            missions["15"] = "Media Blitz";
            missions["16"] = "Piercing the Shroud";
            missions["17"] = "Smash and Grab";
            missions["18"] = "The Dig";
            missions["19"] = "The Moebius Factor";
            missions["20"] = "Supernova";
            missions["21"] = "Maw of the Void";
            missions["22"] = "Whispers of Doom";
            missions["23"] = "A Sinister Turn";
            missions["24"] = "Echoes of the Future";
            missions["25"] = "In Utter Darkness";
            missions["26"] = "Gates of Hell";
            missions["27"] = "Belly of the Beast";
            missions["28"] = "Shatter the Sky";
            missions["29"] = "All-In";
            return missions;
        }
    }
}
