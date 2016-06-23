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
        private string[] colName;

        public DataTable dealCom(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            string name = com_ele[3];
            List<string[]> views = new BaseConfig().getViews(BaseCommand.getCurrentDataBase());  //获取当前数据库所有视图
            for (int i=0;i<views.Count;i++)
                if (views[i][0].Equals(name))
                    return dealView(comstr,views[i][1]);
            return dealTable(comstr);
        }

        /// <summary>
        /// 处理视图查询
        /// </summary>
        /// <param name="com_view"></param>
        /// <param name="comstr"></param>
        /// <returns></returns>
        private DataTable dealView(string com_view,string comstr)
        {
            DataTable view_table = dealTable(comstr);
            return changeTable(com_view, view_table);
        }

        /// <summary>
        /// 处理表查询
        /// </summary>
        /// <param name="comstr"></param>
        /// <returns></returns>
        private DataTable dealTable(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            string table_name = com_ele[3];
            //权限检测
            bool hasPer = (new JudgePermission()).hasPermission(
                BaseCommand.getCurrentUser(), table_name, "insert");
            if (!hasPer)
                return new DataTable();

            DataTable data_table = baseCom.getAllRecord(table_name);

            return changeTable(comstr, data_table);
        }

        /// <summary>
        /// 根据指令处理table
        /// </summary>
        /// <param name="comstr"></param>
        /// <returns></returns>
        private DataTable changeTable(string comstr,DataTable result)
        {
            string[] com_ele = comstr.Split(' ');
            string table_name = com_ele[3];
            string[] col_name = com_ele[1].Split(',');
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
            if (col_name[0].Equals("*"))
            {
                return result;
            }
            else
            {
                List<string> all_col = new List<string>();
                for (int i=0;i<result.Columns.Count;i++)
                {
                    all_col.Add(result.Columns[i].ColumnName);
                }
                List<string> real_col = new List<string>(col_name);
                List<string> yu_col = all_col.Except(real_col).ToList();    //求差集

                for (int i = 0; i < yu_col.Count; i++)
                {
                    result.Columns.Remove(result.Columns[yu_col[i]]);   //删除多于列
                }
            }
            return result;
        }
    }
}
