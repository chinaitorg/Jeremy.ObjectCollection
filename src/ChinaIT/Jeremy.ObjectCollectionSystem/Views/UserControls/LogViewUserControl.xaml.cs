using System.Windows.Controls;
using Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

namespace Jeremy.ObjectCollectionSystem.Views.UserControls;

/// <summary>
/// Interaction logic for LogViewUserControl.xaml
/// </summary>
public partial class LogViewUserControl : UserControl
{
    public LogViewUserControl()
    {
        InitializeComponent();
        DataContext = new LogViewViewModel();
    }
}
