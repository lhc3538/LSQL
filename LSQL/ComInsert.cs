using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class ComInsert
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        private DataDict dataDict;  //数据字典解析类
        private string tableName = "";  //表名
        private string lineStr = "";    //一行数据
        private string[] colEle;    //每个列数据
        public string dealCom(string cmdstr)
        {
            string[] ele = cmdstr.Split(' ');
            tableName = ele[2];
            lineStr = DataUtil.getStringFromBracket(cmdstr);
            colEle = lineStr.Split(',');
            //权限检测
            bool hasPer = (new JudgePermission()).hasPermission(
                baseCom.getCurrentUser(), tableName, "insert");
            if (!hasPer)
                return "Permission Denied";

            //载入相应的数据字典
            dataDict = new DataDict();
            dataDict.init(tableName);
            //数据类型对应检测
            if (colEle.Length != dataDict.colName.Count)
                return "数据数量错误";
            for (int i=0;i<colEle.Length;i++)   //判断类型
            {
                if (dataDict.colType[i].Equals("int"))
                {
                    if (!DataUtil.isInt(colEle[i]))
                        return colEle[i] + "非int";
                }
                else if (dataDict.colType[i].Equals("double"))
                {
                    if (!DataUtil.isDouble(colEle[i]))
                        return colEle[i] + "非double";
                }
            }
            //约束检测
            for (int i = 0; i < colEle.Length; i++)   //列循环
            {
                if (dataDict.colConst[i].Equals("notnull")) //非空约束
                {
                    if (colEle[i] == "")
                        return "数据要求不为空";
                }
                else if (dataDict.colConst[i].Equals("primary"))    //主键约束
                {
                    if (colEle[i] == "")
                        return "数据要求不为空";
                    List<string> coldata = baseCom.readCol(tableName, i);
                    for (int j = 0; j < coldata.Count; j++)
                    {
                        if (coldata[j].Equals(colEle[i]))
                            return "不满足唯一性约束";
                    }
                }
            }
            return baseCom.insertRecord(tableName, colEle); ;
        }
    }
}
