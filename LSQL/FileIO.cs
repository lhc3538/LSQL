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
        private string homePath = @"c:\LSQL\";

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="name">文件夹名</param>
        /// <returns></returns>
        public string createFolder(string name)
        {
            if (name != "")
            {
                string allPath = homePath + name;
                try
                {
                    // Determine whether the directory exists.
                    if (!Directory.Exists(allPath))
                    {
                        // Create the directory it does not exist.
                        Directory.CreateDirectory(allPath);
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

        public string createFile(string databaseName,string fileName)
        {
            if (databaseName!="" && fileName!="")
            {
                string allPath = homePath + databaseName + @"\" + fileName;
                try
                {
                    // Determine whether the directory exists.
                    if (!File.Exists(allPath))
                    {
                        // Create the directory it does not exist.
                        File.Create(allPath);
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
        public string[] getAllFolder()
        {
            return Directory.GetDirectories(homePath);
        }
    }
}
