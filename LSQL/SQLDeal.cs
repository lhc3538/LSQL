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
                string[] key = { "database", "table", "view" };
                List<string> secondkey = new List<string>(key);
                if (secondkey.IndexOf(CmdStr[1]) == -1)
                    return CmdStr[1] + "无法解析，您是不是要输入database/table/view";
                if (CmdStr.Length != 3)
                    return "请输入对象名";
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
                else
                {
                    return CmdStr[1] + "无法解析，您是不是要输入database/table";
                }
            }
            else if (CmdStr[0] == "insert")
            {
                if (CmdStr[1] != "into")
                    return "您是不是要输入insert into ...";
                return comInsert.dealCom(str);
            }
            else if (CmdStr[0] == "alter")
            {
                if (CmdStr[1] != "table")
                    return "您是不是要输入alter table ...";
                return comAlter.dealCom(str);
            }
            else if (CmdStr[0] == "delete")
            {
                if (CmdStr[1] != "from")
                    return "您是不是要输入delete from ...";
                if (CmdStr[3] != "where")
                    return "字段不完整";
                return comDelete.dealCom(str);
            }
            else if (CmdStr[0] == "update")
            {
                if (CmdStr[2] != "set")
                    return "字段不完整";
                if (CmdStr[6] != "where")
                    return "字段不完整";
                return comUpdate.dealCom(str);
            }
            else if (CmdStr[0] == "grant")
            {
                if (CmdStr[2] != "on")
                    return "字段不完整";
                if (CmdStr[4] != "to")
                    return "字段不完整";
                return comGrant.dealCom(str);
            }
            return "Unable to identify";
        }
    }

}
