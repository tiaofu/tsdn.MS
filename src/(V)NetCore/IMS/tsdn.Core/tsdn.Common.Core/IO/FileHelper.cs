﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System;
using System.IO;

namespace tsdn.Common.Core
{
    /// <summary>
    /// 文件相关助手类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 目录分隔符
        /// windows "\" mac os and linux "/"
        /// </summary>
        private static string DirectorySeparatorChar = Path.DirectorySeparatorChar.ToString();

        /// <summary>
        /// 应用程序根路径
        /// </summary>
        private static string _ContentRootPath = AppContext.BaseDirectory;

        /// <summary>
        　　/// 获取文件绝对路径
        　　/// </summary>
        　　/// <param name="path">文件路径</param>
        　　/// <returns></returns>
        public static string MapPath(string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(_ContentRootPath, path.TrimStart('~', '/').Replace("/", DirectorySeparatorChar));
        }

        /// <summary>
        /// 获取文件的绝对路径
        /// </summary>
        /// <param name="path">相对路径地址</param>
        /// <returns>绝对路径地址</returns>
        public static string GetAbsolutePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("参数path空异常！");
            }
            return MapPath(path);
        }

        /// <summary>
        /// 是否是绝对路径
        /// windows下判断 路径是否包含 ":"
        /// Mac OS、Linux下判断 路径是否包含 "\"
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }

        /// <summary>
        /// 检测指定路径是否存在
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        /// <returns></returns>
        public static bool IsExist(string path, bool isDirectory)
        {
            return isDirectory ? Directory.Exists(MapPath(path)) : File.Exists(MapPath(path));
        }

        /// <summary>
        /// 获取目录的绝对路径
        /// </summary>
        /// <param name="dir">相对路径</param>
        /// <param name="create">不存在是否创建</param>
        /// <returns>绝对目录路径</returns>
        public static string GetDirectoryPath(string dir, bool create = false)
        {
            string path = MapPath(dir);
            if (create && !Directory.Exists(MapPath(path)))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 检测目录是否为空
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool IsEmptyDirectory(string path)
        {
            return Directory.GetFiles(MapPath(path)).Length <= 0 && Directory.GetDirectories(MapPath(path)).Length <= 0;
        }

        /// <summary>
        /// 创建目录或文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        public static void CreateFiles(string path, bool isDirectory)
        {
            try
            {
                if (!IsExist(path, isDirectory))
                {
                    if (isDirectory)
                        Directory.CreateDirectory(MapPath(path));
                    else
                    {
                        FileInfo file = new FileInfo(MapPath(path));
                        FileStream fs = file.Create();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除目录或文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        public static void DeleteFiles(string path, bool isDirectory)
        {
            try
            {
                if (!IsExist(path, isDirectory))
                {
                    if (isDirectory)
                        Directory.Delete(MapPath(path));
                    else
                        File.Delete(MapPath(path));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 清空目录下所有文件及子目录，依然保留该目录
        /// </summary>
        /// <param name="path"></param>
        public static void ClearDirectory(string path)
        {
            if (IsExist(path, true))
            {
                //目录下所有文件
                string[] files = Directory.GetFiles(MapPath(path));
                foreach (var file in files)
                {
                    DeleteFiles(file, false);
                }
                //目录下所有子目录
                string[] directorys = Directory.GetDirectories(MapPath(path));
                foreach (var dir in directorys)
                {
                    DeleteFiles(dir, true);
                }
            }
        }

        /// <summary>
        /// 复制文件内容到目标文件夹
        /// </summary>
        /// <param name="sourcePath">源文件</param>
        /// <param name="targetPath">目标文件夹</param>
        /// <param name="isOverWrite">是否可以覆盖</param>
        public static void Copy(string sourcePath, string targetPath, bool isOverWrite = true)
        {
            File.Copy(MapPath(sourcePath), MapPath(targetPath) + GetFileName(sourcePath), isOverWrite);
        }

        /// <summary>
        /// 移动文件到目标目录
        /// </summary>
        /// <param name="sourcePath">源文件</param>
        /// <param name="targetPath">目标目录</param>
        public static void Move(string sourcePath, string targetPath)
        {
            string sourceFileName = GetFileName(sourcePath);
            //如果目标目录不存在则创建
            if (!IsExist(targetPath, true))
            {
                CreateFiles(targetPath, true);
            }
            else
            {
                //如果目标目录存在同名文件则删除
                if (IsExist(Path.Combine(MapPath(targetPath), sourceFileName), false))
                {
                    DeleteFiles(Path.Combine(MapPath(targetPath), sourceFileName), true);
                }
            }
            File.Move(MapPath(sourcePath), Path.Combine(MapPath(targetPath), sourceFileName));
        }

        /// <summary>
        /// 获取文件名和扩展名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            return Path.GetFileName(MapPath(path));
        }

        /// <summary>
        /// 获取文件名不带扩展名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string GetFileNameWithOutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(MapPath(path));
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string GetFileExtension(string path)
        {
            return Path.GetExtension(MapPath(path));
        }
    }
}
