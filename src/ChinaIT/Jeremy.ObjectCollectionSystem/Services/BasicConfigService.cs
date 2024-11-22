using Jeremy.ObjectCollectionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Jeremy.ObjectCollectionSystem.Services;


/// <summary>
/// 基础配置相关服务
/// </summary>
public class BasicConfigService
{
    /// <summary>
    /// 获取基础信息配置
    /// </summary>
    /// <returns>基础信息</returns>
    public static async Task<TbBasicConfig?> GetAsync()
    {
        try
        {
            using DataContext Db = new();
            return await Db.TbBasicConfigs.FirstOrDefaultAsync();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 新增基础信息配置
    /// </summary>
    /// <param name="config">基础信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static bool Post(TbBasicConfig config)
    {
        try
        {
            using DataContext Db = new();
            Db.TbBasicConfigs.Add(config);
            return Db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 更新基础信息配置
    /// </summary>
    /// <param name="config">基础信息</param>
    /// <returns>成功 true 失败 false</returns>
    public static bool Put(TbBasicConfig config)
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

