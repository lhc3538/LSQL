using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class ComAlter
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        private string tableName = "";  //操作表名
        private string operateType = "";  //操作
        private string colNmae = "";    //列名
        private string colType = "";    //列数据类型

        public string dealCom(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            tableName = com_ele[2];
            operateType = com_ele[3];
            colNmae = com_ele[4];
            if (operateType.Equals("add"))
            {
                colType = com_ele[5];
                return baseCom.addCol(tableName, colNmae + " " + colType);
            }
            else if (operateType.Equals("drop"))
            {
                return baseCom.delCol(tableName, colNmae);
            }
            return "Unable to identify";
        }
    }
}
