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

        public string createDataBase(string name)
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
            return "success";
        }
    }
}
