using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;

namespace UnileverCAS.UnileverFun
{
    /// <summary>  
    /// Cookie������  
    /// </summary>  
    public static class CookieHelper
    {
        /// <summary>  
        /// ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires  
        /// </summary>  
        /// <param name="strCookieName">COOKIE������</param>  
        /// <param name="strValue">COOKIE����Valueֵ</param>  
        public static void SetObj(string strCookieName, string strValue)
        {
            SetObj(strCookieName, 1, strValue, "", "/");
        }
        /// <summary>  
        /// ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires  
        /// </summary>  
        /// <param name="strCookieName">COOKIE������</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��</param>  
        /// <param name="strValue">COOKIE����Valueֵ</param>  
        public static void SetObj(string strCookieName, int iExpires, string strValue)
        {
            SetObj(strCookieName, iExpires, strValue, "", "/");
        }
        /// <summary>  
        /// ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires  
        /// </summary>  
        /// <param name="strCookieName">COOKIE������</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��</param>  
        /// <param name="strValue">COOKIE����Valueֵ</param>  
        /// <param name="strDomains">������,���������;����</param>  
        public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains)
        {
            SetObj(strCookieName, iExpires, strValue, strDomains, "/");
        }
        /// <summary>  
        /// ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires  
        /// </summary>  
        /// <param name="strCookieName">COOKIE������</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��</param>  
        /// <param name="strValue">COOKIE����Valueֵ</param>  
        /// <param name="strDomains">������,���������;����</param>  
        /// <param name="strPath">����·��</param>  
        public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains, string strPath)
        {
            string _strDomain = SelectDomain(strDomains);
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
            if (_strDomain.Length > 0)
                objCookie.Domain = _strDomain;
            if (iExpires > 0)
            {
                if (iExpires == 1)
                    objCookie.Expires = DateTime.MaxValue;
                else
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        /// <summary>  
        /// ����COOKIE���󲢸����KEY��ֵ  
        /// ���/ֵ���£�  
        /// NameValueCollection myCol = new NameValueCollection();  
        /// myCol.Add("red", "rojo");  
        /// myCol.Add("green", "verde");  
        /// myCol.Add("blue", "azul");  
        /// myCol.Add("red", "rouge");   �����red:rojo,rouge��green:verde��blue:azul��  
        /// </summary>  
        /// <param name="strCookieName">COOKIE������</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��</param>  
        /// <param name="KeyValue">��/ֵ�Լ���</param>  
        public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue)
        {
            SetObj(strCookieName, iExpires, KeyValue, "", "/");
        }
        public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains)
        {
            SetObj(strCookieName, iExpires, KeyValue, strDomains, "/");
        }
        /// <summary>  
        /// ����COOKIE���󲢸����KEY��ֵ  
        /// ���/ֵ���£�  
        /// NameValueCollection myCol = new NameValueCollection();  
        /// myCol.Add("red", "rojo");  
        /// myCol.Add("green", "verde");  
        /// myCol.Add("blue", "azul");  
        /// myCol.Add("red", "rouge");   �����red:rojo,rouge��green:verde��blue:azul��  
        /// </summary>  
        /// <param name="strCookieName">COOKIE������</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��</param>  
        /// <param name="KeyValue">��/ֵ�Լ���</param>  
        /// <param name="strDomains">������,���������;����</param>  
        /// <param name="strPath">����·��</param>  
        public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains, string strPath)
        {
            string _strDomain = SelectDomain(strDomains);
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            foreach (String key in KeyValue.AllKeys)
            {
                objCookie[key] = HttpUtility.UrlEncode(KeyValue[key].Trim());
            }
            if (_strDomain.Length > 0)
                objCookie.Domain = _strDomain;
            objCookie.Path = strPath.Trim();
            if (iExpires > 0)
            {
                if (iExpires == 1)
                    objCookie.Expires = DateTime.MaxValue;
                else
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        /// <summary>  
        /// ��ȡCookieĳ�������Valueֵ������Valueֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <returns>Valueֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null</returns>  
        public static string GetValue(string strCookieName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            else
            {
                string _value = HttpContext.Current.Request.Cookies[strCookieName].Value;
                return HttpUtility.UrlDecode(_value);
            }
        }
        /// <summary>  
        /// ��ȡCookieĳ�������ĳ��Key���ļ�ֵ������Key��ֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null�����Key�������ڣ��򷵻��ַ���"KeyNonexistence"  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <param name="strKeyName">Key����</param>  
        /// <returns>Key��ֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null�����Key�������ڣ��򷵻��ַ���"KeyNonexistence"</returns>  
        public static string GetValue(string strCookieName, string strKeyName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            else
            {
                string strObjValue = HttpContext.Current.Request.Cookies[strCookieName].Value;
                string strKeyName2 = strKeyName + "=";
                //if (strObjValue.IndexOf(strKeyName2) == -1)  
                if (!strObjValue.Contains(strKeyName2))
                    return null;
                else
                {
                    string _value = HttpContext.Current.Request.Cookies[strCookieName][strKeyName];
                    return HttpUtility.UrlDecode(_value);
                }
            }
        }
        /// <summary>  
        /// �޸�ĳ��COOKIE����ĳ��Key���ļ�ֵ �� ��ĳ��COOKIE�������Key�� �����ñ������������ɹ������ַ���"success"��������󱾾Ͳ����ڣ��򷵻��ַ���null��  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <param name="strKeyName">Key����</param>  
        /// <param name="KeyValue">Key��ֵ</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��ע�⣺�����޸Ĺ��ܣ�ʵ���ؽ����ǣ�����ʱ��ҲҪ���裬��Ϊû�취��þɵ���Ч��</param>  
        /// <returns>������󱾾Ͳ����ڣ��򷵻��ַ���null����������ɹ������ַ���"success"��</returns>  
        public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires)
        {
            return Edit(strCookieName, strKeyName, KeyValue, iExpires, "", "/");
        }
        /// <summary>  
        /// �޸�ĳ��COOKIE����ĳ��Key���ļ�ֵ �� ��ĳ��COOKIE�������Key�� �����ñ������������ɹ������ַ���"success"��������󱾾Ͳ����ڣ��򷵻��ַ���null��  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <param name="strKeyName">Key����</param>  
        /// <param name="KeyValue">Key��ֵ</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��ע�⣺�����޸Ĺ��ܣ�ʵ���ؽ����ǣ�����ʱ��ҲҪ���裬��Ϊû�취��þɵ���Ч��</param>  
        /// <param name="strPath">����·��</param>  
        /// <returns>������󱾾Ͳ����ڣ��򷵻��ַ���null����������ɹ������ַ���"success"��</returns>  
        public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strPath)
        {
            return Edit(strCookieName, strKeyName, KeyValue, iExpires, "", strPath);
        }
        /// <summary>  
        /// �޸�ĳ��COOKIE����ĳ��Key���ļ�ֵ �� ��ĳ��COOKIE�������Key�� �����ñ������������ɹ������ַ���"success"��������󱾾Ͳ����ڣ��򷵻��ַ���null��  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <param name="strKeyName">Key����</param>  
        /// <param name="KeyValue">Key��ֵ</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��ע�⣺�����޸Ĺ��ܣ�ʵ���ؽ����ǣ�����ʱ��ҲҪ���裬��Ϊû�취��þɵ���Ч��</param>  
        /// <param name="strDomains">������,���������;����</param>  
        /// <param name="strPath">����·��</param>  
        /// <returns>������󱾾Ͳ����ڣ��򷵻��ַ���null����������ɹ������ַ���"success"��</returns>  
        public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strDomains, string strPath)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
                return null;
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
                objCookie[strKeyName] = HttpUtility.UrlEncode(KeyValue.Trim());
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                        objCookie.Expires = DateTime.MaxValue;
                    else
                        objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
                HttpContext.Current.Response.Cookies.Add(objCookie);
                return "success";
            }
        }
        /// <summary>  
        /// ɾ��COOKIE����  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        public static void Del(string strCookieName)
        {
            Del(strCookieName, "", "/");
        }
        /// <summary>  
        /// ɾ��COOKIE����  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <param name="strDomains">������,���������;����</param>  
        public static void Del(string strCookieName, string strDomains)
        {
            Del(strCookieName, strDomains, "/");
        }
        /// <summary>  
        /// ɾ��COOKIE����  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <param name="strDomains">������,���������;����</param>  
        /// <param name="strPath">����·��</param>  
        public static void Del(string strCookieName, string strDomains, string strPath)
        {
            string _strDomain = SelectDomain(strDomains);
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            if (_strDomain.Length > 0)
                objCookie.Domain = _strDomain;
            objCookie.Path = strPath.Trim();
            objCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        /// <summary>  
        /// ɾ��ĳ��COOKIE����ĳ��Key�Ӽ��������ɹ������ַ���"success"��������󱾾Ͳ����ڣ��򷵻��ַ���null  
        /// </summary>  
        /// <param name="strCookieName">Cookie��������</param>  
        /// <param name="strKeyName">Key����</param>  
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��ע�⣺�����޸Ĺ��ܣ�ʵ���ؽ����ǣ�����ʱ��ҲҪ���裬��Ϊû�취��þɵ���Ч��</param>  
        /// <returns>������󱾾Ͳ����ڣ��򷵻��ַ���null����������ɹ������ַ���"success"��</returns>  
        public static string DelKey(string strCookieName, string strKeyName, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
                objCookie.Values.Remove(strKeyName);
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                        objCookie.Expires = DateTime.MaxValue;
                    else
                        objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
                HttpContext.Current.Response.Cookies.Add(objCookie);
                return "success";
            }
        }
        /// <summary>  
        /// ��λ����ȷ����  
        /// </summary>  
        /// <param name="strDomains"></param>  
        /// <returns></returns>  
        private static string SelectDomain(string strDomains)
        {
            bool _isLocalServer = false;
            if (strDomains.Trim().Length == 0)
                return "";
            string _thisDomain = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            //if (_thisDomain.IndexOf(".") < 0)//˵���Ǽ������������������  
            if (!_thisDomain.Contains("."))
                _isLocalServer = true;
            string _strDomain = "www.abc.com";//���������Ϲ��  
            string[] _strDomains = strDomains.Split(';');
            for (int i = 0; i < _strDomains.Length; i++)
            {
                //if (_thisDomain.IndexOf(_strDomains[i].Trim()) < 0)//�жϵ�ǰ�����Ƿ�����������  
                if (!_thisDomain.Contains(_strDomains[i].Trim()))
                    continue;
                else
                {
                    //������ʵ����(��IP)��������  
                    if (_isLocalServer)
                        _strDomain = "";//���������գ�����Cookie����д��  
                    else
                        _strDomain = _strDomains[i].Trim();
                    break;
                }
            }
            return _strDomain;
        }
    }
}