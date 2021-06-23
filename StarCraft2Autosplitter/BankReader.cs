using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using StarCraft2Autosplitter.Sc2Campaign;

namespace StarCraft2Autosplitter
{
    public class BankReader
    {
        public BankReader(ICampaign campaign)
        {
            this.campaign = campaign;
        }

        private ICampaign campaign;
        private int missionsDoneLast = 0;

        public event EventHandler<MissionDoneEventArgs> MissionDone;

        public void Reset()
        {
            missionsDoneLast = 0;
        }

        public void Update(string filename)
        {
            var xml = TryLoadDocument(filename);
            if (xml == null) return; // failed

            var age = GetSection(xml, campaign.MissionCompletedAge)
                        .ToDictionary(
                            kv => kv.Key,
                            kv => int.Parse(kv.Value)
                        );

            var times = (from m in GetSection(xml, campaign.MissionBestTime)
                         let num = m.Key.Split('-')[0]
                         select new
                         {
                             Id = num,
                             Time = int.Parse(m.Value)
                         }).ToDictionary(m => m.Id, m => m.Time);
            var count = times.Count();
            var order = times
                .OrderByDescending(m => age.GetValueOrDefault(m.Key, 0))
                .Select(m => m.Key);

            if (count != missionsDoneLast)
            {
                var last_mission = order.Last();

                MissionDone?.Invoke(this, new MissionDoneEventArgs()
                {
                    LastMissionId = last_mission,
                    TimeTaken = times[last_mission],
                    TotalTimeTaken = times.Values.Sum()
                });
            }
            missionsDoneLast = count;

            using (var sw = new StreamWriter(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "times.txt")))
            {
                foreach (var m in order)
                {
                    sw.WriteLine($"{campaign.GetMissionName(m),-26} {TimeSpan.FromSeconds(times[m])}");
                }
            }
        }

        private static XDocument TryLoadDocument(string filename)
        {
            int tries = 0;
            while (tries++ < 10)
            {
                try
                {
                    return XDocument.Load(filename);
                }
                catch (IOException)
                {
                    // retry a few times in case the client is still writing
                    System.Threading.Thread.Sleep(100);
                }
            }
            return null;
        }

        private static IEnumerable<KeyValuePair<string, string>> GetSection(XDocument xml, string sectionName, string valueType = "int")
        {
            var section = xml.Element("Bank")
                .Elements("Section")
                .Where(el => el.Attribute("name").Value == sectionName)
                .FirstOrDefault();

            if (section == null)
                return Enumerable.Empty<KeyValuePair<string, string>>();

            return from kv in section.Elements()
                   select new KeyValuePair<string, string>(
                            kv.Attribute("name").Value,
                            kv.Element("Value").Attribute(valueType).Value);
        }
    }

    public class MissionDoneEventArgs : EventArgs
    {
        public string LastMissionId { get; set; }
        public int TimeTaken { get; set; }
        public int TotalTimeTaken { get; set; }
    }
}
