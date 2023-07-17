using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesign3Demo.Domain
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(nameof(MainWindowViewModel))
        {
            DemoItems = new ObservableCollection<DemoItem>
            {
              new DemoItem(
                    "Home",
                    "我的",
                    typeof(Home),
                    new[]
                    {
                        new DocumentationLink(
                            DocumentationLinkType.Wiki,
                            $"{ConfigurationManager.AppSettings["GitHub"]}/wiki",
                            "WIKI"),
                        DocumentationLink.DemoPageLink<Home>()
                    },
                    selectedIcon: PackIconKind.Home,
                    unselectedIcon: PackIconKind.HomeOutline)
            };

            foreach (var item in GenerateDemoItems(snackbarMessageQueue).OrderBy(i => i.Name))
            {
                DemoItems.Add(item);
            }

            MainDemoItems = new ObservableCollection<DemoItem>
            {
                DemoItems.First(x => x.Name == "Home"),
                DemoItems.First(x => x.Name == "Buttons"),
                DemoItems.First(x => x.Name == "Toggles"),
                DemoItems.First(x => x.Name == "Fields"),
                DemoItems.First(x => x.Name == "Pickers")
            };

            _demoItemsView = CollectionViewSource.GetDefaultView(DemoItems);
            _demoItemsView.Filter = DemoItemsFilter;

            HomeCommand = new AnotherCommandImplementation(
                _ =>
                {
                    SearchKeyword = string.Empty;
                    SelectedIndex = 0;
                });

            MovePrevCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (!string.IsNullOrWhiteSpace(SearchKeyword))
                        SearchKeyword = string.Empty;

                    SelectedIndex--;
                },
                _ => SelectedIndex > 0);

            MoveNextCommand = new AnotherCommandImplementation(
               _ =>
               {
                   if (!string.IsNullOrWhiteSpace(SearchKeyword))
                       SearchKeyword = string.Empty;

                   SelectedIndex++;
               },
               _ => SelectedIndex < DemoItems.Count - 1);

            DismissAllNotificationsCommand = new AnotherCommandImplementation(
                _ => DemoItems[0].DismissAllNotifications(),
                _ => DemoItems[0].Notifications != null);

            AddNewNotificationCommand = new AnotherCommandImplementation(
                _ => DemoItems[0].AddNewNotification());

            AddNewNotificationCommand.Execute(new object());
        }

        private readonly ICollectionView _demoItemsView;
        private DemoItem? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;
        private bool _controlsEnabled = true;

        public string? SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (SetProperty(ref _searchKeyword, value))
                {
                    _demoItemsView.Refresh();
                }
            }
        }

        public ObservableCollection<DemoItem> DemoItems { get; }
        public ObservableCollection<DemoItem> MainDemoItems { get; }

        public DemoItem? SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        public bool ControlsEnabled
        {
            get => _controlsEnabled;
            set => SetProperty(ref _controlsEnabled, value);
        }

        public AnotherCommandImplementation HomeCommand { get; }
        public AnotherCommandImplementation MovePrevCommand { get; }
        public AnotherCommandImplementation MoveNextCommand { get; }
        public AnotherCommandImplementation DismissAllNotificationsCommand { get; }
        public AnotherCommandImplementation AddNewNotificationCommand { get; }

        private static IEnumerable<DemoItem> GenerateDemoItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue is null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));

            yield return new DemoItem(
                "Palette",
                "调色板",
                typeof(PaletteSelector),
                new[]
                {
                    DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                    DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                    DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                    DocumentationLink.DemoPageLink<PaletteSelector>("Demo View"),
                    DocumentationLink.DemoPageLink<PaletteSelectorViewModel>("Demo View Model"),
                    DocumentationLink.ApiLink<PaletteHelper>()
                },
                selectedIcon: PackIconKind.Palette,
                unselectedIcon: PackIconKind.PaletteOutline);

            yield return new DemoItem(
                "Color Tool",
                "颜色工具",
                typeof(ColorTool),
                new[]
                {
                    DocumentationLink.WikiLink("Brush-Names", "画刷"),
                    DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                    DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                    DocumentationLink.DemoPageLink<ColorTool>("Demo View"),
                    DocumentationLink.DemoPageLink<ColorToolViewModel>("Demo View Model"),
                    DocumentationLink.ApiLink<PaletteHelper>()
                },
                selectedIcon: PackIconKind.Eyedropper,
                unselectedIcon: PackIconKind.EyedropperVariant);

            yield return new DemoItem(
                "Buttons",
                "智能",
                typeof(Buttons),
                new[]
                {
                    DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                    DocumentationLink.DemoPageLink<Buttons>("Demo View"),
                    DocumentationLink.DemoPageLink<ButtonsViewModel>("Demo View Model"),
                    DocumentationLink.StyleLink("Button"),
                    DocumentationLink.StyleLink("PopupBox"),
                    DocumentationLink.ApiLink<PopupBox>()
                },
                selectedIcon: PackIconKind.GestureTapHold,
                unselectedIcon: PackIconKind.GestureTapHold);

            yield return new DemoItem(
                "Toggles",
                "开关",
                typeof(Toggles),
                new[]
                {
                    DocumentationLink.DemoPageLink<Toggles>(),
                    DocumentationLink.StyleLink("ToggleButton", true),
                    DocumentationLink.StyleLink("CheckBox")
                },
                selectedIcon: PackIconKind.ToggleSwitch,
                unselectedIcon: PackIconKind.ToggleSwitchOffOutline);

            yield return new DemoItem(
                "Rating Bar",
                "评分",
                typeof(RatingBar),
                new[]
                {
                    DocumentationLink.DemoPageLink<RatingBar>(),
                    DocumentationLink.StyleLink("RatingBar"),
                    DocumentationLink.ApiLink<RatingBar>()
                },
                selectedIcon: PackIconKind.Star,
                unselectedIcon: PackIconKind.StarOutline);

            yield return new DemoItem(
                "Fields",
                "信息录入",
                typeof(Fields),
                new[]
                {
                    DocumentationLink.DemoPageLink<Fields>(),
                    DocumentationLink.StyleLink("TextBox")
                },
                selectedIcon: PackIconKind.Pencil,
                unselectedIcon: PackIconKind.PencilOutline);

            yield return new DemoItem(
                "Fields line up",
                "鱼香ROS",
                typeof(FieldsLineUp),
                new[]
                {
                    DocumentationLink.DemoPageLink<FieldsLineUp>()
                },
                selectedIcon: PackIconKind.PencilBox,
                unselectedIcon: PackIconKind.PencilBoxOutline);

            yield return new DemoItem(
                "ComboBoxes",
                "魔法组合",
                typeof(ComboBoxes),
                new[]
                {
                    DocumentationLink.DemoPageLink<ComboBoxes>(),
                    DocumentationLink.StyleLink("ComboBox")
                },
                selectedIcon: PackIconKind.CheckboxMarked,
                unselectedIcon: PackIconKind.CheckboxMarkedOutline);

            yield return new DemoItem(
                "Pickers",
                "休闲游戏",
                typeof(Pickers),
                new[]
                {
                    DocumentationLink.DemoPageLink<Pickers>(),
                    DocumentationLink.StyleLink("Clock"),
                    DocumentationLink.StyleLink("DatePicker"),
                    DocumentationLink.ApiLink<TimePicker>()
                },
                selectedIcon: PackIconKind.Clock,
                unselectedIcon: PackIconKind.ClockOutline);

            yield return new DemoItem(
                "Sliders",
                "音乐",
                typeof(Sliders),
                new[]
                {
                    DocumentationLink.DemoPageLink<Sliders>(),
                    DocumentationLink.StyleLink("Slider")
                },
                selectedIcon: PackIconKind.TuneVariant,
                unselectedIcon: PackIconKind.TuneVariant);

            yield return new DemoItem(
                "Chips",
                "参数设置",
                typeof(Chips),
                new[]
                {
                    DocumentationLink.DemoPageLink<Chips>(),
                    DocumentationLink.StyleLink("Chip"),
                    DocumentationLink.ApiLink<Chip>()
                },
                selectedIcon: PackIconKind.None,
                unselectedIcon: PackIconKind.None);

            yield return new DemoItem(
                "Typography",
                "洛神赋",
                typeof(Typography),
                new[]
                {
                    DocumentationLink.DemoPageLink<Typography>(),
                    DocumentationLink.StyleLink("TextBlock", true)
                },
                selectedIcon: PackIconKind.FormatSize,
                unselectedIcon: PackIconKind.FormatTitle)
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Cards",
                "美图中心",
                typeof(Cards),
                new[]
                {
                    DocumentationLink.DemoPageLink<Cards>(),
                    DocumentationLink.StyleLink("Card"),
                    DocumentationLink.ApiLink<Card>()
                },
                selectedIcon: PackIconKind.Card,
                unselectedIcon: PackIconKind.CardOutline);

            yield return new DemoItem(
                "Icon Pack",
                "图标集合",
                typeof(IconPack),
                new[]
                {
                    DocumentationLink.DemoPageLink<IconPack>("Demo View"),
                    DocumentationLink.DemoPageLink<IconPackViewModel>("Demo View Model"),
                    DocumentationLink.ApiLink<PackIcon>()
                },
                selectedIcon: PackIconKind.Robot,
                unselectedIcon: PackIconKind.RobotOutline,
                new IconPackViewModel(snackbarMessageQueue))
            {
                VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
            };

            yield return new DemoItem(
                "Colour Zones",
                "色欲无边",
                typeof(ColorZones),
                new[]
                {
                    DocumentationLink.DemoPageLink<ColorZones>(),
                    DocumentationLink.ApiLink<ColorZone>()
                },
                selectedIcon: PackIconKind.Subtitles,
                unselectedIcon: PackIconKind.SubtitlesOutline);

            yield return new DemoItem(
                "Lists",
                "联系人",
                typeof(Lists),
                new[]
                {
                    DocumentationLink.DemoPageLink<Lists>("Demo View"),
                    DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.StyleLink("ListBox"),
                    DocumentationLink.StyleLink("ListView")
                },
                selectedIcon: PackIconKind.FormatListBulletedSquare,
                unselectedIcon: PackIconKind.FormatListCheckbox);

            yield return new DemoItem(
                "Trees",
                "文档中心",
                typeof(Trees),
                new[]
                {
                    DocumentationLink.DemoPageLink<Trees>("Demo View"),
                    DocumentationLink.DemoPageLink<TreesViewModel>("Demo View Model"),
                    DocumentationLink.StyleLink("TreeView")
                },
                selectedIcon: PackIconKind.FileTree,
                unselectedIcon: PackIconKind.FileTreeOutline);

            yield return new DemoItem(
                "Data Grids",
                "档期安排",
                typeof(DataGrids),
                new[]
                {
                    DocumentationLink.DemoPageLink<DataGrids>("Demo View"),
                    DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.StyleLink("DataGrid")
                },
                selectedIcon: PackIconKind.ViewGrid,
                unselectedIcon: PackIconKind.ViewGridOutline);

            yield return new DemoItem(
                "Expander",
                "小魔仙",
                typeof(Expander),
                new[]
                {
                    DocumentationLink.DemoPageLink<Expander>(),
                    DocumentationLink.StyleLink("Expander")
                },
                selectedIcon: PackIconKind.UnfoldMoreHorizontal,
                unselectedIcon: PackIconKind.UnfoldMoreHorizontal);

            yield return new DemoItem(
                "Group Boxes",
                "卡哇伊",
                typeof(GroupBoxes),
                new[]
                {
                    DocumentationLink.DemoPageLink<GroupBoxes>(),
                    DocumentationLink.StyleLink("GroupBox")
                },
                selectedIcon: PackIconKind.TextBoxMultiple,
                unselectedIcon: PackIconKind.TextBoxMultipleOutline);

            yield return new DemoItem(
                "Menus & Tool Bars",
                "开发中心",
                typeof(MenusAndToolBars),
                new[]
                {
                    DocumentationLink.DemoPageLink<MenusAndToolBars>(),
                    DocumentationLink.StyleLink("Menu"),
                    DocumentationLink.StyleLink("ToolBar")
                },
                selectedIcon: PackIconKind.DotsHorizontalCircle,
                unselectedIcon: PackIconKind.DotsHorizontalCircleOutline);

            yield return new DemoItem(
                "Progress Indicators",
                "进度",
                typeof(Progress),
                new[]
                {
                    DocumentationLink.DemoPageLink<Progress>(),
                    DocumentationLink.StyleLink("ProgressBar")
                },
                selectedIcon: PackIconKind.ProgressClock,
                unselectedIcon: PackIconKind.ProgressClock);

            yield return new DemoItem(
                "Navigation Rail",
                "魔法实验室",
                typeof(NavigationRail),
                new[]
                {
                    DocumentationLink.DemoPageLink<NavigationRail>(),
                    DocumentationLink.StyleLink("NavigationRail", true),
                },
                selectedIcon: PackIconKind.NavigationVariant,
                unselectedIcon: PackIconKind.NavigationVariantOutline)
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Navigation Bar",
                "星际迷航",
                typeof(NavigationBar),
                new[]
                {
                    DocumentationLink.DemoPageLink<NavigationBar>(),
                    DocumentationLink.StyleLink("NavigationBar", true),
                },
                selectedIcon: PackIconKind.NavigationVariant,
                unselectedIcon: PackIconKind.NavigationVariantOutline)
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Dialogs",
                "对话框",
                typeof(Dialogs),
                new[]
                {
                    DocumentationLink.WikiLink("Dialogs", "Dialogs"),
                    DocumentationLink.DemoPageLink<Dialogs>("Demo View"),
                    DocumentationLink.DemoPageLink<DialogsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.ApiLink<DialogHost>()
                },
                selectedIcon: PackIconKind.CommentAlert,
                unselectedIcon: PackIconKind.CommentAlertOutline)
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Drawer",
                "机器人",
                typeof(Drawers),
                new[]
                {
                    DocumentationLink.DemoPageLink<Drawers>("Demo View"),
                    DocumentationLink.ApiLink<DrawerHost>()
                },
                selectedIcon: PackIconKind.ExpandAll,
                unselectedIcon: PackIconKind.ExpandAll);

            yield return new DemoItem(
                "Snackbar",
                "实时沟通",
                typeof(Snackbars),
                new[]
                {
                    DocumentationLink.WikiLink("Snackbar", "Snackbar"),
                    DocumentationLink.DemoPageLink<Snackbars>(),
                    DocumentationLink.StyleLink("Snackbar"),
                    DocumentationLink.ApiLink<Snackbar>(),
                    DocumentationLink.ApiLink<ISnackbarMessageQueue>()
                },
                selectedIcon: PackIconKind.InformationCircle,
                unselectedIcon: PackIconKind.InformationCircleOutline)
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Transitions",
                "丝丝顺滑",
                typeof(Transitions),
                new[]
                {
                    DocumentationLink.WikiLink("Transitions", "Transitions"),
                    DocumentationLink.DemoPageLink<Transitions>(),
                    DocumentationLink.ApiLink<Transitioner>("Transitions"),
                    DocumentationLink.ApiLink<TransitionerSlide>("Transitions"),
                    DocumentationLink.ApiLink<TransitioningContent>("Transitions"),
                },
                selectedIcon: PackIconKind.TransitionMasked,
                unselectedIcon: PackIconKind.Transition);

            yield return new DemoItem(
                "Elevation",
                "Elevation",
                typeof(Elevation),
                new[]
                {
                    DocumentationLink.DemoPageLink<Elevation>(),
                    DocumentationLink.StyleLink("Shadows"),
                    DocumentationLink.SpecsLink("https://material.io/design/environment/elevation.html", "Elevation")
                },
                selectedIcon: PackIconKind.BoxShadow,
                unselectedIcon: PackIconKind.BoxShadow);
        }

        private bool DemoItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchKeyword))
            {
                return true;
            }
            return obj is DemoItem item && item.Name.ToLower().Contains(_searchKeyword!.ToLower());
        }
    }
}
