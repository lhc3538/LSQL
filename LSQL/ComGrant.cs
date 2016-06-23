using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class ComGrant
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        private string tableName = "";
        private string user = "";
        private string limits = "";

        public string dealCom(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            tableName = com_ele[3];
            user = com_ele[5];
            limits = com_ele[1];
            return baseCom.insertRecord("." + tableName, user + BaseCommand.colSeparator + limits);
        }
    }
}
