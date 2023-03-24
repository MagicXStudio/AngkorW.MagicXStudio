using System.Collections.ObjectModel;

namespace MaterialDesign3Demo.Domain
{

    /// <summary>
    /// https://devblogs.microsoft.com/dotnet/how-async-await-really-works/
    /// </summary>
    public class ListsAndGridsViewModel : ViewModelBase
    {
        public ListsAndGridsViewModel()
        {
            Items1 = CreateData();
            Items2 = CreateData();
            Items3 = CreateData();

            foreach (var model in Items1)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(SelectableViewModel.IsSelected))
                        OnPropertyChanged(nameof(IsAllItems1Selected));
                };
            }
        }

        public bool? IsAllItems1Selected
        {
            get
            {
                var selected = Items1.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items1);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, IEnumerable<SelectableViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private static ObservableCollection<SelectableViewModel> CreateData()
        {
            return new ObservableCollection<SelectableViewModel>
            {
                new SelectableViewModel
                {
                    Code = 'M',
                    Name = "Iterators",
                    Description = "SynchronizationContext"
                },
                new SelectableViewModel
                {
                    Code = 'D',
                    Name = "Concurrency",
                    Description = "ExecutionContext",
                    Food = "Fries"
                },
                new SelectableViewModel
                {
                    Code = 'P',
                    Name = "GC",
                    Description = "ThreadPool"
                }
            };
        }

        public ObservableCollection<SelectableViewModel> Items1 { get; }
        public ObservableCollection<SelectableViewModel> Items2 { get; }
        public ObservableCollection<SelectableViewModel> Items3 { get; }

        public IEnumerable<string> Foods => new[] { "ManualResetEventSlim", "CancellationTokenSource", "Interlocked", "AsyncLocal" };
    }
}
