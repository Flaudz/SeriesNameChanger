using MovieNameChanger.Classes.Api;
using System.Linq;
using static MovieNameChanger.Classes.Api.ShowSearchProperties;
using static MovieNameChanger.Classes.Api.SeasonCountProperties;
using static MovieNameChanger.Classes.Api.EpisodeInformationProperties;
using System.IO;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using SeriesNameChanger;

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

        public void SeriesSearch(string seriesName, string season, string[] files, string fileLocation, string fileType, string oldFileLocation, int j)
        {
            count = 0;
            ShowSearchProperties.Root Series = ApiCall.GetShowId(seriesName);
            for (int i = 0; i < 1; i++)
            {
                if (Series.results[i].name == seriesName)
                {
                    try
                    {
                        EpisodeInformationMetode(Series.results[i].id.ToString(), seriesName, season, files, fileLocation, fileType, oldFileLocation, j);
                        
                        count++;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        public List<string> GetSeriesNames(string seriesname)
        {
            List<string> names = new List<string>();
            try
            {
                ShowSearchProperties.Root series = ApiCall.GetShowId(seriesname);
                if(series.results.Count != 0)
                {
                    if(series.results.Count < 6)
                    {
                        foreach (var name in series.results)
                        {
                            if (!names.Contains(name.name))
                            {
                                names.Add($"{name.name}");
                            }
                        }
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        if (!names.Contains(series.results[i].name))
                        {
                            names.Add($"{series.results[i].name}");
                        }
                        
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return names;
        }

        public List<string> GetSeason(string name)
        {
            List<string> SeasonCount = new List<string>();
            ShowSearchProperties.Root series = ApiCall.GetShowId(name);
            SeasonCountProperties.Root seasonCounter = ApiCall.GetShowSeasonCount(series.results[0].id.ToString());
            int seasonCount = seasonCounter.seasons.Count - 1;
            for (int i = 0; i <= seasonCount; i++)
            {
                SeasonCount.Add(i.ToString());
            }
            return SeasonCount;
        }

        public void EpisodeInformationMetode(string id, string seriesName, string season, string[] files, string fileLocation, string fileType, string oldFileLocation, int i)
        {
            EpisodeInformationProperties.Root EpisodeInformation = ApiCall.GetEpisodeInformation(season, id);
            
            string secondFinalLocation = $@"{fileLocation}{seriesName}.S{season}.Ep{EpisodeInformation.episodes[i].episode_number}.{EpisodeInformation.episodes[i].name}.{fileType}";
            string finalLocation = secondFinalLocation.Replace("/", @"\");
            
            Console.WriteLine(finalLocation);
            File.Move(oldFileLocation, finalLocation);
            File.Delete($"{oldFileLocation}");
        }

        public string getId(string name, string season, int i, string fileLocation, string fileType)
        {
            ShowSearchProperties.Root Series = ApiCall.GetShowId(name);
            string finalLocation = GetFinalNames(Series.results[0].id.ToString(), name, season, fileLocation, fileType, i);
            return finalLocation;
        }

        public string GetFinalNames(string id, string seriesName, string season, string fileLocation, string fileType, int i)
        {
            EpisodeInformationProperties.Root EpisodeInformation = ApiCall.GetEpisodeInformation(season, id);

            string secondFinalLocation = $@"{fileLocation}{seriesName}.S{season}.Ep{EpisodeInformation.episodes[i].episode_number}.{EpisodeInformation.episodes[i].name}.{fileType}";
            string finalLocation = secondFinalLocation.Replace("/", @"\");

            return finalLocation;
        }

        public int GetSeriesId(string seriesName, string season)
        {
            ShowSearchProperties.Root Series = ApiCall.GetShowId(seriesName);
            int i = NumberOfEpisodes(Series.results[0].id.ToString(), season);
            return i;
        }

        public int NumberOfEpisodes(string id, string season)
        {
            EpisodeInformationProperties.Root epInfo = ApiCall.GetEpisodeInformation(season, id);
            return epInfo.episodes.Count;
        }

        public List<string> PickMovieString(string movieName)
        {
            List<string> stringOfMovies = new List<string>();
            MovieProperties.Root GetMovie = ApiCall.GetMovieInformation(movieName);
            for (int i = 0; i < 5; i++)
            {
                stringOfMovies.Add(GetMovie.results[i].title);
            }
            return stringOfMovies;

        }

        public void GetMovieName(string movieName, string file)
        {
            MovieProperties.Root GetMovie = ApiCall.GetMovieInformation(movieName);
            string finalName = $"{GetMovie.results[0].title}({GetMovie.results[0].release_date.Split('-').First()})";
            file = file.Replace(@"\", "/");
            string filename = file.Split('/').Last();
            string fileLocation = file.Replace(filename, "");
            string fileType = file.Split('.').Last();
            Console.WriteLine($"FileName: {filename}");
            Console.WriteLine($"FileLocation: {fileLocation}");
            File.Move(file, $"{fileLocation}{finalName}.{fileType}");
            File.Delete(file);
        }
    }
}
