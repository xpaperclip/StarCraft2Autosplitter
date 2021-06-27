using System.Diagnostics;
using System.Linq;
using System.Net;

namespace StarCraft2Autosplitter
{
    public class Sc2ClientApi
    {
        private static WebClient wc = new WebClient();
        private static string base_url = "http://127.0.0.1:6119/";

        public static bool IsSc2Running()
        {
            return Process.GetProcesses().Where(p =>
            {
                var lp = p.ProcessName.ToLower();
                return lp == "sc2_x64" || lp == "sc2";
            }).Any();
        }

        public static bool HasGameStarted()
        {
            return (GameTime() ?? 0) > 0;
        }

        public static int? GameTime()
        {
            if (!IsSc2Running())
                return null;

            try
            {
                string ui = wc.DownloadString($"{base_url}ui/");
                if (ui.Contains("ScreenLoading"))
                    return null;

                string game = wc.DownloadString($"{base_url}game/");
                if (!game.Contains("\"type\":\"computer\""))
                    return null;

                if (game.Contains("displayTime"))
                    return (int)float.Parse(game.TrimBetween("displayTime\":", ","));
            }
            catch
            {
            }

            return 0;
        }
    }
}
