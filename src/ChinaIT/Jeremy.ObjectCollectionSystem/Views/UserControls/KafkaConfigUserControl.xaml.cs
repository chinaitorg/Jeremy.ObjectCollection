using System.Windows.Controls;
using Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

namespace Jeremy.ObjectCollectionSystem.Views.UserControls;

/// <summary>
/// Interaction logic for KafkaConfigUserControl.xaml
/// </summary>
public partial class KafkaConfigUserControl : UserControl
{
    public KafkaConfigUserControl()
    {
        InitializeComponent();
        DataContext = new KafkaConfigViewModel();
    }
}
