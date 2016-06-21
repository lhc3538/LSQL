using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LSQL
{
    class DataUtil
    {
        /// <summary>
        /// 删除字符串数组某元素
        /// </summary>
        /// <param name="array">字符串数组</param>
        /// <param name="index">下标</param>
        /// <returns></returns>
        public static string[] removeStringArrayElement(string[] array, int index)
        {
            int length = array.Length;
            string[] result = new string[length - 1];
            Array.Copy(array, result, index);
            Array.Copy(array, index + 1, result, index, length - index - 1);
            return result;
        }

        /// <summary>
        /// 合并字符串数组，中间以字符隔开
        /// </summary>
        /// <param name="array">字符串数组</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string combineStringArrayInsertChar(string[] array,char separator)
        {
            string result = "";
            for (int i=0;i<array.Length; i++)
            {
                if (i == 0)
                    result = array[i];
                else
                    result += (separator + array[i]);
            }
            return result;
        }

        /// <summary>
        /// 提取括号中的内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getStringFromBracket(string str)
        {
            string pattern = @"\(.*?\)";//匹配模式
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(str);
            StringBuilder sb = new StringBuilder();//存放匹配结果
            foreach (Match match in matches)
            {
                string value = match.Value.Trim('(', ')');
                sb.AppendLine(value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 判断字符串是否为int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isInt(string str)
        {
            bool isnum = Regex.IsMatch(str, @"^\d+$");
            return isnum;
        }

        public static bool isDouble(string str)
        {
            double temp;
            bool isdouble = Double.TryParse(str,out temp);
            return isdouble;
        }
    }
}
