using System.Windows.Controls;
using Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

namespace Jeremy.ObjectCollectionSystem.Views.UserControls;

/// <summary>
/// Interaction logic for JobConfigUserControl.xaml
/// </summary>
public partial class JobConfigUserControl : UserControl
{
    public JobConfigUserControl()
    {
        InitializeComponent();
        DataContext = new JobConfigViewModel();
    }
}
