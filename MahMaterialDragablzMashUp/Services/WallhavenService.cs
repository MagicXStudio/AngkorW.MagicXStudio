using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageStudio.ViewModels;

namespace ImageStudio.Services
{
    public class WallhavenService
    {
        private WallhavenContext DBContext { get; }

        public WallhavenService()
        {
            DBContext = new WallhavenContext();
        }

        /// <summary>
        /// GET https://wallhaven.cc/api/v1/w/94x38z
        /// </summary>
        public void Get(string id)
        {
            DBContext.Tags.Where(x => x.Name == id);
        }

        /// <summary>
        /// GET https://wallhaven.cc/api/v1/search
        /// </summary>
        public void Search(SearchParameter search)
        {

        }


        /// <summary>
        /// GET https://wallhaven.cc/api/v1/tag/1
        /// </summary>
        public void tag() { }


        /// <summary>
        /// GET https://wallhaven.cc/api/v1/settings?apikey=****
        /// </summary>
        public void settings() { }


        /// <summary>
        /// GET https://wallhaven.cc/api/v1/collections?apikey=****
        /// </summary>
        public void collections() { }
    }
}
