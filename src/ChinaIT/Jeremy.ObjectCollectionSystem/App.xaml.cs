using System.Windows;
using HandyControl.Tools;

namespace Jeremy.ObjectCollectionSystem;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // 创建互斥对象
    private static readonly Mutex mutex = new(true, "Jeremy.ObjectCollectionSystem");

    /// <summary>
    /// 重写 Startup 方法，添加全局异常处理
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e)
    {
        ConfigHelper.Instance.SetLang("zh-cn");
        if (mutex.WaitOne(TimeSpan.Zero, true))
        {
            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        else
        {
            HandyControl.Controls.MessageBox.Show("Jeremy.ObjectCollectionSystem 已启动，请勿重复启动！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            Environment.Exit(0);
        }
    }

    /// <summary>
    /// 重写 OnExit 事件
    /// </summary>
    /// <param name="e"></param>
    protected override void OnExit(ExitEventArgs e)
    {
        mutex.ReleaseMutex();
        base.OnExit(e);
    }

    /// <summary>
    /// 全局处理异常，保证程序稳健运行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        mutex.ReleaseMutex();
        //throw new NotImplementedException();
        // Set as resolved
        e.Handled = true;
        // Show exception info
        Growl.ErrorGlobal($"操作异常 -[{e.Exception.Message}]");
    }
}
