using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class ComSelect
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        private string tableName = "";
        private string[] colName;

        public List<string[]> dealCom(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            tableName = com_ele[3];
            colName = com_ele[1].Split(',');
            if (colName[0].Equals("*"))
            {
                return baseCom.getAllRecord(tableName);
            }
            return new List<string[]>();
        }
    }
}
