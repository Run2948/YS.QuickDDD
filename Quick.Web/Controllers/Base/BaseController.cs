using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Quick.Core.Domain;

namespace Quick.Web.Controllers
{
    public class BaseController : Controller
    {
        #region 获取应用程序的Debug模式

        protected object DebugMode = ConfigurationManager.AppSettings["IsDebug"];

        #endregion

        #region StringSplit返回字符串数组

        private readonly char[] _splits = { ',', ':', '，', '：' };
        /// <summary>
        /// 通用字符串分割
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string[] Split(string str)
        {
            return str.Split(_splits, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region 通用返回JsonResult的封装
        /// <summary>
        /// 只返回响应状态和提示信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult ConvertJsonresult(int status, string msg, bool get = false)
        {
            JsonResult js = new JsonResult();
            ResultInfo info = new ResultInfo
            {
                status = status,
                msg = msg
            };
            js.Data = info;
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }


        /// <summary>
        /// 返回响应状态、提示信息和跳转地址
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="url">跳转地址</param>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult ConvertJsonresult(int status, string msg, string url, bool get = false)
        {
            JsonResult js = new JsonResult();
            ResultInfo info = new ResultInfo
            {
                status = status,
                msg = msg,
                url = url
            };
            js.Data = info;
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        /// <summary>
        /// 返回响应状态、消息和数据对象
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult ConvertJsonresult(int status, string msg, object data, bool get = false)
        {
            JsonResult js = new JsonResult();
            ResultInfo info = new ResultInfo
            {
                status = status,
                msg = msg,
                data = data
            };
            js.Data = info;
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        /// <summary>
        /// 返回响应状态、消息、数据和跳转地址
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <param name="url">跳转地址</param>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult ConvertJsonresult(int status, string msg, object data, string url, bool get = false)
        {
            JsonResult js = new JsonResult();
            ResultInfo info = new ResultInfo
            {
                status = status,
                msg = msg,
                data = data,
                url = url
            };
            js.Data = info;
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        #endregion     

        #region 通用返回完成页面 Finished
        protected ActionResult FinishResult(string msgText)
        {
            return !string.IsNullOrEmpty(msgText) ? RedirectToAction("Finished", "ErrorPage", new { msg = msgText }) : RedirectToAction("Finished", "ErrorPage");
        }
        #endregion

        #region 删除临时文件
        /// <summary>
        /// 删除临时文件
        /// </summary>
        /// <param name="filePath">临时文件路径</param>
        protected void DeleteFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return;
            FileInfo file = new FileInfo(filePath);
            try
            {
                if (file.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                {
                    file.Attributes = FileAttributes.Normal;
                }
                System.IO.File.Delete(file.FullName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 删除临时目录下的所有文件
        /// <summary>
        /// 删除临时目录下的所有文件
        /// </summary>
        /// <param name="tempPath">临时目录路径</param>
        protected void DeleteFiles(string tempPath)
        {
            if (!System.IO.Directory.Exists(tempPath))
                return;
            DirectoryInfo directory = new DirectoryInfo(tempPath);
            try
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                    {
                        file.Attributes = FileAttributes.Normal;
                    }
                    System.IO.File.Delete(file.FullName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 删除临时目录下的所有文件及文件夹
        protected void DeleteFilesAndFolders(string path)
        {
            if (!System.IO.Directory.Exists(path))
                return;

            // Delete files.  
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                System.IO.File.Delete(file);
            }

            // Delete folders.  
            string[] folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                DeleteFilesAndFolders(folder);
                Directory.Delete(folder);
            }
        }
        #endregion

        #region 图片上传
        /// <summary>
        /// 图片上传，对图片的大小有限制
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <param name="width">图片宽度限制</param>
        /// <param name="height">图片高度限制</param>
        /// <returns></returns>
        public string ImageUpload(HttpPostedFileBase file, string folderName, int width, int height)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (file == null)
            {
                return "no:上传图片文件不能为空!";
            }
            else
            {
                string fileName = Path.GetFileName(file.FileName); //文件名
                string fileExt = Path.GetExtension(fileName);  //文件后缀名
                if (fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".png" || fileExt == ".ico" || fileExt == "jpeg")  // 格式过滤
                {
                    string dir = "/upload/images/" + folderName + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                    if (!Directory.Exists(Request.MapPath(dir)))
                        Directory.CreateDirectory(Request.MapPath(dir));
                    string imagePath = dir + Guid.NewGuid().ToString() + fileExt;  //全球唯一标示符，作为文件名
                    file.SaveAs(Request.MapPath(imagePath));
                    var pic = Image.FromFile(Request.MapPath(imagePath));
                    if (pic.Width > width || pic.Height > height)
                    {
                        pic.Dispose();
                        DeleteFile(Request.MapPath(imagePath));
                        return "error:图片宽度在" + width + "像素以上,高度在" + height + "像素之间！不符合上传规则！";
                    }
                    pic.Dispose();
                    return "ok:" + imagePath;  // 上传成功，返回全路径 
                }
                else
                {
                    return "no:上传文件格式错误!!支持后缀为.jpg、.jpeg、png、.gif、.ico格式的图片上传";
                }
            }
        }

        /// <summary>
        /// 通用图片上传
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <returns></returns>
        public string ImageUpload(HttpPostedFileBase file, string folderName)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (file == null)
                return "no:上传图片文件不能为空!";

            string fileName = Path.GetFileName(file.FileName); //文件名
            string fileExt = Path.GetExtension(fileName);  //文件后缀名

            if (fileExt == null)
                return "no:无法识别的图片格式!!";

            fileExt = fileExt.ToLower();
            if (!".jpg".Equals(fileExt) && !".gif".Equals(fileExt) && !".png".Equals(fileExt) && !".ico".Equals(fileExt) && !"jpeg".Equals(fileExt))
                return "no:上传文件格式错误!!支持后缀为.jpg、.jpeg、png、.gif、.ico格式的图片上传";

            try
            {
                string dir = "/upload/images/" + folderName + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                if (!Directory.Exists(HttpContext.Request.MapPath(dir)))
                    Directory.CreateDirectory(HttpContext.Request.MapPath(dir));
                string imagePath = dir + Guid.NewGuid() + fileExt;  //全球唯一标示符，作为文件名
                file.SaveAs(HttpContext.Request.MapPath(imagePath));
                var pic = Image.FromFile(HttpContext.Request.MapPath(imagePath));
                pic.Dispose();
                return "ok:" + imagePath;  // 上传成功，返回全路径 
            }
            catch (Exception e)
            {
                return "no:" + e.Message;
            }
        }


        #endregion

        #region 文件上传

        /// <summary>
        /// 通用文件上传
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/excels/teachers/...，则直接填写 excels/teachers</param>
        /// <param name="extName">文件后缀</param>
        /// <returns></returns>
        public string FileUpload(HttpPostedFileBase file, string folderName, string extName = "")
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (file == null)
                return "no:上传文件不能为空!";

            string fileName = Path.GetFileName(file.FileName); //文件名
            string fileExt = Path.GetExtension(fileName);  //文件后缀名

            if (fileExt == null)
                return "no:无法识别的文件格式!!";

            if (!string.IsNullOrEmpty(extName))
                if (!fileExt.ToLower().Equals(extName))
                    return "no:上传文件格式错误!!";

            try
            {
                string dir = "/upload/" + folderName + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                if (!Directory.Exists(HttpContext.Request.MapPath(dir)))
                    Directory.CreateDirectory(HttpContext.Request.MapPath(dir));
                string filePath = dir + Guid.NewGuid() + fileExt; //全球唯一标示符，作为文件名 
                file.SaveAs(HttpContext.Request.MapPath(filePath));
                return "ok:" + filePath;  // 上传成功，返回全路径  
            }
            catch (Exception)
            {
                return "no:" + "上传文件失败";  // 上传成功，返回全路径  
            }
        }
        #endregion
    }
}