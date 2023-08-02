using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageStudio.Entities;
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

        public Tag GetTag(string name)
        {
            return DBContext.Tags.First(x => x.Name == name);
        }


        /// <summary>
        /// GET https://wallhaven.cc/api/v1/w/94x38z
        /// </summary>
        public Detail GetDetail(string name)
        {
            return DBContext.Details.First(x => x.Name == name);
        }

        public IEnumerable<Detail> GetAllDetail()
        {
            return DBContext.Details.OrderBy(x => x.Id);
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
