using HandyControl.Data;
using Jeremy.ObjectCollectionSystem.Domains;
using Jeremy.ObjectCollectionSystem.Models;
using Jeremy.ObjectCollectionSystem.Services;

namespace Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

public class BasicConfigViewModel : ObservableRecipient
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BasicConfigViewModel()
    {
        InitAsync();
    }

    #region 声明通知属性
    private TbBasicConfig _TbBasicConfig;
    /// <summary>
    /// 基础信息配置对象
    /// </summary>
    public TbBasicConfig TbBasicConfig
    {
        get => _TbBasicConfig;
        set => SetProperty(ref _TbBasicConfig, value);
    }
    #endregion

    #region 事件绑定声明与实现
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
        Growl.Success(new GrowlInfo()
        {
            Message = "取消成功！",
            WaitTime = 1
        });
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
            if (string.IsNullOrWhiteSpace(TbBasicConfig.Ip))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"IP 地址不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbBasicConfig.Mac))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"Mac 地址不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbBasicConfig.DeviceNumber))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"设备编码不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbBasicConfig.DeviceName))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"设备名称不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }

            TbBasicConfig.UpdateBy = "Administrator";
            TbBasicConfig.UpdateTime = DateTime.Now;
            // 保存数据库信息
            _ = BasicConfigService.Put(TbBasicConfig);
            GlobalInfo.DeviceNumber = TbBasicConfig.DeviceNumber;
            GlobalInfo.DeviceName = TbBasicConfig.DeviceName;
            GlobalInfo.Ip = TbBasicConfig.Ip;
            GlobalInfo.Mac = TbBasicConfig.Mac;
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

    #endregion

    #region 私有函数
    /// <summary>
    /// 初始化函数
    /// </summary>
    /// <returns></returns>
    private async Task InitAsync()
    {
        TbBasicConfig = new();
        try
        {
            TbBasicConfig = await BasicConfigService.GetAsync();
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"获取基础配置信息失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }
    }
    #endregion

}
