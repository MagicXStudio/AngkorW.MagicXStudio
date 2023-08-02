namespace ImageStudio.Entities
{
    public class Detail
    {
        public Detail()
        {

        }
        public int Id { get; set; }

        public string Name { get; set; }


        public bool IsBlocked { get; set; }
        public string Title { get; set; }


        public Colors Colour { get; set; }
    }
}
