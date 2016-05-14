using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    /// <summary>
    /// 以c:\LSQL\为根目录进行文件操作
    /// </summary>
    class FileIO
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="name">文件夹路径</param>
        /// <returns></returns>
        public string createFolder(string path)
        {
            if (path != "")
            {
                try
                {
                    // Determine whether the directory exists.
                    if (!Directory.Exists(path))
                    {
                        // Create the directory it does not exist.
                        Directory.CreateDirectory(path);
                        return "success";
                    }
                    else
                        return "error:had existed";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            return "error:empty name";
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">创建位置</param>
        /// <returns>创建结果</returns>
        public string createFile(string path)
        {
            if (path!="")
            {
                try
                {
                    // Determine whether the fiel exists.
                    if (!File.Exists(path))
                    {
                        // Create the file it does not exist.
                        File.Create(path);
                        return "success";
                    }
                    else
                        return "error:had existed";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            return "error:empty name";
        }

        /// <summary>
        /// 获取指定目录下所有文件夹名
        /// </summary>
        /// <returns>文件夹名数组</returns>
        public string[] getAllFolder(string path)
        {
            return getAllName(Directory.GetDirectories(path));
        }

        /// <summary>
        /// 获取指定目录下所有文件
        /// </summary>
        /// <param name="path">指定目录</param>
        /// <returns>文件名数组</returns>
        public string[] getAllFile(string path)
        {
            return getAllName(Directory.GetFiles(path));
        }

        /// <summary>
        /// 从路径数组中提取最后一项重新组成数组
        /// </summary>
        /// <param name="allPath">路径数组</param>
        /// <returns>最后一项的数组</returns>
        private string[] getAllName(string[] allPath)
        {
            string result = "";
            for (int i = 0; i < allPath.Length; i++)
            {
                string[] name = allPath[i].Split('\\');   //提取最后面的数据库名
                if (i == 0)
                    result += name[name.Length - 1];
                else
                    result += "|" + name[name.Length - 1];
            }
            return result.Split('|');
        }
    }
}
