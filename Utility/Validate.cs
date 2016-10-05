using System.Text.RegularExpressions;
using System.Globalization;

namespace UnileverCAS.UnileverFun
{
    /// <summary>
    /// 操作正则表达式的公共类
    /// </summary>    
    public sealed class RegexHelper
    {
        /// <summary>
        /// 清除包含'字符串
        /// </summary>
        public const string CLEAN_STRING = @"[']";

        /// <summary>
        /// 验证字符串是否为字符begin-end之间
        /// </summary>
        public const string IS_VALID_BYTE = @"^[A-Za-z0-9]{#0#,#1#}$";

        /// <summary>
        /// 验证字符串是否为年月日
        /// </summary>
        public const string IS_VALID_DATE =
            @"^2\d{3}-(?:0?[1-9]|1[0-2])-(?:0?[1-9]|[1-2]\d|3[0-1])(?:0?[1-9]|1\d|2[0-3]):(?:0?[1-9]|[1-5]\d):(?:0?[1-9]|[1-5]\d)$";

        /// <summary>
        /// 验证字符串是否为小数
        /// </summary>
        public const string IS_VALID_DECIMAL = @"[0].\d{1,2}|[1]";

        /// <summary>
        /// 验证字符串是否为EMAIL
        /// </summary>
        public const string IS_VALID_EMAIL =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// 验证字符串是否为IP
        /// </summary>
        public const string IS_VALID_IP =
            @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";

        /// <summary>
        /// 验证字符串是否为后缀名
        /// </summary>
        public const string IS_VALID_POSTFIX = @"\.(?i:{0})$";

        /// <summary>
        /// 验证字符串是否为电话号码
        /// </summary>
        public const string IS_VALID_TEL = @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?";

        /// <summary>
        /// 验证字符串是否为URL
        /// </summary>
        public const string IS_VALID_URL = @"^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$";

        #region 替换字符串
        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>替换后字符串</returns>
        public static string ReplaceInput(string input, string regex)
        {
            return Regex.Replace(input, regex, string.Empty);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="replace">替换字符串</param>
        /// <returns>替换后字符串</returns>
        public static string ReplaceInput(string input, string regex, string replace)
        {
            return Regex.Replace(input, regex, replace);
        }

        #endregion

        #region 验证字符串

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>是否验证通过</returns>
        public static bool CheckInput(string input, string regex)
        {
            return Regex.IsMatch(input, regex);
        }

        #endregion

        #region 常用方法

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="begin">开始数字</param>
        /// <param name="end">结尾数字</param>
        /// <returns>是否验证通过</returns>
        public static bool ValidByte(string input, string regex, int begin, int end)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(regex))
            {
                string rep = regex.Replace("#0#", begin.ToString(CultureInfo.InvariantCulture));
                rep = rep.Replace("#1#", end.ToString(CultureInfo.InvariantCulture));
                ret = CheckInput(input, rep);
            }
            return ret;
        }

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="fix">后缀名</param>
        /// <returns>是否验证通过</returns>
        public static bool ValidPostfix(string input, string regex, string fix)
        {
            string ret = string.Format(CultureInfo.InvariantCulture, regex, fix);
            return CheckInput(input, ret);
        }

        #endregion

        private RegexHelper()
        {
        }

        internal static bool IsMatch(string ip, string pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}
