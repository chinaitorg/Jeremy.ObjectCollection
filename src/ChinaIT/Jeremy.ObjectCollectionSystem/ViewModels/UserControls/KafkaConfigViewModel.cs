using HandyControl.Data;
using Jeremy.ObjectCollectionSystem.Domains;
using Jeremy.ObjectCollectionSystem.Models;
using Jeremy.ObjectCollectionSystem.Services;

namespace Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

public class KafkaConfigViewModel : ObservableRecipient
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KafkaConfigViewModel()
    {
        InitAsync();
    }

    #region 声明通知属性
    private TbKafkaConfig _TbKafkaConfig;
    /// <summary>
    /// Kafka 信息配置对象
    /// </summary>
    public TbKafkaConfig TbKafkaConfig
    {
        get => _TbKafkaConfig;
        set => SetProperty(ref _TbKafkaConfig, value);
    }

    #endregion

    #region 声明事件并实现
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
            if (string.IsNullOrWhiteSpace(TbKafkaConfig.BootstrapServers))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"集群地址不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbKafkaConfig.Topic))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"Topic 名称不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }

            TbKafkaConfig.UpdateBy = "Administrator";
            TbKafkaConfig.UpdateTime = DateTime.Now;
            // 保存数据库信息
            _ = KafkaConfigService.Put(TbKafkaConfig);
            GlobalInfo.BootstrapServers = TbKafkaConfig.BootstrapServers;
            GlobalInfo.Topic = TbKafkaConfig.Topic;
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
    private async Task InitAsync()
    {
        TbKafkaConfig = new();
        try
        {
            TbKafkaConfig = await KafkaConfigService.GetAsync();
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"获取 Kafka 配置信息失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }
    }
    #endregion

}
