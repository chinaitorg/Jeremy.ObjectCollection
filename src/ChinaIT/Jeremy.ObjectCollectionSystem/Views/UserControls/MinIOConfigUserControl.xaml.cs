using System.Windows.Controls;
using Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

namespace Jeremy.ObjectCollectionSystem.Views.UserControls;

/// <summary>
/// Interaction logic for MinIOConfigUserControl.xaml
/// </summary>
public partial class MinIOConfigUserControl : UserControl
{
    public MinIOConfigUserControl()
    {
        InitializeComponent();
        DataContext = new MinIOConfigViewModel();
    }
}
