using System.IO;
using HandyControl.Data;
using Jeremy.ObjectCollectionSystem.Domains;
using Jeremy.ObjectCollectionSystem.Models;
using Jeremy.ObjectCollectionSystem.Services;

namespace Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

public class JobConfigViewModel : ObservableRecipient
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public JobConfigViewModel()
    {
        InitAsync();
    }

    #region 声明通知属性
    private TbJobConfig _TbJobConfig;
    /// <summary>
    /// Job 信息配置对象
    /// </summary>
    public TbJobConfig TbJobConfig
    {
        get => _TbJobConfig;
        set => SetProperty(ref _TbJobConfig, value);
    }

    private string _FullPath;
    /// <summary>
    /// 文件路径
    /// </summary>
    public string FullPath
    {
        get => _FullPath;
        set => SetProperty(ref _FullPath, value);
    }

    private List<string> _ScanTypeList;
    /// <summary>
    /// 线程扫描类型集合
    /// </summary>
    public List<string> ScanTypeList
    {
        get => _ScanTypeList;
        set => SetProperty(ref _ScanTypeList, value);
    }

    private int _ScanTypeSelectedIndex;
    /// <summary>
    /// 线程扫描类型选择索引
    /// </summary>
    public int ScanTypeSelectedIndex
    {
        get => _ScanTypeSelectedIndex;
        set => SetProperty(ref _ScanTypeSelectedIndex, value);
    }

    private List<string> _IsDeleteList;
    /// <summary>
    /// 是否删除源文件集合
    /// </summary>
    public List<string> IsDeleteList
    {
        get => _IsDeleteList;
        set => SetProperty(ref _IsDeleteList, value);
    }

    private int _IsDeleteSelectedIndex;
    /// <summary>
    /// 是否删除源文件选择索引
    /// </summary>
    public int IsDeleteSelectedIndex
    {
        get => _IsDeleteSelectedIndex;
        set => SetProperty(ref _IsDeleteSelectedIndex, value);
    }
    #endregion

    #region 事件声明与实现
    /// <summary>
    /// 取消事件声明
    /// </summary>
    public RelayCommand<EventArgs> CancelCommand => new(Cancel);
    /// <summary>
    /// 取消事件实现
    /// </summary>
    /// <param name="args"></param>
    private void Cancel(EventArgs args)
    {
        InitAsync();
        Growl.Success("取消成功！");
    }

    /// <summary>
    /// 保存事件声明
    /// </summary>
    public RelayCommand<EventArgs> SaveCommand => new(Save);
    /// <summary>
    /// 保存事件实现
    /// </summary>
    /// <param name="args"></param>
    private void Save(EventArgs args)
    {
        try
        {
            // 数据校验
            if (string.IsNullOrWhiteSpace(TbJobConfig.Name))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"Job 名称不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (0 >= TbJobConfig.ThreadCount)
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"线程数量不能为小于1，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbJobConfig.ScanInterval))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"周期参数值不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbJobConfig.ScanPath))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"监控路径不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (IsDeleteSelectedIndex.Equals(0))
            {
                TbJobConfig.IsDelete = true;
            }
            else
            {
                TbJobConfig.IsDelete = false;
            }
            TbJobConfig.ScanIntervalType = ScanTypeSelectedIndex;
            TbJobConfig.UpdateBy = "Administrator";
            TbJobConfig.UpdateTime = DateTime.Now;
            // 保存数据库信息
            _ = JobConfigService.Put(TbJobConfig);
            GlobalInfo.ScanPath = TbJobConfig.ScanPath;
            GlobalInfo.ScanInterval = TbJobConfig.ScanInterval;
            GlobalInfo.JobId = TbJobConfig.Id;
            GlobalInfo.JobName = TbJobConfig.Name;
            GlobalInfo.IsDeleteFile = TbJobConfig.IsDelete;
            if (!Directory.Exists($"{Path.GetPathRoot(GlobalInfo.ScanPath)}/done"))
            {
                Directory.CreateDirectory($"{Path.GetPathRoot(GlobalInfo.ScanPath)}/done");
            }
            // 保存 
            Growl.Success(new GrowlInfo()
            {
                Message = "保存成功！",
                WaitTime = 1
            });
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"保存失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }
    }

    /// <summary>
    /// 选择监控路径事件声明
    /// </summary>
    public RelayCommand<EventArgs> ChoosePathCommand => new(ChoosePath);
    /// <summary>
    /// 选择监控路径事件实现
    /// </summary>
    /// <param name="args"></param>
    private void ChoosePath(EventArgs args)
    {
        // 选择文件路径
        var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
        if (dialog.ShowDialog().GetValueOrDefault())
        {
            TbJobConfig.ScanPath = dialog.SelectedPath;
            FullPath = dialog.SelectedPath;
        }
        else
        {
            TbJobConfig.ScanPath = "";
            FullPath = "";
        }
    }
    #endregion

    #region 私有函数
    /// <summary>
    /// 初始化函数
    /// </summary>
    private async Task InitAsync()
    {
        ScanTypeList = new()
        {
            "时间间隔",
            "Cron 表达式"
        };
        IsDeleteList = new()
        {
            "是",
            "否"
        };
        TbJobConfig = new();
        try
        {
            TbJobConfig = await JobConfigService.GetAsync();
            FullPath = TbJobConfig.ScanPath;
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"获取 Job 配置信息失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }
        if (TbJobConfig.ScanIntervalType.Equals(0))
        {
            ScanTypeSelectedIndex = 0;
        }
        else
        {
            ScanTypeSelectedIndex = 1;
        }
        if (TbJobConfig.IsDelete)
        {
            IsDeleteSelectedIndex = 0;
        }
        else
        {
            IsDeleteSelectedIndex = 1;
        }

    }
    #endregion

}
