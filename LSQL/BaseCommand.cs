using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    class BaseCommand
    {
        private string homePath;  //DBMS根目录
        private FileIO fileIO;  //具体数据库操作类
        private static string currentDataBase = "";   //当前数据库
        public static char colSeparator = (char)2;    //列间分隔符

        public BaseCommand()
        {
            fileIO = new FileIO();
            homePath = @"c:\LSQL\";
        }

        /// <summary>
        /// 切换当前数据库
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns>切换结果</returns>
        public string useDatabase(string name)
        {
            if (hadExistedDatabase(name))
            {
                currentDataBase = name;
                return "select success";
            }
            return "Database not exist";
        }

        /// <summary>
        /// 判断数据库是否存在
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns>判断结果</returns>
        private bool hadExistedDatabase(string name)
        {
            string[] allDatabase = fileIO.getAllFolder(homePath);
            for (int i=0;i<allDatabase.Length;i++)
                if (allDatabase[i] == name)
                    return true;
            return false;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns>创建结果</returns>
        public string createDataBase(string name)
        {
            string result_database = fileIO.createFolder(homePath + name);  //创建数据库
            string result_dict = fileIO.createFile(homePath + "." + name);  //创建数据库对应的数据字典
            if (result_database.Equals("success") && result_dict.Equals("success"))
                return "success";
            else
                return result_database + "\r\n" + result_dict;
        }

        /// <summary>
        /// 显示所有数据库
        /// </summary>
        /// <returns>所有数据库名，换行隔开</returns>
        public string showDataBases()
        {
            return formatStringArray(getDataBases().ToArray());
        }

        /// <summary>
        /// 获取所有数据库
        /// </summary>
        /// <returns></returns>
        public List<string> getDataBases()
        {
            List<string> databases = new List<string>();
            string[] folders = fileIO.getAllFolder(homePath);
            for (int i=0;i<folders.Length;i++)
                if (folders[i].ElementAt(0) != '.')
                    databases.Add(folders[i]);
            return databases;
        }

        /// <summary>
        /// 在当前数据库中创建表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>创建结果</returns>
        public string createTable(string tableName)
        {
            if (currentDataBase == "")
                return "Please select database";
            else if (tableName == "")
                return "Please input table name";
            else
            {
                string result_database = fileIO.createFile(homePath + currentDataBase + @"\" + tableName);  //创建数据表
                string result_dict = fileIO.createFile(homePath + currentDataBase + @"\." + tableName); //创建数据表对应的数据字典
                if (result_database.Equals("success") && result_dict.Equals("success"))
                    return "success";
                else
                    return result_database + "\r\n" + result_dict;
            }
        }

        /// <summary>
        /// 显示当前数据库所有数据表
        /// </summary>
        /// <returns>所有表名，换行隔开</returns>
        public string showTables()
        {
            if (currentDataBase!="")
                return formatStringArray(fileIO.getAllFile(homePath + currentDataBase));
            return "Please select database";
        }

        /// <summary>
        /// 显示当前数据库所有数据表
        /// </summary>
        /// <returns>表名列表</returns>
        public List<string> getTables()
        {
            string[] files = fileIO.getAllFile(homePath + currentDataBase);
            List<string> list = new List<string>();
            foreach(string name in files)
            {
                list.Add(name);
            }
            return list;
        }

        /// <summary>
        /// 格式化字符串数组
        /// 每个字符串之间添加换行
        /// </summary>
        /// <param name="strArr">字符串数组</param>
        /// <returns>格式化后的字符串</returns>
        protected string formatStringArray(string[] strArr)
        {
            string result = "";
            for (int i = 0; i < strArr.Length; i++)
            {
                if (i == 0)
                    result += strArr[i];
                else
                    result += "\r\n" + strArr[i];
            }
            return result;
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <returns></returns>
        public string dropDatabase(string name)
        {
            if (name != "")
            {
                string rul_database = fileIO.deleteDirectory(homePath + name);
                string rul_dict = fileIO.deleteFile(homePath + "." + name);
                if (rul_database.Equals("success") && rul_dict.Equals("success"))
                    return "success";
                else
                    return rul_database + "\r\n" + rul_dict;
            }
            return "Please input dabases name";
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string dropTable(string name)
        {
            if (name == "")
                return "Please input table name";
            else if (currentDataBase == "")
                return "Please select database";
            else
            {
                string rul_database = fileIO.deleteFile(homePath + currentDataBase + @"\" + name);
                string rul_dict = fileIO.deleteFile(homePath + currentDataBase + @"\." + name);
                if (rul_database.Equals("success") && rul_dict.Equals("success"))
                    return "success";
                else
                    return rul_database + "\r\n" + rul_dict;
            }
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="table"></param>
        /// <param name="strline"></param>
        /// <returns></returns>
        public string insertRecord(string table,string strline)
        {
            return fileIO.appendLine(strline, homePath + currentDataBase + @"\" + table);
        }
        public string insertRecord(string table,string[] str_ele)
        {
            string allstr = "";
            for (int i=0;i<str_ele.Length;i++)
            {
                if (i == 0)
                    allstr = str_ele[0];
                else
                    allstr += (colSeparator + str_ele[i]);
            }
            return fileIO.appendLine(allstr, homePath + currentDataBase + @"\" + table);
        }

        /// <summary>
        /// 显示所有记录
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>格式化后的所有记录</returns>
        public string showRecords(string table)
        {
            string records = "";
            List<string> list = fileIO.readAllLine(homePath + currentDataBase + @"\" + table);
            foreach(string line in list)
            {
                records += line + "\r\n";
            }
            return records;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="id">行号，从0开始</param>
        /// <returns>操作结果</returns>
        public string delRecord(string table,int id)
        {
            return fileIO.delLine(homePath + currentDataBase + @"\" + table, id);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="id">行号，从0开始</param>
        /// <param name="linestr">一条数据</param>
        /// <returns>操作结果</returns>
        public string modifyRecord(string table,int id,string linestr)
        {
            return fileIO.modifyLine(homePath + currentDataBase + @"\" + table, id, linestr);
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="col_name">列名</param>
        /// <param name="type">数据类型</param>
        /// <param name="constraint">约束</param>
        /// <returns></returns>
        public string addCol(string table_name,string dict)
        {
            List<string> lines = fileIO.readAllLine(homePath + currentDataBase + @"\" + table_name);
            //添加属性
            if (lines.Count == 0)   //文件为空
            {
                lines.Add(dict);
            }
            else//文件非空
            {
                if (lines[0]!="")
                    lines[0] += (colSeparator + dict);
                for (int i=1;i<lines.Count;i++)
                {
                    if (lines[i]!="")
                    lines[i] += colSeparator;   //在实际表中添加一空列
                }      
            }
            fileIO.writeAllLine(lines, homePath + currentDataBase + @"\" + table_name);
            return "success";
        }

        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="col_name">列名</param>
        /// <returns></returns>
        public string delCol(string table_name,string col_name)
        {
            int id = -1;    //列的id
            //删除表头的属性，并找到列id
            string str_head = getHeadData(table_name);
            string[] str_col = str_head.Split(colSeparator);
            for (int i=0;i<str_col.Length;i++)
            {
                string[] element = str_col[i].Split(' ');
                if (element[0].Equals(col_name))
                {
                    id = i;
                    break;
                }
            }
            
            if (id == -1)
                return "Col name not found";
            //删除表中的对应列
            List<string> lines = fileIO.readAllLine(homePath + currentDataBase + @"\" + table_name);
            for (int i = 0; i < lines.Count; i++)
            {
                string[] element = lines[i].Split(colSeparator);
                element = DataUtil.removeStringArrayElement(element, id);   //删除对应列
                lines[i] = DataUtil.combineStringArrayInsertChar(element, colSeparator);    //合并删除后的数组，保存
            }
            fileIO.writeAllLine(lines, homePath + currentDataBase + @"\" + table_name);
            return "success";
        }

        /// <summary>
        /// 找到某值所在位置
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="col_name">列名</param>
        /// <param name="col_value">列值</param>
        /// <returns></returns>
        public int findPos(string table_name,string col_name,string col_value)
        {
            string[] head_ele = getHeadData(table_name).Split(colSeparator);
            int col_id = -1;
            for (int i=0;i<head_ele.Length;i++)
            {
                string[] ele = head_ele[i].Split(' ');
                if (ele[0] == col_name)
                {
                    col_id = i;
                    break;
                }
            }
            if (col_id == -1)
                return -1;
            List<string> all_value = readCol(table_name, col_id);
            return all_value.IndexOf(col_value);
        }

        /// <summary>
        /// 读取一列
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="col_id">列号</param>
        /// <returns></returns>
        public List<string> readCol(string table_name,int col_id)
        {
            List<string> lines = fileIO.readAllLine(homePath + currentDataBase + @"\" + table_name);
            List<string> result = new List<string>();
            for (int i=0;i<lines.Count;i++)
            {
                string[] ele = lines[i].Split(colSeparator);
                result.Add(ele[col_id]);
            }
            return result;
        }

        /// <summary>
        /// 获取表头
        /// </summary>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public string getHeadData(string table_name)
        {
            return fileIO.readLine(homePath + currentDataBase + @"\" + table_name, 0);
        }
    }
}
