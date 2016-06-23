using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class JudgePermission
    {
        private BaseCommand baseCom = new BaseCommand();    //基本操作指令类
        //private ComSelect comSelect = new ComSelect();
        private string tableName = "";

        public bool hasPermission(string user,string table_name,string operation)
        {
            //alldata[i][0]里存了user，alldata[i][1]里存了permission(select,update,insert,delete)
            List<string[]> alldata = baseCom.readAllRecords("." + table_name);
            for (int i=0;i<alldata.Count;i++)
            {
                if (alldata[i][0].Equals(user))
                {
                    string[] limits = alldata[i][1].Split(',');
                    for (int j=0;j<limits.Length;j++)
                    {
                        if (operation.Equals(limits[j]))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
