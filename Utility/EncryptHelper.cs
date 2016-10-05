
    	
    ///֮ǰ��ƪ���ܰ�������ò����Ͻ�,����ǿ�����������޸�,Ҳ���Լ�����һ�㣬��������а������Կ���,
    ///�Ǻǣ�~����඼�������˲��Թ���û����.
      
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;
    //using log4net;
      
    namespace Utility
    {
        /// <summary>
        /// @Author:������
        /// @Date:2012/7/30
        /// @Descripte:EncryptHelper���ܰ�����.
        /// </summary>
        public class EncryptHelper
        {
            /// <summary>
            ///����һ:
            ///���ּ���֮����ַ�������ʮ��λ��(��ĸ������)�ַ��� 
            /// Example: password��admin ���ܱ�ɺ�21232f297a57a5a743894a0e4a801fc3
            /// </summary>
            /// <param name="beforeStr">
            /// <returns></returns>
            public string MD5Encrypt(string beforeStr)
            {
                string afterString = "";
                try
                {
                    MD5 md5 = MD5.Create();
                    byte[] hashs = md5.ComputeHash(Encoding.UTF8.GetBytes(beforeStr));
      
                    foreach (byte by in hashs)
                        //��������ĸ�������ݽ��м���.//3y ����,y3�����Ի� x3j��Ӧ���ǳ���32λ������
                        afterString += by.ToString("x2");
                }
                catch (Exception ex)
                {
                    //ILog log = log4net.LogManager.GetLogger(this.GetType());
                    //log.Error("==============��������һ��������==============" + ex.Message.ToString());
                }
                return afterString;
            }
      
            /// <summary>
            /// ������
            /// HashAlgorithm����
            /// ���ּ�����  ��ĸ��-���ַ� 
            /// Example: password��admin ���ܱ�ɺ�19-A2-85-41-44-B6-3A-8F-76-17-A6-F2-25-01-9B-12
            /// </summary>
            /// <param name="password">
            /// <returns></returns>
            public String HashEncrypt(string password)
            {
                Byte[] hashedBytes = null;
                try
                {
                    Byte[] clearBytes = new UnicodeEncoding().GetBytes(password);
                    hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
                }
                catch (Exception ex)
                {
                    //ILog log = log4net.LogManager.GetLogger(this.GetType());
                    //log.Error("==============��������һ��������==============" + ex.Message.ToString());
                }
                return BitConverter.ToString(hashedBytes);//MD5����
            }
      
            /// <summary>
            /// ������:
            /// MD5  +   HashCode����
            /// Example: password��admin ���ܱ�ɺ� 895b7da64943134be17b825ce118456c
            /// </summary>
            /// <returns></returns>
            public String MD5HashCodeEncrypt(string EncryptPwd)
            {
                return MD5Encrypt(HashEncrypt(EncryptPwd)); //��HashEncrypt��������MD5
            }
      
            /// <summary>
            /// ������:
            /// HashCode +MD5 ����
            /// Example: password��admin ���ܱ�ɺ�EB-1D-6D-E2-FC-F1-CD-94-4D-75-78-E6-3D-7A-12-32
            /// </summary>
            /// <param name="EncryptPwd">
            /// <returns></returns>
            public String HashCodeMD5Encrypt(string EncryptPwd)
            {
                return HashEncrypt(MD5Encrypt(EncryptPwd)); //��MD5������HashCode
            }
            /// <summary>
            /// ������
            /// </summary>
            /// <param name="EncryptPwd">
            /// <returns></returns>
      
            public String HashMD5Encrypt(string EncryptPwd)
            {
                return HashCodeMD5Encrypt(HashCodeMD5Encrypt(EncryptPwd)); //��HashCodeMD5Encrypt������HashCode
            }
            /// <summary>
            /// ������
            /// �����ǲ����е����أ�
            /// ��һ���Լ���д.
            /// </summary>
            /// <param name="EncryptPwd">
            /// <returns></returns>
            public String MD5HashEncrypt(string EncryptPwd)
            {
                return MD5HashCodeEncrypt(MD5HashCodeEncrypt(EncryptPwd)); //��MD5������HashCode
            }
            /// <summary>
            /// 64λ˫��MD5Сд
            /// </summary>
            /// <returns></returns>
            public static string Last64(string s)
            {
                if (s.Length != 32)
                    return "";
                string s1 = s.Substring(0, 16);
                string s2 = s.Substring(16, 16);
                return Lower32(s1) + Lower32(s2);
            }
            /// <summary>
            /// 32λ��д
            /// </summary>
            /// <returns></returns>
            public static string Upper32(string s)
            {
                //s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
                //return s.ToUpper();
            }
            /// <summary>
            /// 32λСд
            /// </summary>
            /// <returns></returns>
            public static string Lower32(string s)
            {
                //s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
                //return s.ToLower();
            }
            /// <summary>
            /// 16λ��д
            /// </summary>
            /// <returns></returns>
            public static string Upper16(string s)
            {
                //s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
                //return s.ToUpper().Substring(8, 16);
            }
            /// <summary>
            /// 16λСд
            /// </summary>
            /// <returns></returns>
            public static string Lower16(string s)
            {
                //s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
                //return s.ToLower().Substring(8, 16);
            }
        }
      
    }

