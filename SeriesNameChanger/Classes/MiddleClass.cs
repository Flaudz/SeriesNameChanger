using MovieNameChanger.Classes.Api;
using System.Linq;
using static MovieNameChanger.Classes.Api.ShowSearchProperties;
using static MovieNameChanger.Classes.Api.SeasonCountProperties;
using static MovieNameChanger.Classes.Api.EpisodeInformationProperties;
using System.IO;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace MovieNameChanger.Classes
{
    public class MiddleClass
    {
        Api.ShowSearchCall apiCall = new Api.ShowSearchCall();
        private int count;
        private List list = new List();
        public ShowSearchCall ApiCall { get => apiCall; }
        public int Count { get => count; set => count = value; }
        public List List { get => list; set => list = value; }

        public void SeriesSearch(string seriesName, string season, string[] files, string filename)
        {
            Array.Sort(files);
            count = 0;
            ShowSearchProperties.Root Series = ApiCall.GetShowId(seriesName);
            for (int i = 0; count < 1; i++)
            {
                if (Series.results[i].name == seriesName)
                {
                    EpisodeInformationMetode(Series.results[i].id.ToString(), seriesName, season, files, filename);
                    count++;
                }
            }
        }

        public void EpisodeInformationMetode(string id, string seriesName, string season, string[] files, string filename)
        {
            Array.Sort(files);
            EpisodeInformationProperties.Root EpisodeInformation = ApiCall.GetEpisodeInformation(season, id);
            int i = 0;
            foreach (string file in files)
            {   
                string asd = Path.GetFileName(files[i]);
                string filetype = file.Split('.').Last();
                string fullLocation = file.Replace(@"\", "/");
                string location = $@"{fullLocation.Replace(asd, "")}{i+1}.{filetype}";
                string location2 = $@"{fullLocation}{seriesName}.S{season}.EP{EpisodeInformation.episodes[i].episode_number}.{EpisodeInformation.episodes[i].name}.{filetype}";
                string idontknow = location2.Replace(asd, "");
                string finallocation = idontknow.Replace("\"", "");
                i++;
                File.Move(location, finallocation);
            }

            foreach (string file in files)
            {
                string fullLocation = file.Replace(@"\", "/");
                File.Delete($"{fullLocation}");
            }
        }
    }
}
