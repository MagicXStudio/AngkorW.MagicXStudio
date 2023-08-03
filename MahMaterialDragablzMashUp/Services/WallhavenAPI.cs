using System.Net.Http;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using ImageStudio.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageStudio.Services
{
    public static class WallhavenAPI
    {
        private static HttpClient Client { get; set; }

        static WallhavenAPI()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://wallhaven.cc");
            Client.DefaultRequestHeaders.Add("X-API-Key", "T401JgkfBJXdgMkteJfGVDYXjjmWoBo5");
        }

        private static Task<string> GetJsonAsync(string name)
        {
            return Client.GetStringAsync(name);
        }

        /// <summary>
        /// GET https://wallhaven.cc/api/v1/w/94x38z
        /// </summary>
        public static async Task<JObject> GetDetail(string name)
        {
            string text = await Client.GetStringAsync(name);
            return JObject.Parse(text);
        }

        /// <summary>
        /// GET /api/v1/search
        /// </summary>
        public static async Task<JsonResult> Search(SearchParameter search)
        {
            string text = await Client.GetStringAsync("/api/v1/search");
            return JsonConvert.DeserializeObject<JsonResult>(text);
        }


        /// <summary>
        /// GET https://wallhaven.cc/api/v1/tag/1
        /// </summary>
        public static void tag() { }


        /// <summary>
        /// GET https://wallhaven.cc/api/v1/settings?apikey=****
        /// </summary>
        public static void settings() { }


        /// <summary>
        /// GET https://wallhaven.cc/api/v1/collections?apikey=****
        /// </summary>
        public static void collections() { }
    }
}
