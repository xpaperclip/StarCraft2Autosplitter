using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraft2Autosplitter
{
    public struct MissionTime
    {
        public string Id { get; set; }
        public int Time { get; set; }
    }

    public static class MissionTimesWriter
    {
        public static void WriteMissionTimes(string filename, IEnumerable<MissionTime> missionTimes, ICampaign campaign)
        {
            using (var sw = new StreamWriter(filename))
            {
                foreach (var m in missionTimes)
                {
                    sw.WriteLine($"{campaign.GetMissionName(m.Id),-26} {TimeSpan.FromSeconds(m.Time)}");
                }
            }
        }
    }
}
