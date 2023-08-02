using System.Net.Http;
using System.Text.Json.Nodes;
using ImageStudio.ViewModels;
using Newtonsoft.Json.Linq;

namespace ImageStudio.Services
{
    public static class WallhavenAPI
    {
        private static HttpClient Client { get; set; }

        static WallhavenAPI()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
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
            string text = await GetJsonAsync(name);
            return JObject.Parse(text);
        }

        /// <summary>
        /// GET https://wallhaven.cc/api/v1/search
        /// </summary>
        public static void Search(SearchParameter search)
        {

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
