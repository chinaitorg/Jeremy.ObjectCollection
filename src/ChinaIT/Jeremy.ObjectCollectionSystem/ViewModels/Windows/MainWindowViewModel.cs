using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using Jeremy.ObjectCollectionSystem.Domains;
using Jeremy.ObjectCollectionSystem.Services;
using Jeremy.ObjectCollectionSystem.Models;
using Jeremy.ObjectCollectionSystem.Views.UserControls;

namespace Jeremy.ObjectCollectionSystem.ViewModels.Windows;

public class MainWindowViewModel : ObservableRecipient
{
    public MainWindowViewModel()
    {
        InitAsync();
    }

    #region 私有函数
    /// <summary>
    /// 初始化
    /// </summary>
    private async Task InitAsync()
    {
        // Theme
        ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
        _ = Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    // 清除90天内的采集历史记录
                    await JobLogService.DeleteAsync(90);
                }
                catch
                {
                }
                await Task.Delay(1800000);
            }
        });
        // 时间
        _ = Task.Run(async () =>
        {
            while (true)
            {
                CurrentTime = DateTime.Now.ToString();
                await Task.Delay(1000);
            }
        });
        // 参数初始化
        MenuSelectedIndex = -1;

        // 初始化基础信息配置
        var data = await BasicConfigService.GetAsync();
        if (data is null)
        {
            TbBasicConfig tbBasic = new()
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                Ip = NetHelper.GetIp2(),
                Mac = NetHelper.Mac(),
                CreateBy = "Administrator",
                CreateTime = DateTime.Now,
                UpdateBy = "Administrator",
                UpdateTime = DateTime.Now,
                DeviceName = "N/A",
                DeviceNumber = "N/A",
                Comment = "系统自动添加"
            };
            _ = BasicConfigService.Post(tbBasic);
        }
        else
        {
            // 取消IP自动更新
            //data.Ip = NetHelper.GetIp2();
            data.Mac = NetHelper.Mac();
            data.UpdateBy = "Administrator";
            data.UpdateTime = DateTime.Now;
            _ = BasicConfigService.Put(data);
        }
        // 初始化所有参数
        _ = Task.Run(async () =>
        {
            _ = await InitService.Init();
        });

    }

    #endregion

    #region 通知属性
    private string _CurrentTime;
    /// <summary>
    /// 系统当前时间
    /// </summary>
    public string CurrentTime
    {
        get => _CurrentTime;
        set => SetProperty(ref _CurrentTime, value);
    }

    /// <summary>
    /// 页面集合
    /// </summary>
    public ObservableCollection<TabItem> TabSource { get; set; } = new();

    private int _MenuSelectedIndex;
    /// <summary>
    /// 界面菜单索引
    /// </summary>
    public int MenuSelectedIndex
    {
        get => _MenuSelectedIndex;
        set => SetProperty(ref _MenuSelectedIndex, value);
    }

    #endregion

    #region 事件绑定并实现
    /// <summary>
    /// 声明切换主题颜色事件
    /// </summary>
    public RelayCommand<EventArgs> ChangeThemeCommand => new(ChangeTheme);
    /// <summary>
    /// 实现切换主题颜色事件
    /// </summary>
    /// <param name="args"></param>
    private void ChangeTheme(EventArgs args)
    {
        if (GlobalInfo.ApplicationTheme.Equals(ApplicationTheme.Light))
        {
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            GlobalInfo.ApplicationTheme = ApplicationTheme.Dark;
        }
        else
        {
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            GlobalInfo.ApplicationTheme = ApplicationTheme.Light;
        }
    }

    /// <summary>
    /// 声明按下鼠标左键命令 - 基础配置
    /// </summary>
    public RelayCommand<EventArgs> BasicConfigMouseLeftButtonDownCommand => new(BasicConfigMouseLeftButtonDown);
    /// <summary>
    /// 实现声明按下鼠标左键命令
    /// </summary>
    /// <param name="p"></param>
    private void BasicConfigMouseLeftButtonDown(EventArgs p)
    {
        if (!MenuCollection.CheckMenu("基础配置"))
        {
            TabItem tabItem = new()
            {
                Header = "基础配置",
                Content = new BasicConfigUserControl()
            };
            tabItem.SetValue(IconElement.HeightProperty, 16.0);
            tabItem.SetValue(IconElement.WidthProperty, 16.0);
            tabItem.SetValue(IconElement.GeometryProperty, Application.Current.Resources["ConfigGeometry"] as Geometry);
            TabSource.Add(tabItem);
            MenuSelectedIndex = TabSource.IndexOf(tabItem);
        }
        else
        {
            MenuSelectedIndex = MenuCollection.ListMenuCollection.IndexOf("基础配置");
        }
    }

    /// <summary>
    /// 声明按下鼠标左键命令 - MinIO 配置
    /// </summary>
    public RelayCommand<EventArgs> MinIOConfigMouseLeftButtonDownCommand => new(MinIOConfigMouseLeftButtonDown);
    /// <summary>
    /// 实现声明按下鼠标左键命令
    /// </summary>
    /// <param name="p"></param>
    private void MinIOConfigMouseLeftButtonDown(EventArgs p)
    {
        if (!MenuCollection.CheckMenu("MinIO 配置"))
        {
            TabItem tabItem = new()
            {
                Header = "MinIO 配置",
                Content = new MinIOConfigUserControl()
            };
            tabItem.SetValue(IconElement.HeightProperty, 16.0);
            tabItem.SetValue(IconElement.WidthProperty, 16.0);
            tabItem.SetValue(IconElement.GeometryProperty, Application.Current.Resources["ConfigGeometry"] as Geometry);
            TabSource.Add(tabItem);
            MenuSelectedIndex = TabSource.IndexOf(tabItem);
        }
        else
        {
            MenuSelectedIndex = MenuCollection.ListMenuCollection.IndexOf("MinIO 配置");
        }
    }

    /// <summary>
    /// 声明按下鼠标左键命令 - Kafka 配置
    /// </summary>
    public RelayCommand<EventArgs> KafkaConfigMouseLeftButtonDownCommand => new(KafkaConfigMouseLeftButtonDown);
    /// <summary>
    /// 实现声明按下鼠标左键命令
    /// </summary>
    /// <param name="p"></param>
    private void KafkaConfigMouseLeftButtonDown(EventArgs p)
    {
        if (!MenuCollection.CheckMenu("Kafka 配置"))
        {
            TabItem tabItem = new()
            {
                Header = "Kafka 配置",
                Content = new KafkaConfigUserControl()
            };
            tabItem.SetValue(IconElement.HeightProperty, 16.0);
            tabItem.SetValue(IconElement.WidthProperty, 16.0);
            tabItem.SetValue(IconElement.GeometryProperty, Application.Current.Resources["ConfigGeometry"] as Geometry);
            TabSource.Add(tabItem);
            MenuSelectedIndex = TabSource.IndexOf(tabItem);
        }
        else
        {
            MenuSelectedIndex = MenuCollection.ListMenuCollection.IndexOf("Kafka 配置");
        }
    }

    /// <summary>
    /// 声明按下鼠标左键命令 - Job 配置
    /// </summary>
    public RelayCommand<EventArgs> JobConfigMouseLeftButtonDownCommand => new(JobConfigMouseLeftButtonDown);
    /// <summary>
    /// 实现声明按下鼠标左键命令
    /// </summary>
    /// <param name="p"></param>
    private void JobConfigMouseLeftButtonDown(EventArgs p)
    {
        if (!MenuCollection.CheckMenu("Job 配置"))
        {
            TabItem tabItem = new()
            {
                Header = "Job 配置",
                Content = new JobConfigUserControl()
            };
            tabItem.SetValue(IconElement.HeightProperty, 16.0);
            tabItem.SetValue(IconElement.WidthProperty, 16.0);
            tabItem.SetValue(IconElement.GeometryProperty, Application.Current.Resources["ConfigGeometry"] as Geometry);
            TabSource.Add(tabItem);
            MenuSelectedIndex = TabSource.IndexOf(tabItem);
        }
        else
        {
            MenuSelectedIndex = MenuCollection.ListMenuCollection.IndexOf("Job 配置");
        }
    }

    /// <summary>
    /// 声明按下鼠标左键命令 - 日志查看
    /// </summary>
    public RelayCommand<EventArgs> LogViewMouseLeftButtonDownCommand => new(LogViewMouseLeftButtonDown);
    /// <summary>
    /// 实现声明按下鼠标左键命令
    /// </summary>
    /// <param name="p"></param>
    private void LogViewMouseLeftButtonDown(EventArgs p)
    {
        if (!MenuCollection.CheckMenu("日志查看"))
        {
            TabItem tabItem = new()
            {
                Header = "日志查看",
                Content = new LogViewUserControl()
            };
            tabItem.SetValue(IconElement.HeightProperty, 16.0);
            tabItem.SetValue(IconElement.WidthProperty, 16.0);
            tabItem.SetValue(IconElement.GeometryProperty, Application.Current.Resources["PageModeGeometry"] as Geometry);
            TabSource.Add(tabItem);
            MenuSelectedIndex = TabSource.IndexOf(tabItem);
        }
        else
        {
            MenuSelectedIndex = MenuCollection.ListMenuCollection.IndexOf("日志查看");
        }
    }

    /// <summary>
    /// 声明界面关闭路由事件
    /// </summary>
    public RelayCommand<RoutedEventArgs> PageCloseCommand => new(PageClose);
    /// <summary>
    /// 实现界面关闭路由事件
    /// </summary>
    /// <param name="args"></param>
    private void PageClose(RoutedEventArgs args)
    {
        if (args is RoutedEventArgs tabControl)
        {
            MenuCollection.CloseMenu((tabControl.OriginalSource as TabItem)?.Header.ToString());
        }
    }


    #endregion

    #region 私有属性
    /// <summary>
    /// Cancel Task Flag
    /// </summary>
    CancellationTokenSource cancellationToken = new();

    #endregion

    #region 运行和停止事件操作
    public AsyncRelayCommand<EventArgs> RunAsyncCommand => new(RunAsync);

    private Task RunAsync(EventArgs? args)
    {
        //// 更新状态
        //_ = Task.Run(async () =>
        //{
        //    await JobConfigService.Put(true);
        //});
        //cancellationToken = new();
        //// MinIO 和 Kafka 操作
        //Task.Run(async () =>
        //{
        //    var minIO = MinioService.Create(GlobalInfo.EndPoint, GlobalInfo.AccessKey, GlobalInfo.SecretKey);
        //    var kafka = new ProducerBuilder<Null, string>(new ProducerConfig { BootstrapServers = GlobalInfo.BootstrapServers }).Build();
        //    string minioFileName;
        //    while (!cancellationToken.IsCancellationRequested)
        //    {
        //        try
        //        {
        //            var fileLists = FileHelper.GetFileNames(GlobalInfo.ScanPath);

        //            if (fileLists.Any())
        //            {
        //                if (GlobalInfo.IsIncrementalCollection.Equals("Y"))
        //                {
        //                    var doneFileLists = JobLogService.Get().ToArray();
        //                    fileLists = fileLists.Except(doneFileLists).ToArray();
        //                }

        //                foreach (var file in fileLists)
        //                {
        //                    var stream = FileHelper.FileToStream(file, out long length);
        //                    if (0 >= length)
        //                    {
        //                        continue;
        //                    }
        //                    // MinIO
        //                    minioFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{Path.GetExtension(file)}";
        //                    await MinioService.PutObjectAsync(minIO, GlobalInfo.BucketName, $"cs-client/{GlobalInfo.DeviceNumber}/{DateTime.Now:yyyyMMdd}/{minioFileName}", stream, length);
        //                    // Kafka
        //                    KafkaMessageDTO KafkaMessageDTO = new()
        //                    {
        //                        collectionTime = TimeHelper.ConvertToTimestamp(DateTime.Now),
        //                        pointCode = GlobalInfo.PointCode,
        //                        deviceCode = GlobalInfo.PlcCode,
        //                    };
        //                    KafkaMessageDTO.pointValue.subFolder = file;
        //                    KafkaMessageDTO.pointValue.collectionTime = KafkaMessageDTO.collectionTime;
        //                    KafkaMessageDTO.pointValue.folderCode = KafkaMessageDTO.deviceCode;
        //                    KafkaMessageDTO.pointValue.ip = GlobalInfo.Ip;
        //                    KafkaMessageDTO.pointValue.url = $"/{GlobalInfo.BucketName}/cs-client/{GlobalInfo.DeviceNumber}/{DateTime.Now:yyyyMMdd}/{minioFileName}";
        //                    KafkaMessageDTO.pointValue.folder = "N/A";
        //                    KafkaMessageDTO.pointValue.createTime = KafkaMessageDTO.collectionTime;
        //                    KafkaMessageDTO.pointValue.name = Path.GetFileNameWithoutExtension(file);
        //                    KafkaMessageDTO.pointValue.topic = GlobalInfo.Topic;
        //                    KafkaMessageDTO.pointValue.imageType = Path.GetExtension(file).ToLower().Replace(".", "");
        //                    _ = await kafka.ProduceAsync(GlobalInfo.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(KafkaMessageDTO) });
        //                    // Log
        //                    TbJobLog log = new()
        //                    {
        //                        Id = Guid.NewGuid().ToString().Replace("-", ""),
        //                        JobId = GlobalInfo.JobId,
        //                        DeviceNumber = GlobalInfo.DeviceNumber,
        //                        CreateTime = DateTime.Now,
        //                        CreateBy = "Administrator",
        //                        OriginFilefullPath = file,
        //                        FileName = Path.GetFileName(file),
        //                        FileType = Path.GetExtension(file),
        //                        IsMinIo = true,
        //                        IsMinIotime = DateTime.Now,
        //                        MinIobucketName = GlobalInfo.BucketName,
        //                        MinIopath = GlobalInfo.MinIOPath,
        //                        MinIofileName = minioFileName,
        //                        MinIourl = KafkaMessageDTO.pointValue.url,
        //                        IsKafka = true,
        //                        IsKafkaTime = DateTime.Now,
        //                        KafkaTopic = GlobalInfo.Topic,
        //                        KafkaBody = JsonConvert.SerializeObject(KafkaMessageDTO),
        //                        Comment = "系统自动生成"
        //                    };
        //                    _ = await JobLogService.PutAsync(log);
        //                    if (!GlobalInfo.IsIncrementalCollection.Equals("Y"))
        //                    {
        //                        // Delete origin file
        //                        if (GlobalInfo.IsDeleteFile)
        //                        {
        //                            FileHelper.DeleteFile(file);
        //                        }
        //                        else
        //                        {
        //                            var fi = new FileInfo(file);
        //                            fi.MoveTo($"{Path.GetPathRoot(GlobalInfo.ScanPath)}/done/{Path.GetFileName(file)}", true);
        //                        }
        //                    }
        //                    await Task.Delay(int.Parse(GlobalInfo.ScanInterval));
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Growl.Warning($"异常请及时处理 - {ex.Message}");
        //        }

        //        // 推送数据
        //        await Task.Delay(int.Parse(GlobalInfo.ScanInterval));

        //    }
        //}, cancellationToken.Token);

        Growl.Success("计划任务已启动！");
        return Task.CompletedTask;
    }

    public AsyncRelayCommand<EventArgs> StopAsyncCommand => new(StopAsync);

    private Task StopAsync(EventArgs? args)
    {
        // 更新状态
        _ = Task.Run(async () =>
        {
            await JobConfigService.Put(false);
        });
        cancellationToken.Cancel();
        Growl.Success("计划任务已停止！");
        return Task.CompletedTask;
    }

    #endregion
}
