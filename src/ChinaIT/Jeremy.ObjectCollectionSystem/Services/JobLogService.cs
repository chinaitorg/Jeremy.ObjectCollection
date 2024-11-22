using Jeremy.ObjectCollectionSystem.DataTransferObjects;
using Jeremy.ObjectCollectionSystem.Models;
using Microsoft.EntityFrameworkCore;
using Npoi.Mapper;

namespace Jeremy.ObjectCollectionSystem.Services;


public class JobLogService
{
    /// <summary>
    /// 根据查询条件获取
    /// </summary>
    /// <param name="job">查询条件</param>
    /// <returns>日志列表信息</returns>
    public static async Task<List<TbJobLog>> GetAsync(JobLogDTO job)
    {
        try
        {
            using DataContext Db = new();
            var data = await Db.TbJobLogs.Where(x => x.CreateTime >= job.StartTime && x.CreateTime < job.EndTime).ToListAsync();
            if (job.FileName is not null)
            {
                data = data.Where(x => x.FileName.Contains(job.FileName)).ToList();
            }
            return data.OrderByDescending(x => x.CreateTime).ToList();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 根据查询条件获取分页日志信息
    /// </summary>
    /// <param name="job">查询条件</param>
    /// <param name="pageIndex">分页索引</param>
    /// <param name="pageSize">每页多少条</param>
    /// <param name="totalCount">共计多少条</param>
    /// <returns>日志信息</returns>
    public static List<TbJobLog> Get(JobLogDTO job, int pageIndex, int pageSize, out int totalCount)
    {
        try
        {
            using DataContext Db = new();
            var data = Db.TbJobLogs.Where(x => x.CreateTime >= job.StartTime && x.CreateTime < job.EndTime).ToList();
            if (job.FileName is not null)
            {
                data = data.Where(x => x.MinIofileName.Contains(job.FileName) || x.FileName.Contains(job.FileName)).ToList();
            }
            totalCount = data.Count;
            return data.OrderByDescending(x => x.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        catch
        {
            totalCount = 0;
            return null;
        }
    }

    /// <summary>
    /// 根据查询条件导出文件到指定的路径下
    /// </summary>
    /// <param name="job">查询条件</param>
    /// <param name="fileFullPath">导出文件的全路径</param>
    /// <param name="message">消息</param>
    /// <returns>true 成功 false 失败</returns>
    public static bool Post(JobLogDTO job, string fileFullPath, out string message)
    {
        try
        {
            using DataContext Db = new();
            var data = Db.TbJobLogs.Where(x => x.CreateTime >= job.StartTime && x.CreateTime < job.EndTime).ToList();
            if (job.FileName is not null)
            {
                data = data.Where(x => x.FileName.Contains(job.FileName)).ToList();
            }
            data = data.OrderByDescending(x => x.CreateTime).ToList();
            if (data is not null)
            {
                var mapper = new Mapper();
                mapper.Map<TbJobLog>("系统标识", x => x.Id).Map<TbJobLog>("任务ID", x => x.JobId).Map<TbJobLog>("创建人", x => x.CreateBy).Map<TbJobLog>("备注", x => x.Comment).Map<TbJobLog>("时间", x => x.CreateTime).Map<TbJobLog>("源文件路径", x => x.OriginFilefullPath).Map<TbJobLog>("源文件名称", x => x.FileName).Map<TbJobLog>("文件类型", x => x.FileType).Map<TbJobLog>("是否上传MinIO", x => x.IsMinIo).Map<TbJobLog>("上传MinIO时间", x => x.IsMinIotime).Map<TbJobLog>("MinIO桶名称", x => x.MinIobucketName).Map<TbJobLog>("MinIO路径", x => x.MinIopath).Map<TbJobLog>("MinIO文件名称", x => x.MinIofileName).Map<TbJobLog>("MinIO路径", x => x.MinIourl).Map<TbJobLog>("是否推送Kafka", x => x.IsKafka).Map<TbJobLog>("推送Kafka时间", x => x.IsKafkaTime).Map<TbJobLog>("Kafka Topic", x => x.KafkaTopic).Map<TbJobLog>("推送报文", x => x.KafkaBody).Format<TbJobLog>("yyyy-MM-dd hh:mm:ss", x => x.CreateTime).Format<TbJobLog>("yyyy-MM-dd hh:mm:ss", x => x.IsMinIotime).Format<TbJobLog>("yyyy-MM-dd hh:mm:ss", x => x.IsKafkaTime);
                mapper.Save(fileFullPath, data);
            }
            message = $"导出成功，请到 {fileFullPath} 目录下查看！";
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 添加job日志
    /// </summary>
    /// <param name="log">Job日志模型信息</param>
    /// <returns></returns>
    public static async Task<bool> PutAsync(TbJobLog log)
    {
        try
        {
            using DataContext dataContext = new();
            dataContext.TbJobLogs.Add(log);
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 按照指定时间清空历史数据
    /// </summary>
    /// <param name="days">天数</param>
    /// <returns></returns>
    public static async Task<bool> DeleteAsync(int days)
    {
        try
        {
            using DataContext dataContext = new();
            var logs = dataContext.TbJobLogs.Where(x => DateTime.Now.AddDays(-days) >= x.CreateTime);
            dataContext.TbJobLogs.RemoveRange(logs);
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 获取历史采集的源文件全路径集合
    /// </summary>
    /// <returns>源文件全路径集合</returns>
    public static List<string> Get()
    {
        using DataContext dataContext = new();
        return dataContext.TbJobLogs.Select(x => x.OriginFilefullPath).Distinct().ToList();
    }
}

