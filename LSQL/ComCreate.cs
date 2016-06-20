using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    /// <summary>
    /// create 指令操作类
    /// </summary>
    class ComCreate
    {
        private BaseCommand baseCom;    //基本操作指令类
        private class Creater
        {
            public string module = "";
            public string module_name = "";
            public List<string> col_name;
            public List<string> col_type;
            public List<string> col_constraint;
        }   //创建者数据类
        private Creater dataCreator = new Creater();

        public ComCreate()
        {
            baseCom = new BaseCommand();
        }
        public string dealCom(string comstr)
        {
            string[] ele = comstr.Split(' ');
            dataCreator.module = ele[1];
            dataCreator.module_name = ele[2];
            string str_dict = DataUtil.getStringFromBracket(comstr);//正则匹配出括号内的内容
            string[] str_col = str_dict.Split(',');
            for(int i=0;i<str_col.Length;i++)
            {
                string[] str = { "", "", "" };
                string[] str_ele = str_col[i].Split(' ');
                for (int j = 0; j < str_ele.Length; j++)
                    str[j] = str_ele[j];
                dataCreator.col_name.Add(str[0]);
                dataCreator.col_type.Add(str[1]);
                dataCreator.col_constraint.Add(str[2]);
            }
            //
            //
            //
            return "success";
        }
        public string createTable(string name,List<string> dict)
        {
            baseCom.createTable(name);  //创建空表

            return "success";
        }
    }
}
