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
            string[] allDatabase = fileIO.getAllFolder(homePath);
            for (int i=0;i<allDatabase.Length;i++)
                if (allDatabase[i] == name)
                    return true;
            return false;
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
            return formatStringArray(fileIO.getAllFolder(homePath));
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

        /// <summary>
        /// 显示当前数据库所有数据表
        /// </summary>
        /// <returns>所有表名，换行隔开</returns>
        public string showTables()
        {
            if (currentDataBase!="")
                return formatStringArray(fileIO.getAllFile(homePath + currentDataBase));
            return "Please select database";
        }

        /// <summary>
        /// 格式化字符串数组
        /// 每个字符串之间添加换行
        /// </summary>
        /// <param name="strArr">字符串数组</param>
        /// <returns>格式化后的字符串</returns>
        protected string formatStringArray(string[] strArr)
        {
            string result = "";
            for (int i = 0; i < strArr.Length; i++)
            {
                if (i == 0)
                    result += strArr[i];
                else
                    result += "\r\n" + strArr[i];
            }
            return result;
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns></returns>
        public string dropDatabase(string name)
        {
            if (name != "")
                return fileIO.deleteDirectory(homePath + name);
            return "Please input dabases name";
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string dropTable(string name)
        {
            if (name == "")
                return "Please input table name";
            else if (currentDataBase == "")
                return "Please select database";
            else
                return fileIO.deleteFile(homePath + currentDataBase + @"\" + name);
        }

        public string insertRecord(string table,string strline)
        {
            return fileIO.appendLine(strline, homePath + currentDataBase + @"\" + table);
        }

        /// <summary>
        /// 显示所有记录
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>格式化后的所有记录</returns>
        public string showRecords(string table)
        {
            string records = "";
            List<string> list = fileIO.readAllLine(homePath + currentDataBase + @"\" + table);
            foreach(string line in list)
            {
                records += line + "\r\n";
            }
            return records;
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string delRecord(string table,int id)
        {
            return fileIO.delLine(homePath + currentDataBase + @"\" + table, id);
        }

        public string modifyRecord(string table,int id,string linestr)
        {
            return fileIO.modifyLine(homePath + currentDataBase + @"\" + table, id, linestr);
        }
    }
}
