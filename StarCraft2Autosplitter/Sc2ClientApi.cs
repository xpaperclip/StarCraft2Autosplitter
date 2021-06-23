using System.Net;

namespace StarCraft2Autosplitter
{
    public class Sc2ClientApi
    {
        private static WebClient wc = new WebClient();
        private static string base_url = "http://127.0.0.1:6119/";

        public static bool HasGameStarted()
        {
            try
            {
                string ui = wc.DownloadString($"{base_url}ui/");
                if (ui.Contains("ScreenLoading"))
                    return false;

                string game = wc.DownloadString($"{base_url}game/");
                if (!game.Contains("\"type\":\"computer\""))
                    return false;

                if (game.Contains("displayTime"))
                    return float.Parse(game.TrimBetween("displayTime\":", ",")) > 0;
            }
            catch
            {
            }

            return false;
        }

        public static int? GameTime()
        {
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
