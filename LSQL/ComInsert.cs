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
            
            dataDict = new DataDict();
            dataDict.init(tableName);
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
            return baseCom.insertRecord(tableName, colEle); ;
        }

     
    }
}
