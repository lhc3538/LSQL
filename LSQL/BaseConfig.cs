using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class BaseConfig
    {
        private static string homePath = BaseCommand.getHomePath();  //DBMS根目录
        FileIO fileIO = new FileIO();
        public List<string[]> getViews(string database_name)
        {
            List<string[]> result = new List<string[]>();
            List<string> lines = fileIO.readAllLine(homePath + "." + database_name);
            for (int i=1;i<lines.Count;i++)
            {
                result.Add(lines[i].Split(BaseCommand.colSeparator));
            }
            return result;
        }
    }
}
