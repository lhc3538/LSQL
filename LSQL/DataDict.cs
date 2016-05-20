using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class DataDict
    {
        private BaseCommand basecom;
        public DataDict()
        {
            basecom = new BaseCommand();
            basecom.useDatabase(".Config");
        }
        public string initDict()
        {
            string result;
            result = basecom.createDataBase(".Config");
            if (result != "success")
                return result;
            //string[] tables = basecom.getDataBases();
            return "success";
        }
    }
}
