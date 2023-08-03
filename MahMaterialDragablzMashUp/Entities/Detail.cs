using ImageStudio.ViewModels;

namespace ImageStudio.Entities
{
    public class Detail : Data
    {
        public Detail()
        {

        }

        public bool IsBlocked { get; set; }


        public Colors Colour { get; set; }
    }
}
