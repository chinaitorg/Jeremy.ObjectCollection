namespace Jeremy.ObjectCollectionSystem.Domains;


public class MenuCollection
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public static List<string> ListMenuCollection { get; set; } = [];

    /// <summary>
    /// 检查菜单是否已经打开
    /// </summary>
    /// <param name="menuName">菜单名称</param>
    /// <returns>true 打开了 false 未打开</returns>
    public static bool CheckMenu(string menuName)
    {
        if (!ListMenuCollection.Exists(l => l == menuName))
        {
            ListMenuCollection.Add(menuName);
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 移除集合中的元素
    /// </summary>
    /// <param name="menuName">菜单名称</param>
    public static void CloseMenu(string menuName)
    {
        for (var i = ListMenuCollection.Count - 1; i >= 0; i--)
        {
            if (ListMenuCollection[i] == menuName)
            {
                ListMenuCollection.RemoveAt(i);
                return;
            }
        }
    }
}

