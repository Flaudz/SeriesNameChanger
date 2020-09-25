using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNameChanger.Classes.Api
{
    public class EpisodeInformationProperties
    {
        public class Crew
        {
            public int id { get; set; }
            public string credit_id { get; set; }
            public string name { get; set; }
            public string department { get; set; }
            public string job { get; set; }
            public int gender { get; set; }
            public string profile_path { get; set; }
        }

        public class GuestStar
        {
            public int id { get; set; }
            public string name { get; set; }
            public string credit_id { get; set; }
            public string character { get; set; }
            public int order { get; set; }
            public int gender { get; set; }
            public string profile_path { get; set; }
        }

        public class Episode
        {
            public string air_date { get; set; }
            public int episode_number { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string overview { get; set; }
            public string production_code { get; set; }
            public int season_number { get; set; }
            public int show_id { get; set; }
            public string still_path { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }
            public List<Crew> crew { get; set; }
            public List<GuestStar> guest_stars { get; set; }
        }

        public class Root
        {
            public string _id { get; set; }
            public string air_date { get; set; }
            public List<Episode> episodes { get; set; }
            public string name { get; set; }
            public string overview { get; set; }
            public int id { get; set; }
            public string poster_path { get; set; }
            public int season_number { get; set; }
        }

    }
}
