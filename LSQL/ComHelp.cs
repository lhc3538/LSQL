using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class ComHelp
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        private FileIO fileIO = new FileIO();

        public string dealCom(string comstr)
        {
            string[] com_ele = comstr.Split(' ');
            string name = com_ele[2];
            if (com_ele[1].Equals("database"))
            {
                if (baseCom.hadExistedDatabase(name))
                {
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(
                        BaseCommand.getHomePath() + name);
                    DateTime DT = dir.CreationTime;//获取目录或者文件的创建 日期
                    return DT.ToString();
                }
                else
                    return "数据库不存在";
            }
            else if (com_ele[1].Equals("table"))
            {
                if (baseCom.hadExistedTable(name))
                {
                    System.IO.DirectoryInfo file = new System.IO.DirectoryInfo(
                        BaseCommand.getHomePath() + BaseCommand.getCurrentDataBase() + @"/" + name);
                    DateTime DT = file.CreationTime;//获取目录或者文件的创建 日期
                    return DT.ToString() +"\r\n"+ baseCom.getHeadData(name);
                }
                else
                    return "数据表不存在";
            }
            else if (com_ele[1].Equals("view"))
            {
                if (baseCom.hadExistedView(name))
                {
                    return name;
                }
                else
                    return "数据表不存在";
            }
            return "无法解析";
        }
    }
}
