using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Jeremy.ObjectCollectionSystem.Domains;

/// <summary>
/// 加解密相关帮助类
/// </summary>
public class CryptHelper
{
    private static readonly string strKey = "JEREMY";
    private static readonly string strIV = "CHINAIT";

    /// <summary>
    /// 字符串加密
    /// </summary>
    /// <param name="str">要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string Encrypt(string str)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(str);
        MemoryStream ms = new();
        using (DESCryptoServiceProvider tdes = new())
        {
            CryptoStream encStream = new(ms, tdes.CreateEncryptor(Encoding.UTF8.GetBytes(strKey), Encoding.UTF8.GetBytes(strIV)), CryptoStreamMode.Write);
            encStream.Write(buffer, 0, buffer.Length);
            encStream.FlushFinalBlock();
        }
        return Convert.ToBase64String(ms.ToArray()).Replace("+", "%");
    }

    /// <summary>
    /// 字符串解密
    /// </summary>
    /// <param name="str">要解密的字符串</param>
    /// <returns>解密后的字符串</returns>
    public static string Decrypt(string str)
    {
        str = str.Replace("%", "+");
        byte[] buffer = Convert.FromBase64String(str);
        MemoryStream ms = new();
        using (DESCryptoServiceProvider tdes = new())
        {
            CryptoStream encStream = new(ms, tdes.CreateDecryptor(Encoding.UTF8.GetBytes(strKey), Encoding.UTF8.GetBytes(strIV)), CryptoStreamMode.Write);
            encStream.Write(buffer, 0, buffer.Length);
            encStream.FlushFinalBlock();
        }
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    /// <summary>
    /// 解密加载 xml 文档
    /// </summary>
    /// <param name="xmlDoc">xml 文档容器</param>
    /// <param name="fileName">加密文件的全路径</param>
    public static void XmlLoadDecrypt(XmlDocument xmlDoc, string fileName)
    {
        FileStream fileStream = new(fileName, FileMode.Open);
        byte[] bsXml = new byte[fileStream.Length];
        fileStream.Read(bsXml, 0, bsXml.Length);
        fileStream.Close();

        MemoryStream ms = new();
        using (DESCryptoServiceProvider tdes = new())
        {
            CryptoStream encStream = new(ms, tdes.CreateDecryptor(Encoding.UTF8.GetBytes(strKey), Encoding.UTF8.GetBytes(strIV)), CryptoStreamMode.Write);
            encStream.Write(bsXml, 0, bsXml.Length);
            encStream.FlushFinalBlock();
        }

        xmlDoc.Load(new MemoryStream(ms.ToArray()));
    }

    /// <summary>
    /// 加密存储 xml 文档
    /// </summary>
    /// <param name="xmlDoc">xml 文档容器</param>
    /// <param name="fileName">保存的文件全路径</param>
    public static void XmlSaveEncrypt(XmlDocument xmlDoc, string fileName)
    {
        if (!File.Exists(fileName))
            File.Create(fileName).Close();

        FileStream fileStream = new FileStream(fileName, FileMode.Truncate);
        MemoryStream msXml = new MemoryStream();
        xmlDoc.Save(msXml);

        using (DESCryptoServiceProvider tdes = new())
        {
            CryptoStream cs = new(fileStream, tdes.CreateEncryptor(Encoding.UTF8.GetBytes(strKey), Encoding.UTF8.GetBytes(strIV)), CryptoStreamMode.Write);
            cs.Write(msXml.ToArray(), 0, msXml.ToArray().Length);
            cs.FlushFinalBlock();
        }

        msXml.Close();
        fileStream.Close();
    }
}

