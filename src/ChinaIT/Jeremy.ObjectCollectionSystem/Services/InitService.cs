using System.IO;
using Jeremy.ObjectCollectionSystem.Domains;
using Microsoft.EntityFrameworkCore;

namespace Jeremy.ObjectCollectionSystem.Services;

internal class InitService
{
    /// <summary>
    /// 初始化参数
    /// </summary>
    /// <returns></returns>
    public static async Task<bool> Init()
    {
        try
        {
            //GlobalInfo.Ip = NetHelper.GetIp2();
            //GlobalInfo.Mac = NetHelper.Mac();
            using DataContext context = new();
            var basic = await context.TbBasicConfigs.FirstOrDefaultAsync();
            GlobalInfo.Ip = basic.Ip;
            GlobalInfo.Mac = basic.Mac;
            GlobalInfo.DeviceNumber = basic.DeviceNumber;
            GlobalInfo.DeviceName = basic.DeviceName;
            var kafka = await context.TbKafkaConfigs.FirstOrDefaultAsync();
            GlobalInfo.BootstrapServers = kafka.BootstrapServers;
            GlobalInfo.Topic = kafka.Topic;
            var minIO = await context.TbMinioConfigs.FirstOrDefaultAsync();
            GlobalInfo.EndPoint = minIO.EndPoint;
            GlobalInfo.AccessKey = minIO.AccessKey;
            GlobalInfo.SecretKey = minIO.SecretKey;
            GlobalInfo.BucketName = minIO.BucketName;
            GlobalInfo.MinIOPath = minIO.Path;
            var job = await context.TbJobConfigs.FirstOrDefaultAsync();
            GlobalInfo.ScanPath = job.ScanPath;
            GlobalInfo.ScanInterval = job.ScanInterval;
            GlobalInfo.JobId = job.Id;
            GlobalInfo.JobName = job.Name;
            GlobalInfo.IsDeleteFile = job.IsDelete;
            if (!Directory.Exists($"{Path.GetPathRoot(GlobalInfo.ScanPath)}/done"))
            {
                Directory.CreateDirectory($"{Path.GetPathRoot(GlobalInfo.ScanPath)}/done");
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
