using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class BaseCommand
    {
        private string homePath;  //DBMS根目录
        private FileIO fileIO;  //具体数据库操作类
        private string currentDataBase;   //当前数据库

        public BaseCommand()
        {
            fileIO = new FileIO();
            currentDataBase = "";
            homePath = @"c:\LSQL\";
        }

        /// <summary>
        /// 切换当前数据库
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns>切换结果</returns>
        public string useDatabase(string name)
        {
            if (hadExistedDatabase(name))
            {
                currentDataBase = name;
                return "select success";
            }
            return "Database not exist";
        }

        /// <summary>
        /// 判断数据库是否存在
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns>判断结果</returns>
        private bool hadExistedDatabase(string name)
        {
            string[] allDatabase = getAllDatabase();
            for (int i=0;i<allDatabase.Length;i++)
                if (allDatabase[i] == name)
                    return true;
            return false;
        }

        /// <summary>
        /// 获得所有数据库
        /// </summary>
        /// <returns>数据库名字的数组</returns>
        private string[] getAllDatabase()
        {
            string[] databasesPath = fileIO.getAllFolder(homePath); //返回所有数据库路径
            string result = "";
            for (int i = 0; i < databasesPath.Length; i++)
            {
                string[] databaseName = databasesPath[i].Split('\\');   //提取最后面的数据库名
                result += databaseName[databaseName.Length - 1] + "|";
            }
            return result.Split('|');
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns>创建结果</returns>
        public string createDataBase(string name)
        {
            return fileIO.createFolder(homePath + name);
        }

        /// <summary>
        /// 显示所有数据库
        /// </summary>
        /// <returns>所有数据库名，换行隔开</returns>
        public string showDataBases()
        {
            string result = "";
            string[] databaseName = getAllDatabase(); //返回所有数据库
            for (int i = 0; i < databaseName.Length; i++)
                result += databaseName[i] + "\r\n";
            return result;
        }

        /// <summary>
        /// 在当前数据库中创建表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>创建结果</returns>
        public string createTable(string tableName)
        {
            if (currentDataBase == "")
                return "Please select database";
            else if (tableName == "")
                return "Please input table name";
            else
                return fileIO.createFile(homePath + currentDataBase + @"\" + tableName);
        }
    }
}
