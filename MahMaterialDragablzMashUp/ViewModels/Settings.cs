namespace ImageStudio.ViewModels
{
    public class Settings
    {

        public Settings()
        {

        }
        public static Settings Default => new Settings();

        public string SavePath { get; set; }

        public int Threads { get; set; }

        public string Apikey { get; set; }
    }
}
