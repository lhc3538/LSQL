using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class SQLDeal
    {
        private FileIO fileIO;
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
                    result = fileIO.createDataBase(CmdStr[2]);
                    return result;
                }
            }
            return "Unable to identify";
        }
    }

}
