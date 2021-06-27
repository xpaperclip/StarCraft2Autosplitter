using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace StarCraft2Autosplitter
{
    public static class BankReader
    {
        public static IEnumerable<MissionTime> Read(string filename, ICampaign campaign)
        {
            var xml = TryLoadDocument(filename);
            if (xml == null) return null; // failed

            var age = GetSection(xml, campaign.MissionCompletedAge)
                        .ToDictionary(
                            kv => kv.Key,
                            kv => int.Parse(kv.Value)
                        );

            var times = from m in GetSection(xml, campaign.MissionBestTime)
                        let id = m.Key.Split('-')[0]
                        orderby age.GetValueOrDefault(id, 0) descending
                        select new MissionTime()
                        {
                            Id = id,
                            Time = int.Parse(m.Value)
                        };

            return times;
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
}
