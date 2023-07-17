using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain;

public class SampleItem : ViewModelBase
{
    public string? Title { get; set; }
    public PackIconKind SelectedIcon { get; set; }

    public SampleItem() : base(nameof(SampleItem))
    {
        
    }
    public PackIconKind UnselectedIcon { get; set; }
    private object? _notification = null;

    public object? Notification
    {
        get { return _notification; }
        set { SetProperty(ref _notification, value); }
    }

}
