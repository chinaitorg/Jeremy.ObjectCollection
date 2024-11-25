using HandyControl.Data;
using Jeremy.ObjectCollectionSystem.Domains;
using Jeremy.ObjectCollectionSystem.Models;
using Jeremy.ObjectCollectionSystem.Services;

namespace Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

public class MinIOConfigViewModel : ObservableRecipient
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public MinIOConfigViewModel()
    {
        InitAsync();
    }

    #region 声明通知属性

    private TbMinioConfig _TbMinioConfig;
    /// <summary>
    /// MinIO信息配置对象
    /// </summary>
    public TbMinioConfig TbMinioConfig
    {
        get => _TbMinioConfig;
        set => SetProperty(ref _TbMinioConfig, value);
    }

    #endregion

    #region 事件绑定并实现
    /// <summary>
    /// 连接测试事件声明
    /// </summary>
    public RelayCommand<EventArgs> TryConnectCommand => new(TryConnect);
    /// <summary>
    /// 连接测试事件实现
    /// </summary>
    /// <param name="args"></param>
    private void TryConnect(EventArgs args)
    {
        try
        {
            // 数据校验
            if (string.IsNullOrWhiteSpace(TbMinioConfig.EndPoint))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"终结点不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbMinioConfig.AccessKey))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"用户名不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbMinioConfig.SecretKey))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"密码不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            var client = MinioService.Create(TbMinioConfig.EndPoint, TbMinioConfig.AccessKey, TbMinioConfig.SecretKey);
            MinioService.BucketExists(client, TbMinioConfig.BucketName);
            Growl.Success(new GrowlInfo()
            {
                Message = "连接成功！",
                WaitTime = 1
            });
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"连接失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }




    }
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
            if (string.IsNullOrWhiteSpace(TbMinioConfig.EndPoint))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"终结点不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbMinioConfig.AccessKey))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"用户名不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbMinioConfig.SecretKey))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"密码不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbMinioConfig.BucketName))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"MinIO 桶名称不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(TbMinioConfig.Path))
            {
                Growl.Warning(new GrowlInfo()
                {
                    Message = $"MinIO 路径不能为空，请确认！",
                    WaitTime = 1
                });
                return;
            }

            TbMinioConfig.UpdateBy = "Administrator";
            TbMinioConfig.UpdateTime = DateTime.Now;
            // 保存数据库信息
            _ = MinIOConfigService.Put(TbMinioConfig);
            GlobalInfo.EndPoint = TbMinioConfig.EndPoint;
            GlobalInfo.AccessKey = TbMinioConfig.AccessKey;
            GlobalInfo.SecretKey = TbMinioConfig.SecretKey;
            GlobalInfo.BucketName = TbMinioConfig.BucketName;
            GlobalInfo.MinIOPath = TbMinioConfig.Path;
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
        TbMinioConfig = new();
        try
        {
            TbMinioConfig = await MinIOConfigService.GetAsync();
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"获取 MinIO 配置信息失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }
    }
    #endregion

}

