using Jeremy.ObjectCollectionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Jeremy.ObjectCollectionSystem.Services;

/// <summary>
/// MinIO 配置相关服务
/// </summary>
public class MinIOConfigService
{
    /// <summary>
    /// 获取 MinIO 信息配置
    /// </summary>
    /// <returns>MinIO 信息</returns>
    public static async Task<TbMinioConfig?> GetAsync()
    {
        try
        {
            using DataContext Db = new();
            return await Db.TbMinioConfigs.FirstOrDefaultAsync();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 新增 MinIO 信息配置
    /// </summary>
    /// <param name="config">MinIO 信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static async Task<bool> PostAsync(TbMinioConfig config)
    {
        try
        {
            using DataContext Db = new();
            Db.TbMinioConfigs.Add(config);
            return await Db.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 更新 MinIO 信息配置
    /// </summary>
    /// <param name="config">MinIO 信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static bool Put(TbMinioConfig config)
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
