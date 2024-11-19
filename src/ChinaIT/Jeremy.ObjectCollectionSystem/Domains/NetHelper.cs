using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;

namespace Jeremy.ObjectCollectionSystem.Domains;


public class NetHelper
{
    /// <summary>
    /// 通过网络信息获取Mac地址
    /// </summary>
    /// <returns>返回Mac地址</returns>
    public static string Mac()
    {
        try
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                return BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
            }
        }
        catch (Exception)
        {
        }
        return "00-00-00-00-00-00";
    }

    /// <summary>
    /// 取本主机Ip地址
    /// </summary>
    /// <returns>主机Ip地址</returns>
    public static string GetIp()
    {
        ///获取本地的IP地址
        string AddressIP = string.Empty;
        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
            {
                AddressIP = _IPAddress.ToString();
            }
        }
        return AddressIP;
    }


    /// <summary>
    /// 取本机主机ip
    /// </summary>
    /// <returns>主机Ip地址</returns>
    public static string GetIp2()
    {
        try
        {
            string HostName = Dns.GetHostName(); //得到主机名
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    string ip = "";
                    ip = IpEntry.AddressList[i].ToString();
                    return IpEntry.AddressList[i].ToString();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
