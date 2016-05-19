using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSQL
{
    /// <summary>
    /// 以c:\LSQL\为根目录进行文件操作
    /// </summary>
    class FileIO
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="name">文件夹路径</param>
        /// <returns></returns>
        public string createFolder(string path)
        {
            if (path != "")
            {
                try
                {
                    // Determine whether the directory exists.
                    if (!Directory.Exists(path))
                    {
                        // Create the directory it does not exist.
                        Directory.CreateDirectory(path);
                        return "success";
                    }
                    else
                        return "error:had existed";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            return "error:empty name";
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">创建位置</param>
        /// <returns>创建结果</returns>
        public string createFile(string path)
        {
            if (path!="")
            {
                try
                {
                    // Determine whether the fiel exists.
                    if (!File.Exists(path))
                    {
                        // Create the file it does not exist.
                        File.Create(path);
                        return "success";
                    }
                    else
                        return "error:had existed";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            return "error:empty name";
        }

        /// <summary>
        /// 获取指定目录下所有文件夹名
        /// </summary>
        /// <returns>文件夹名数组</returns>
        public string[] getAllFolder(string path)
        {
            return getAllName(Directory.GetDirectories(path));
        }

        /// <summary>
        /// 获取指定目录下所有文件
        /// </summary>
        /// <param name="path">指定目录</param>
        /// <returns>文件名数组</returns>
        public string[] getAllFile(string path)
        {
            return getAllName(Directory.GetFiles(path));
        }

        /// <summary>
        /// 从路径数组中提取最后一项重新组成数组
        /// </summary>
        /// <param name="allPath">路径数组</param>
        /// <returns>最后一项的数组</returns>
        private string[] getAllName(string[] allPath)
        {
            string result = "";
            for (int i = 0; i < allPath.Length; i++)
            {
                string[] name = allPath[i].Split('\\');   //提取最后面的数据库名
                if (i == 0)
                    result += name[name.Length - 1];
                else
                    result += "|" + name[name.Length - 1];
            }
            return result.Split('|');
        }
        /// <summary>
        /// 删除非空文件夹
        /// </summary>
        /// <param name="path">要删除的文件夹目录</param>
        public string deleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] childs = dir.GetDirectories();
                foreach (DirectoryInfo child in childs)
                {
                    child.Delete(true);
                }
                dir.Delete(true);
                return "Delete success";
            }
            else
                return "not exist";
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>删除结果</returns>
        public string deleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return "Delete succcess";
            }
            return "not exist";
        }

        /// <summary>
        /// 读取所有行
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>所有行列表</returns>
        public List<string> readAllLine(string path)
        {
            return new List<string>(File.ReadLines(path));
        }

        /// <summary>
        /// 文件尾追加行
        /// </summary>
        /// <param name="strline">追加内容</param>
        /// <param name="path">文件路径</param>
        /// <returns>操作结果</returns>
        public string appendLine(string strline,string path)
        {
            FileStream fs = null;
            //将待写的入数据从字符串转换为字节数组  
            Encoding encoder = Encoding.UTF8;
            byte[] bytes = encoder.GetBytes(strline+ "\r\n");
            try
            {
                fs = File.OpenWrite(path);
                //设定书写的开始位置为文件的末尾  
                fs.Position = fs.Length;
                //将待写入内容追加到文件末尾  
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                fs.Close();
            }
            return "success";
        }

        /// <summary>
        /// 在文件尾追加多行
        /// </summary>
        /// <param name="strlist">字符串列表</param>
        /// <param name="path">文件路径</param>
        /// <returns>操作结果</returns>
        public string appendAllLine(List<string>strlist,string path)
        {
            FileStream fs = null;
            //将待写的入数据从字符串转换为字节数组  
            Encoding encoder = Encoding.UTF8;
            fs = File.OpenWrite(path);
            try
            {
                foreach (string line in strlist)
                {
                    byte[] bytes = encoder.GetBytes(line + "\r\n");
                    //设定书写的开始位置为文件的末尾  
                    fs.Position = fs.Length;
                    //将待写入内容追加到文件末尾  
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                fs.Close();
            }
            return "success";
        }
        /// <summary>
        /// 从头重写文件
        /// </summary>
        /// <param name="strlist">数据列表</param>
        /// <param name="path">文件路径</param>
        /// <returns>操作结果</returns>
        public string writeAllLine(List<string> strlist, string path)
        {
            FileStream fs = null;
            //将待写的入数据从字符串转换为字节数组  
            Encoding encoder = Encoding.UTF8;
            fs = File.OpenWrite(path);
            try
            {
                foreach (string line in strlist)
                {
                    byte[] bytes = encoder.GetBytes(line + "\r\n");
                    //将待写入内容追加到文件末尾  
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                fs.Close();
            }
            return "success";
        }
        /// <summary>
        /// 删除文件某一行
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="id">行号，从0开始</param>
        /// <returns>操作结果</returns>
        public string delLine(string path,int id)
        {
            List<string> lines = readAllLine(path);
            lines.RemoveAt(id);
            return writeAllLine(lines, path);
        }

        /// <summary>
        /// 修改某一行
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="id">行号，从0开始</param>
        /// <param name="linestr">一行数据</param>
        /// <returns>操作结果</returns>
        public string modifyLine(string path,int id,string linestr)
        {
            List<string> lines = readAllLine(path);
            lines[id] = linestr;
            return writeAllLine(lines, path);
        }
    }
}
