using Jeremy.ObjectCollectionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Jeremy.ObjectCollectionSystem.Services;

/// <summary>
/// Kafka 配置相关服务
/// </summary>
public class KafkaConfigService
{
    /// <summary>
    /// 获取 Kafka 信息配置
    /// </summary>
    /// <returns>Kafka 信息</returns>
    public static async Task<TbKafkaConfig?> GetAsync()
    {
        try
        {
            using DataContext Db = new();
            return await Db.TbKafkaConfigs.FirstOrDefaultAsync();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 新增 Kafka 信息配置
    /// </summary>
    /// <param name="config">Kafka 信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static async Task<bool> PostAsync(TbKafkaConfig config)
    {
        try
        {
            using DataContext Db = new();
            Db.TbKafkaConfigs.Add(config);
            return await Db.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 更新 Kafka 信息配置
    /// </summary>
    /// <param name="config">Kafka 信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static bool Put(TbKafkaConfig config)
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
}
