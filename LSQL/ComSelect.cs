using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable dealCom(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            tableName = com_ele[3];
            //权限检测
            bool hasPer = (new JudgePermission()).hasPermission(
                BaseCommand.getCurrentUser(), tableName, "insert");
            if (!hasPer)
                return new DataTable();

            DataTable result = baseCom.getAllRecord(tableName);
            colName = com_ele[1].Split(',');
            if (com_ele.Length > 7)   //有where
            {
                string name = com_ele[5];
                string value = com_ele[7];
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (!result.Rows[i][name].Equals(value))
                    {
                        result.Rows[i].Delete();
                        i--;
                    }
                }
            }
            if (colName[0].Equals("*"))
            {
                return result;
            }
            else
            {
                List<string> all_col = baseCom.getColName(tableName);
                List<string> real_col = new List<string>(colName);
                List<string> yu_col = all_col.Except(real_col).ToList();    //求差集
                
                for (int i=0;i<yu_col.Count;i++)
                {
                    result.Columns.Remove(result.Columns[yu_col[i]]);   //删除多于列
                }
            }
            return result;
        }
    }
}
