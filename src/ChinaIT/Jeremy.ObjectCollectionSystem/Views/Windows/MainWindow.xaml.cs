namespace Jeremy.ObjectCollectionSystem;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Rewite menu
    /// </summary>
    /// <param name="e"></param>
    //protected override void OnContentRendered(EventArgs e) => NonClientAreaContent = new MenuTitleUserControl();

    /// <summary>
    /// Close application
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true; // 中断点击事件            
        Growl.Ask("Jeremy.ObjectCollectionSystem 将要退出，请确定是否继续！？", isConfirmed =>
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
