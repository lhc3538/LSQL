using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class SQLDeal
    {
        private FileIO fileIO;  //具体数据库操作类
        private string currentDataBase;   //当前数据库
        public SQLDeal()
        {
            fileIO = new FileIO();
        }
        /// <summary>
        /// 终端指令处理函数
        /// </summary>
        /// <param name="str">终端传入的内容</param>
        public string dealTerminal(string str)
        {
            string[] CmdStr = str.Split(' ');
            if (CmdStr[0] == "create")
            {
                if (CmdStr[1] == "database")
                {
                    string result;
                    result = fileIO.createFolder(CmdStr[2]);
                    return result;
                }
            }
            else if (CmdStr[0] == "show")
            {
                if (CmdStr[1] == "databases")
                {
                    return showDataBases();
                }
            }
            else if (CmdStr[0] == "test")
            {
                return fileIO.createFile("lhc","hahaha");
            }
            return "Unable to identify";
        }

        /// <summary>
        /// 显示所有数据库
        /// </summary>
        /// <returns></returns>
        private string showDataBases()
        {
            string result = "";
            string[] databasesPath = fileIO.getAllFolder(); //返回所有数据库路径
            for (int i=0;i<databasesPath.Length;i++)
            {
                string[] databaseName = databasesPath[i].Split('\\');   //提取最后面的数据库名
                result += databaseName[databaseName.Length-1] + "\r\n";
            }
            return result;
        }
    }

}
