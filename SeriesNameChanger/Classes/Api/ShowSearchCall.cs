using Newtonsoft.Json;
using System.Net;
using static MovieNameChanger.Classes.Api.ShowSearchProperties;
using static MovieNameChanger.Classes.Api.SeasonCountProperties;
using static MovieNameChanger.Classes.Api.EpisodeInformationProperties;

namespace MovieNameChanger.Classes.Api
{
    public class ShowSearchCall
    {
        public ShowSearchProperties.Root GetShowId(string showname)
        {
            string showName = showname.Replace(" ", "%26");
            string json = new WebClient().DownloadString($@"https://api.themoviedb.org/3/search/tv?api_key=0a34d1c5f077444133ab367be386561a&language=en-US&page=1&query={showName}&include_adult=false");
            ShowSearchProperties.Root myDeserializedClass = JsonConvert.DeserializeObject<ShowSearchProperties.Root>(json);
            return myDeserializedClass;
        }

        public SeasonCountProperties.Root GetShowSeasonCount(string id)
        {
            string json = new WebClient().DownloadString($@"https://api.themoviedb.org/3/tv/{id}?api_key=0a34d1c5f077444133ab367be386561a&language=en-US");
            SeasonCountProperties.Root myDeserializedClass = JsonConvert.DeserializeObject<SeasonCountProperties.Root>(json);
            return myDeserializedClass;
        }

        public EpisodeInformationProperties.Root GetEpisodeInformation(string season, string id)
        {
            string json = new WebClient().DownloadString($@"https://api.themoviedb.org/3/tv/{id}/season/{season}?api_key=0a34d1c5f077444133ab367be386561a&language=en-US");
            EpisodeInformationProperties.Root myDeserializedClass = JsonConvert.DeserializeObject<EpisodeInformationProperties.Root>(json);
            return myDeserializedClass;
        }
    }
}
