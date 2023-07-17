namespace MaterialDesign3Demo.Domain
{
    public class SampleDialogViewModel : ViewModelBase
    {
        private string? _name;

        public SampleDialogViewModel() : base(nameof(SampleDialogViewModel))
        {
            
        }

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
