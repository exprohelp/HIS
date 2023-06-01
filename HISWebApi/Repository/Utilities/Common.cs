using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
	public Common()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
   
   
    
    public void ScriptMsg(string str, Page webpageinstance)
    {
       
        string strScript = "<script language=JavaScript runat=server> window.alert('" + str + "')</script>";
        if (!webpageinstance.ClientScript.IsStartupScriptRegistered("clientScript"))
        {
            webpageinstance.ClientScript.RegisterStartupScript(typeof(Page), "clientScript", strScript);
        }
    }

    public void ScriptMsgForAjax(string str, Page webpageinstance)
    {
        string strScript = "window.alert('" + str + "');";
        ScriptManager.RegisterStartupScript(webpageinstance, this.GetType(), "clientScript", strScript, true);
    }
    public static string Encrypt(string encryptText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptText.Replace("+", ""));
        using(Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptText;
    }
    public static string Decrypt(string decryptText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(decryptText.Replace(" ", "+"));
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                decryptText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return decryptText;
    }

    public  static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }  
}