using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class FileIO
    {
        private string homePath = @"c:\LSQL\";

        public string createFolder(string name)
        {
            if (name != "")
            {
                string allPath = homePath + name;
                // Determine whether the directory exists.
                if (!Directory.Exists(allPath))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(allPath);
                    return "created database:" + name;
                }
                else
                    return "database had exist";
            }
            return "please input name";
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
