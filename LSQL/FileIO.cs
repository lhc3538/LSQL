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
        /// 获取根目录下所有文件夹名
        /// </summary>
        /// <returns></returns>
        public string[] getAllFolder(string path)
        {
            return Directory.GetDirectories(path);
        }
    }
}
