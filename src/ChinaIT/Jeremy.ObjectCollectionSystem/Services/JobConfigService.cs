using Jeremy.ObjectCollectionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Jeremy.ObjectCollectionSystem.Services;

/// <summary>
/// 计划配置相关服务
/// </summary>
public class JobConfigService
{
    /// <summary>
    /// 获取计划任务信息配置
    /// </summary>
    /// <returns>计划任务信息</returns>
    public static async Task<TbJobConfig?> GetAsync()
    {
        try
        {
            using DataContext Db = new();
            return await Db.TbJobConfigs.FirstOrDefaultAsync();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 新增计划任务信息配置
    /// </summary>
    /// <param name="config">计划任务信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static async Task<bool> PostAsync(TbJobConfig config)
    {
        try
        {
            using DataContext Db = new();
            Db.TbJobConfigs.Add(config);
            return await Db.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 更新计划任务信息配置
    /// </summary>
    /// <param name="config">计划任务信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static bool Put(TbJobConfig config)
    {
        try
        {
            using DataContext Db = new();
            Db.Attach(config).State = EntityState.Modified;
            return Db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 自动更新任务计划的状态
    /// </summary>
    /// <param name="isRuning">true-运行 false-暂停</param>
    /// <param name="message">消息</param>
    /// <returns></returns>
    public static async Task<bool> Put(bool isRuning)
    {
        try
        {
            using DataContext Db = new();
            var data = Db.TbJobConfigs.FirstOrDefault();
            if (data.IsRunning.Equals(isRuning)) return true;
            data.IsRunning = isRuning;
            Db.Attach(data).State = EntityState.Modified;
            return await Db.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }
}
