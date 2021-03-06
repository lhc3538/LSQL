﻿using System;
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
        private FileIO fileIO = new FileIO();
        private class Creater
        {
            public string module = "";  //数据库还是数据表
            public string module_name = ""; //名字
            public string dict; //后缀要求
        }   //创建者数据类
        private Creater dataCreator = new Creater();

        public ComCreate()
        {
            baseCom = new BaseCommand();
        }
        /// <summary>
        /// 处理创建类请求
        /// </summary>
        /// <param name="comstr">原是指令</param>
        /// <returns></returns>
        public string dealCom(string comstr)
        {
            string[] ele = comstr.Split(' ');
            dataCreator.module = ele[1];
            dataCreator.module_name = ele[2];
            //for(int i=0;i<str_col.length;i++)
            //{
            //    string[] str = { "", "", "" };
            //    string[] str_ele = str_col[i].split(' ');
            //    for (int j = 0; j < str_ele.length; j++)
            //        str[j] = str_ele[j];
            //    datacreator.col_name.add(str[0]);
            //    datacreator.col_type.add(str[1]);
            //    datacreator.col_constraint.add(str[2]);
            //}
            if (dataCreator.module.Equals("database"))  //创建数据库
            {
                return baseCom.createDataBase(dataCreator.module_name);
            }
            else if (dataCreator.module.Equals("table"))    //创建数据表
            {
                dataCreator.dict = DataUtil.getStringFromBracket(comstr);//正则匹配出括号内的内容
                return createTable(dataCreator.module_name,dataCreator.dict);
            }
            else if (dataCreator.module.Equals("view")) //创建视图
            {
                string[] str_select = DataUtil.splitString(comstr, "as ");   //按照as拆分
                return createView(dataCreator.module_name, str_select[1]);
            }
            return "success";
        }
        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public string createView(string view_name,string str_select)
        {
            string line = view_name + BaseCommand.colSeparator + str_select;
            string path = BaseCommand.getHomePath() + "." + BaseCommand.getCurrentDataBase();
            fileIO.appendLine(line, path);
            return "success";
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="dict">附加要求，默认列等</param>
        /// <returns></returns>
        public string createTable(string name,string dict)
        {
            string rult = baseCom.createTable(name);  //创建空表
            //授予当前用户操作权限
            new ComGrant().dealCom("grant select,update,insert,delete on " + name + " to " + BaseCommand.getCurrentUser());
            if (rult != "success")
                return rult;
            if (dict != "") //创建初始要求列
            {
                string[] str_col = dict.Split(',');
                for (int i=0;i<str_col.Length;i++)
                {
                    baseCom.addCol(name, str_col[i]);
                }
            }
            return "success";
        }
    }
}
