using System.Windows.Controls;
using Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

namespace Jeremy.ObjectCollectionSystem.Views.UserControls;

/// <summary>
/// Interaction logic for BasicConfigUserControl.xaml
/// </summary>
public partial class BasicConfigUserControl : UserControl
{
    public BasicConfigUserControl()
    {
        InitializeComponent();
        DataContext = new BasicConfigViewModel();
    }
}
