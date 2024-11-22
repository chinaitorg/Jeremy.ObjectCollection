using Jeremy.ObjectCollectionSystem.ViewModels.Windows;
using Jeremy.ObjectCollectionSystem.Views.UserControls;

namespace Jeremy.ObjectCollectionSystem;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = StaticMainWindowWiewModel.VM;
    }

    /// <summary>
    /// Rewite menu
    /// </summary>
    /// <param name="e"></param>
    protected override void OnContentRendered(EventArgs e) => NonClientAreaContent = new MenuTitleUserControl();

    /// <summary>
    /// Close application
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true; // 中断点击事件            
        Growl.Ask("对象采集程序将要退出，请确定是否继续！？", isConfirmed =>
        {
            if (isConfirmed)
            {
                //MainNotifyIcon.Dispose();
                Environment.Exit(0);
            }
            return true;
        });
    }
}
