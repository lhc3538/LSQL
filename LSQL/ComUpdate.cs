using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class ComUpdate
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        private DataDict dataDict;  //数据字典解析类
        private string tableName = "";

        public string dealCom(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            tableName = com_ele[1];
            string col_name = com_ele[7];
            string value = com_ele[9];
            string ncol_name = com_ele[3];
            string nvalue = com_ele[5];
            //权限检测
            bool hasPer = (new JudgePermission()).hasPermission(
                baseCom.getCurrentUser(), tableName, "update");
            if (!hasPer)
                return "Permission Denied";

            //载入相应的数据字典
            dataDict = new DataDict();
            dataDict.init(tableName);

            DataTable allrecord;
            allrecord = baseCom.getAllRecord(tableName);
            for (int l=0;l<allrecord.Rows.Count;l++)
            {
                if (allrecord.Rows[l][col_name].Equals(value))
                {
                    //约束检测
                    //判断类型
                    int i = allrecord.Columns.IndexOf(ncol_name);
                    if (dataDict.colType[i].Equals("int"))
                    {
                        if (!DataUtil.isInt(nvalue))
                            return nvalue + "非int";
                    }
                    else if (dataDict.colType[i].Equals("double"))
                    {
                        if (!DataUtil.isDouble(nvalue))
                            return nvalue + "非double";
                    }
                    //约束检测
                    if (dataDict.colConst[i].Equals("notnull")) //非空约束
                    {
                        if (nvalue == "")
                            return "数据要求不为空";
                    }
                    else if (dataDict.colConst[i].Equals("primary"))    //主键约束
                    {
                        if (nvalue == "")
                            return "数据要求不为空";
                        List<string> coldata = baseCom.readCol(tableName, i);
                        for (int j = 0; j < coldata.Count; j++)
                        {
                            if (coldata[j].Equals(nvalue))
                                return "不满足唯一性约束";
                        }
                    }
                    //----
                    allrecord.Rows[l][ncol_name] = nvalue;
                }
            }
            return baseCom.setAllRecord(tableName, allrecord);
        }
    }
}
