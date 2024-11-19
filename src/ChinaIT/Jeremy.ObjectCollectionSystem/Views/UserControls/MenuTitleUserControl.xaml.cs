using Jeremy.ObjectCollectionSystem.ViewModels.Windows;

namespace Jeremy.ObjectCollectionSystem.Views.UserControls;

/// <summary>
/// MenuTitleUserControl.xaml 的交互逻辑
/// </summary>
public partial class MenuTitleUserControl
{
    public MenuTitleUserControl()
    {
        InitializeComponent();
        DataContext = StaticMainWindowWiewModel.VM;
    }
}
