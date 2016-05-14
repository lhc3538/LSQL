using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class SQLDeal
    {
        private BaseCommand baseCommand;
        public SQLDeal()
        {
            baseCommand = new BaseCommand();
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
                    return baseCommand.createDataBase(CmdStr[2]);
                }
                else if (CmdStr[1] == "table")
                {
                    return baseCommand.createTable(CmdStr[2]);
                }
            }
            else if (CmdStr[0] == "show")
            {
                if (CmdStr[1] == "databases")
                {
                    return baseCommand.showDataBases();
                }
                else if(CmdStr[1] == "tables")
                {
                    return baseCommand.showTables();
                }
            }
            else if (CmdStr[0] == "use")
            {
                return baseCommand.useDatabase(CmdStr[1]);
            }
            return "Unable to identify";
        }
    }

}
