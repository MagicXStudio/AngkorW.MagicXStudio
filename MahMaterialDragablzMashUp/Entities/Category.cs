using Microsoft.EntityFrameworkCore;

namespace ImageStudio.Entities
{
    public class Category
    {

        public Category() { }


        public int Id { get; set; }

        public string Name { get; set; }
    }
}
