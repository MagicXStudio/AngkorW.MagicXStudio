using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain
{
    public class TreeExampleSimpleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? PlanetTemplate { get; set; }

        public DataTemplate? SolarSystemTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is Planet)
                return PlanetTemplate;

            if (item?.ToString() == "Solar System")
                return SolarSystemTemplate;

            return TreeViewAssist.SuppressAdditionalTemplate;
        }
    }

    public sealed class Movie
    {
        public Movie(string name)
        {
            Name = name;
            Director = name;
        }

        public string Name { get; }

        public string Director { get; }
    }

    public class Planet
    {
        public string? Name { get; set; }

        public double DistanceFromSun { get; set; }

        public double DistanceFromEarth { get; set; }

        public double Velocity { get; set; }
    }

    public sealed class MovieCategory
    {
        public MovieCategory(string dir)
        {
            Name = dir;
            Movies = new ObservableCollection<Movie>(Directory.GetFiles(dir).Select(file => new Movie(file)));
        }

        public string Name { get; }

        public ObservableCollection<Movie> Movies { get; }
    }

    public sealed class TreesViewModel : ViewModelBase
    {
        private object? _selectedItem;

        public string[] Drives => System.IO.Directory.GetLogicalDrives();

        public ObservableCollection<MovieCategory> MovieCategories { get; }

        public AnotherCommandImplementation AddCommand { get; }

        public AnotherCommandImplementation RemoveSelectedItemCommand { get; }

        public object? SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public TreesViewModel()
        {
            var items = Drives.Select(x => new MovieCategory(x));

            MovieCategories = new ObservableCollection<MovieCategory>(items);

            AddCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (!MovieCategories.Any())
                    {
                        MovieCategories.Add(new MovieCategory(GenerateString(15)));
                    }
                    else
                    {
                        var index = new Random().Next(0, MovieCategories.Count);

                        MovieCategories[index].Movies.Add(
                            new Movie(GenerateString(15)));
                    }
                });

            RemoveSelectedItemCommand = new AnotherCommandImplementation(
                _ =>
                {
                    var movieCategory = SelectedItem as MovieCategory;
                    if (movieCategory != null)
                    {
                        MovieCategories.Remove(movieCategory);
                    }
                    else
                    {
                        var movie = SelectedItem as Movie;
                        if (movie == null) return;
                        MovieCategories.FirstOrDefault(v => v.Movies.Contains(movie))?.Movies.Remove(movie);
                    }
                },
                _ => SelectedItem != null);
        }

        private static string GenerateString(int length)
        {
            var random = new Random();
            return string.Join(string.Empty,
                Enumerable.Range(0, length)
                .Select(v => (char)random.Next('a', 'z' + 1)));
        }
    }
}
