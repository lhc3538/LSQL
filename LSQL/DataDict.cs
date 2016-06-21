using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class DataDict
    {
        private BaseCommand baseCommand;
        public string dataStr; //表整体字典
        public List<string> colName = new List<string>();   //列名集合
        public List<string> colType = new List<string>();   //列数据类型
        public List<string> colConst = new List<string>();  //列约束


        public void init(string table_name)
        {
            baseCommand = new BaseCommand();
            dataStr = baseCommand.getHeadData(table_name);
            string[] line_ele = dataStr.Split(BaseCommand.colSeparator);
            for (int i = 0; i < line_ele.Length; i++)
            {
                colName.Add("");
                colType.Add("");
                colConst.Add("");
                string[] col_ele = line_ele[i].Split(' ');
                for (int j = 0; j < col_ele.Length; j++)
                {
                    if (j == 0)
                        colName[i] = col_ele[0];
                    else if (j == 1)
                        colType[i] = col_ele[1];
                    else if (j == 2)
                        colConst[i] = col_ele[2];
                }
            }
        }
    }
}
