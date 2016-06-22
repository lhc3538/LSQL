using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class ComDelete
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        private string tableName = "";
        private string colName = "";
        private string colValue = "";

        public string dealCom(string comstr)
        {
            string[] ele = comstr.Split(' ');
            tableName = ele[2];
            colName = ele[4];
            colValue = ele[6];
            while(true)
            {
                int pos = baseCom.findPos(tableName, colName, colValue);
                if (pos == -1)
                    break;
                baseCom.delRecord(tableName, pos);
            }
            return "success";
        }
    }
}
