using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class SQLDeal
    {
        private BaseCommand baseCommand;
        private ComCreate comCreate;
        private ComAlter comAlter;
        private ComInsert comInsert;
        private ComDelete comDelete;
        private ComSelect comSelect;
        private ComUpdate comUpdate;
        private ComGrant comGrant;
        private ComHelp comHelp;

        public SQLDeal()
        {
            baseCommand = new BaseCommand();
            comCreate = new ComCreate();
            comAlter = new ComAlter();
            comInsert = new ComInsert();
            comDelete = new ComDelete();
            comSelect = new ComSelect();
            comUpdate = new ComUpdate();
            comGrant = new ComGrant();
            comHelp = new ComHelp();
        }
        /// <summary>
        /// 查询语句单独处理
        /// </summary>
        /// <param name="comstr"></param>
        /// <returns></returns>
        public DataTable dealSelect(string comstr)
        {
            return comSelect.dealCom(comstr);
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
                string[] key = { "database", "table", "view" };
                List<string> secondkey = new List<string>(key);
                if (secondkey.IndexOf(CmdStr[1]) == -1)
                    return CmdStr[1] + "无法解析，您是不是要输入database/table/view";

                return comCreate.dealCom(str);
            }
            else if (CmdStr[0] == "show")
            {
                string[] key = { "databases", "tables", "views" };
                List<string> secondkey = new List<string>(key);
                if (secondkey.IndexOf(CmdStr[1]) == -1)
                    return CmdStr[1] + "无法解析，您是不是要输入databases/tables/views";

                if (CmdStr[1] == "databases")
                {
                    return baseCommand.showDataBases();
                }
                else if(CmdStr[1] == "tables")
                {
                    return baseCommand.showTables();
                }
                else if (CmdStr[1] == "views")
                {
                    return baseCommand.showViews();
                }
            }
            else if (CmdStr[0] == "help")
            {
                return comHelp.dealCom(str);
            }
            else if (CmdStr[0] == "use")
            {
                return baseCommand.useDatabase(CmdStr[1]);
            }
            else if (CmdStr[0] == "drop")
            {
                if (CmdStr[1] == "database")
                {
                    return baseCommand.dropDatabase(CmdStr[2]);
                }
                else if (CmdStr[1] == "table")
                {
                    return baseCommand.dropTable(CmdStr[2]);
                }
            }
            else if (CmdStr[0] == "insert")
            {
                return comInsert.dealCom(str);
            }
            else if (CmdStr[0] == "alter")
            {
                return comAlter.dealCom(str);
            }
            else if (CmdStr[0] == "delete")
            {
                return comDelete.dealCom(str);
            }
            else if (CmdStr[0] == "update")
            {
                return comUpdate.dealCom(str);
            }
            else if (CmdStr[0] == "grant")
            {
                return comGrant.dealCom(str);
            }
            return "Unable to identify";
        }
    }

}
