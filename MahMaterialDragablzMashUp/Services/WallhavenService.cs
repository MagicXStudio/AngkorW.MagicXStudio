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

        public Detail GetDetail(string name)
        {
            return DBContext.Details.First(x => x.id == name);
        }

        public async Task<int> AddDetail(Detail item)
        {
            await DBContext.Details.AddAsync(item);
            return await DBContext.SaveChangesAsync();
        }

        public IEnumerable<Detail> GetAllDetail()
        {
            return DBContext.Details.OrderBy(x => x.id);
        }

        public void Search(SearchParameters search)
        {

        }
    }
}
