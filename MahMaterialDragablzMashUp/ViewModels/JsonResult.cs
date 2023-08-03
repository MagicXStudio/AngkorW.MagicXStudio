using Newtonsoft.Json;

namespace ImageStudio.ViewModels
{
    public class JsonResult
    {
        public JsonResult()
        {

        }
        [JsonProperty("data")]
        public List<Data> Data { get; set; }


        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }


    public class Data
    {
        public string id { get; set; }
        public string url { get; set; }
        public string short_url { get; set; }
        public int views { get; set; }
        public int favorites { get; set; }
        public string source { get; set; }
        public string purity { get; set; }

        public string category { get; set; }

        public int dimension_x { get; set; }

        public int dimension_y { get; set; }

        public string resolution { get; set; }

        public string ratio { get; set; }
        public int file_size { get; set; }

        public string file_type { get; set; }

        public DateTime created_at { get; set; }

        public List<string> colors { get; set; }
        public string path { get; set; }

        public Dictionary<string, string> thumbs { get; set; }
    }


    public class Meta
    {
        public int current_page { get; set; }//current_page

        public int last_page { get; set; }//45107

        public int per_page { get; set; }//24

        public int total { get; set; }//1082557

        public Dictionary<string, string> query { get; set; }

        public string seed { get; set; }
    }
}
