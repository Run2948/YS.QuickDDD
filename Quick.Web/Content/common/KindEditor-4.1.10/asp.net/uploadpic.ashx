<%@ webhandler Language="C#" class="uploadpic" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

public class uploadpic : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string pic = context.Request.QueryString["pic"];

        string[] arr = pic.Split('|');
        string sstr = "";
        UpLoadIMG st = new UpLoadIMG();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].IndexOf("http://") >= 0 || arr[i].IndexOf("https://") >= 0)
            {
                string std = st.SaveUrlPics(arr[i], "attached/image/");
                if (std.Length > 0)
                {
                    if (i == arr.Length - 1)
                        sstr += std;
                    else
                        sstr += std + "|";
                }
            }
        }
        context.Response.Write(sstr);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

public class UpLoadIMG
{
    public string SaveUrlPics(string imgurlAry, string path)
    {
        string strHTML = "";
        string dirPath = HttpContext.Current.Server.MapPath(path);

        try
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + imgurlAry.Substring(imgurlAry.LastIndexOf("."));

            System.Net.WebClient wc = new System.Net.WebClient();
            wc.DownloadFile(imgurlAry, dirPath + newFileName);
            strHTML = ymd + "/" + newFileName;
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return strHTML;
    }
}