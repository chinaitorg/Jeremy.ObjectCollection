using System.IO;
using HandyControl.Data;
using Jeremy.ObjectCollectionSystem.DataTransferObjects;
using Jeremy.ObjectCollectionSystem.Domains;
using Jeremy.ObjectCollectionSystem.Models;
using Jeremy.ObjectCollectionSystem.Services;
using Minio;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Jeremy.ObjectCollectionSystem.ViewModels.UserControls;

public class LogViewViewModel : ObservableRecipient
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LogViewViewModel()
    {
        InitAsync();
    }

    #region 声明通知属性
    private JobLogDTO _JobLogDTO;
    /// <summary>
    /// 日志信息查询参数对象
    /// </summary>
    public JobLogDTO JobLogDTO
    {
        get => _JobLogDTO;
        set => SetProperty(ref _JobLogDTO, value);
    }

    private List<TbJobLog> _TbJobLogList;
    /// <summary>
    /// 日志信息列表对象
    /// </summary>
    public List<TbJobLog> TbJobLogList
    {
        get => _TbJobLogList;
        set => SetProperty(ref _TbJobLogList, value);
    }

    private int _TotalCount;
    /// <summary>
    /// 总记录数
    /// </summary>
    public int TotalCount
    {
        get => _TotalCount;
        set => SetProperty(ref _TotalCount, value);
    }

    private string _TotalCountString;
    /// <summary>
    /// 总记录数字符串
    /// </summary>
    public string TotalCountString
    {
        get => _TotalCountString;
        set => SetProperty(ref _TotalCountString, value);
    }

    private int _PageSize;
    /// <summary>
    /// 每页多少条
    /// </summary>
    public int PageSize
    {
        get => _PageSize;
        set => SetProperty(ref _PageSize, value);
    }

    private int _PageIndex;
    /// <summary>
    /// 当前页索引
    /// </summary>
    public int PageIndex
    {
        get => _PageIndex;
        set => SetProperty(ref _PageIndex, value);
    }

    private int _MaxPage;
    /// <summary>
    /// 最大页数
    /// </summary>
    public int MaxPage
    {
        get => _MaxPage;
        set => SetProperty(ref _MaxPage, value);
    }

    private string _ImageVisibility;
    /// <summary>
    /// 预览图片可见性
    /// </summary>
    public string ImageVisibility
    {
        get => _ImageVisibility;
        set => SetProperty(ref _ImageVisibility, value);
    }

    private string _JsonBodyVisibility;
    /// <summary>
    /// Kafka 消息可见性
    /// </summary>
    public string JsonBodyVisibility
    {
        get => _JsonBodyVisibility;
        set => SetProperty(ref _JsonBodyVisibility, value);
    }

    private BitmapSource _ImageUri;
    /// <summary>
    /// 图片Uri
    /// </summary>
    public BitmapSource ImageUri
    {
        get => _ImageUri;
        set => SetProperty(ref _ImageUri, value);
    }

    private string _JsonBody;
    /// <summary>
    /// Kafka 消息体
    /// </summary>
    public string JsonBody
    {
        get => _JsonBody;
        set => SetProperty(ref _JsonBody, value);
    }

    #endregion

    #region 事件声明与实现

    /// <summary>
    /// 查询事件声明
    /// </summary>
    public RelayCommand<EventArgs> SearchCommand => new(Search);
    /// <summary>
    /// 查询事件实现
    /// </summary>
    /// <param name="args"></param>
    private void Search(EventArgs args)
    {
        try
        {
            if (JobLogDTO.StartTime > JobLogDTO.EndTime)
            {
                Growl.Warning($"开始时间[{JobLogDTO.StartTime}]必须小于等于结束时间[{JobLogDTO.EndTime}]，请确认!");
                return;
            }
            TbJobLogList = JobLogService.Get(JobLogDTO, PageIndex, PageSize, out int cnt);
            TotalCount = cnt;
            MaxPage = (TotalCount / PageSize) + ((TotalCount % PageSize) > 0.0 ? 1 : 0);
            TotalCountString = $"共计 {TotalCount} 条，每页条数 ";
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"查询失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }
    }

    /// <summary>
    /// 页码跳转事件声明
    /// </summary>
    public RelayCommand<EventArgs> PageUpdatedCommand => new(PageUpdated);
    /// <summary>
    /// 页码跳转事件实现
    /// </summary>
    /// <param name="args"></param>
    private void PageUpdated(EventArgs args)
    {
        try
        {
            if (args is FunctionEventArgs<int> index)
            {
                PageIndex = index.Info;
            }
            TbJobLogList = JobLogService.Get(JobLogDTO, PageIndex, PageSize, out int cnt);
            TotalCount = cnt;
            MaxPage = (TotalCount / PageSize) + ((TotalCount % PageSize) > 0.0 ? 1 : 0);
            TotalCountString = $"共计 {TotalCount} 条，每页条数 ";
        }
        catch (Exception ex)
        {
            Growl.Error(new GrowlInfo()
            {
                Message = $"查询失败，错误信息 - {ex.Message}",
                WaitTime = 1
            });
        }
    }

    /// <summary>
    /// 关闭图片预览事件声明
    /// </summary>
    public RelayCommand<EventArgs> CloseCommand => new(Close);
    /// <summary>
    /// 关闭图片预览事件实现
    /// </summary>
    /// <param name="args"></param>
    private void Close(EventArgs args)
    {
        ImageVisibility = "Hidden";
        JsonBodyVisibility = "Hidden";
    }

    /// <summary>
    /// 图片预览事件声明
    /// </summary>
    public RelayCommand<EventArgs> ReviewImageCommand => new(ReviewImage);
    /// <summary>
    /// 图片预览事件实现
    /// </summary>
    /// <param name="args"></param>
    private async void ReviewImage(EventArgs args)
    {
        try
        {
            MouseButtonEventArgs e = args as MouseButtonEventArgs;
            SelectedJobLog = ((System.Windows.FrameworkElement) e.OriginalSource).DataContext as TbJobLog;
            if (!Directory.Exists($@"C:\filecache\"))
            {
                Directory.CreateDirectory($@"C:\filecache\");
            }
            var ext = Path.GetExtension(SelectedJobLog.MinIourl);
            var fileName = @$"C:\filecache\{SelectedJobLog.MinIofileName}";
            if (".png" == ext.ToLower() || ".jpg" == ext.ToLower() || ".jpeg" == ext.ToLower())
            {
                //MinIO = MinioService.Create(GlobalInfo.EndPoint, GlobalInfo.AccessKey, GlobalInfo.SecretKey);
                await MinioService.FGetObjectAsync(MinIO, SelectedJobLog.MinIobucketName, SelectedJobLog.MinIourl.Replace($"/{SelectedJobLog.MinIobucketName}/", ""), fileName);
                BitmapSource bitmap = new BitmapImage(new Uri(fileName));
                ImageUri = bitmap;
                ImageVisibility = "Visible";
            }
            else
            {
                ImageVisibility = "Hidden";
                Growl.Warning("该对象不支持预览，请知悉！");
            }
        }
        catch (Exception ex)
        {
            ImageVisibility = "Hidden";
            Growl.Warning($"预览失败，错误原因 - {ex.Message}");
        }
    }

    /// <summary>
    /// Kafka 消息预览事件声明
    /// </summary>
    public RelayCommand<EventArgs> ReviewKafkaMsgCommand => new(ReviewKafkaMsg);
    /// <summary>
    /// Kafka 消息预览事件实现
    /// </summary>
    /// <param name="args"></param>
    private async void ReviewKafkaMsg(EventArgs args)
    {
        try
        {
            MouseButtonEventArgs e = args as MouseButtonEventArgs;
            SelectedJobLog = ((System.Windows.FrameworkElement) ((System.Windows.RoutedEventArgs) args).Source).DataContext as TbJobLog;

            //JsonBody = SelectedJobLog.KafkaBody;
            //JsonBody = JsonConvert.SerializeObject(SelectedJobLog.KafkaBody, Formatting.None);

            JToken parsedJson = JToken.Parse(SelectedJobLog.KafkaBody);
            JsonBody = parsedJson.ToString(Formatting.Indented);

            JsonBodyVisibility = "Visible";
        }
        catch (Exception ex)
        {
            JsonBodyVisibility = "Hidden";
            Growl.Warning($"预览失败，错误原因 - {ex.Message}");
        }
    }




    public RelayCommand<EventArgs> ExportCommand => new(Export);
    /// <summary>
    /// 导出报表
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private void Export(EventArgs args)
    {
        var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
        if (dialog.ShowDialog().GetValueOrDefault())
        {
            var fileName = @$"{dialog.SelectedPath}\对象采集日志{DateTime.Now:yyyyMMddHH}.xlsx";
            bool b = JobLogService.Post(JobLogDTO, fileName, out string message);
            if (b)
            {
                Growl.Success(new GrowlInfo()
                {
                    Message = $"导出成功 - {fileName}",
                    WaitTime = 1
                });
            }
            else
            {
                Growl.Error(new GrowlInfo()
                {
                    Message = $"导出失败，错误信息 - {message}",
                    WaitTime = 1
                });
            }
        }
    }

    #endregion

    #region 私有属性
    /// <summary>
    /// MinIO 实例
    /// </summary>
    public MinioClient MinIO { get; set; } = MinioService.Create(GlobalInfo.EndPoint, GlobalInfo.AccessKey, GlobalInfo.SecretKey);
    /// <summary>
    /// 选中的日志记录
    /// </summary>
    public TbJobLog? SelectedJobLog { get; set; } = new();
    #endregion

    #region 私有函数
    /// <summary>
    /// 初始化
    /// </summary>
    private void InitAsync()
    {
        ImageVisibility = "Hidden";
        JsonBodyVisibility = "Hidden";
        JsonBody = string.Empty;
        //ImageVisibility = "Visible";
        JobLogDTO = new()
        {
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = DateTime.Now.AddDays(1),
        };
        PageSize = 10;
        PageIndex = 0;
        TotalCount = 0;
        TotalCountString = $"共 {TotalCount} 条，每页条数 ";
    }
    #endregion

}
