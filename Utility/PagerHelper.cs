using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

namespace UnileverCAS.UnileverFun
{
    public class PageHelper
    {
        #region �ؼ�״̬����

        /// <summary>
        /// ����ҳ���ϵ�һЩ���
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">���������Ŀؼ�</param>
        public static void LockPage(Page page, object[] obj)
        {
            Control htmlForm = null;
            foreach (Control ctl in page.Controls)
            {
                if (ctl is HtmlForm)
                {
                    htmlForm = ctl;
                    break;
                }
            }
            //foreach (Control ctl in page.Controls[1].Controls)
            foreach (Control ctl in htmlForm.Controls)
            {
                if (IsContains(obj, ctl) == false)
                {
                    //����
                    LockControl(page, ctl);
                }
                else
                {
                    //�������
                    UnLockControl(page, ctl);
                }
            }
        }

        /// <summary>
        /// �������ҳ���ϵ�һЩ���
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">�������������Ŀؼ�</param>
        public static void UnLockPage(Page page, object[] obj)
        {
            Control htmlForm = null;
            foreach (Control ctl in page.Controls)
            {
                if (ctl is HtmlForm)
                {
                    htmlForm = ctl;
                    break;
                }
            }
            //foreach (Control ctl in page.Controls[1].Controls)
            foreach (Control ctl in htmlForm.Controls)
            {
                if (IsContains(obj, ctl) == false)
                {
                    //�������
                    UnLockControl(page, ctl);
                }
                else
                {
                    //����
                    LockControl(page, ctl);
                }
            }
        }

        /// <summary>
        /// ���ÿؼ�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctl"></param>
        private static void LockControl(Page page, Control ctl)
        {
            //WebControl
            if (ctl is Button || ctl is CheckBox || ctl is HyperLink || ctl is LinkButton
                || ctl is ListControl || ctl is TextBox)
            {
                ((WebControl)ctl).Enabled = false;

                #region �����ı����ܽ��ã�Ӧ��Ϊֻ������Ȼ����������ʹ��

                if (ctl is TextBox)
                {
                    if (((TextBox)ctl).TextMode == TextBoxMode.MultiLine)
                    {
                        ((TextBox)ctl).Enabled = true;
                        ((TextBox)ctl).ReadOnly = true;
                    }
                }

                #endregion

                #region ʱ��ؼ�����ʱ����ʾͼƬ



                #endregion
            }

            //HtmlControl
            if (ctl is HtmlInputFile)
            {
                ((HtmlInputFile)ctl).Disabled = true;
            }
        }

        /// <summary>
        /// ���ſؼ�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctl"></param>
        private static void UnLockControl(Page page, Control ctl)
        {
            //WebControl
            if (ctl is Button || ctl is CheckBox || ctl is HyperLink || ctl is LinkButton
                || ctl is ListControl || ctl is TextBox)
            {
                ((WebControl)ctl).Enabled = true;

                //�ı���ȥ��ֻ������
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).ReadOnly = false;
                }

                ////ʱ�������ı��򲻽���ʱ��ʾ��ť
                //if (ctl is WebDateTimeEdit)
                //{
                //    ((WebDateTimeEdit)ctl).SpinButtons.Display = ButtonDisplay.OnRight;
                //}

                ////ʱ��ѡ���ı��򲻽���ʱ��ʾ��ť
                //if (ctl is WebDateChooser)
                //{
                //    page.ClientScript.RegisterStartupScript(typeof(string), "Display" + ctl.ClientID + "Image", "<script language=javascript>" +
                //        "document.getElementById('" + ctl.ClientID + "_img" + "').style.display='';</script>");
                //}
            }

            //HtmlControl
            if (ctl is HtmlInputFile)
            {
                ((HtmlInputFile)ctl).Disabled = false;
            }
        }

        /// <summary>
        /// �������Ƿ������ǰ�ؼ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ctl"></param>
        /// <returns></returns>
        private static bool IsContains(object[] obj, Control ctl)
        {
            foreach (Control c in obj)
            {
                if (c.ID == ctl.ID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region ҳ�洦��������������

        /// <summary>
        /// �õ���ǰҳ����ʵ��
        /// </summary>
        /// <returns></returns>
        public static Page GetCurrentPage()
        {
            return (Page)HttpContext.Current.Handler;
        }

        /// <summary>
        /// ��System.Web.HttpRequest��Url�л�ȡ�����õ�ҳ������
        /// </summary>
        /// <returns>ҳ������</returns>
        public static string GetPageName()
        {
            int start = 0;
            int end = 0;
            string Url = HttpContext.Current.Request.RawUrl;
            start = Url.LastIndexOf("/") + 1;
            end = Url.IndexOf("?");
            if (end <= 0)
            {
                return Url.Substring(start, Url.Length - start);
            }
            else
            {
                return Url.Substring(start, end - start);
            }
        }

        /// <summary>
        /// ��ȡQueryStringֵ
        /// </summary>
        /// <param name="queryStringName">QueryString����</param>
        /// <returns>QueryStringֵ</returns>
        public static string GetQueryString(string queryStringName)
        {
            if ((HttpContext.Current.Request.QueryString[queryStringName] != null) &&
                (HttpContext.Current.Request.QueryString[queryStringName] != "undefined"))
            {
                return HttpContext.Current.Request.QueryString[queryStringName].Trim();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// ҳ����ת
        /// </summary>
        /// <param name="url">URL��ַ</param>
        public void Redirect(string url)
        {
            Page page = GetCurrentPage();
            page.Response.Redirect(url);
        }

        /// <summary>
        /// ��ȡ��ǰ����ҳ������ڸ�Ŀ¼�Ĳ㼶
        /// </summary>
        /// <returns></returns>
        public static string GetRelativeLevel()
        {
            string ApplicationPath = HttpContext.Current.Request.ApplicationPath;
            if (ApplicationPath.Trim() == "/")
            {
                ApplicationPath = "";
            }

            int i = ApplicationPath == "" ? 1 : 2;
            return "";//Nandasoft.Helper.NDHelperString.Repeat("../", Nandasoft.Helper.NDHelperString.RepeatTime(HttpContext.Current.Request.Path, "/") - i);
        }

        /// <summary>
        /// дjavascript�ű�
        /// </summary>
        /// <param name="script">�ű�����</param>
        public static void WriteScript(string script)
        {
            Page page = GetCurrentPage();

            // NDGridViewScriptFirst(page.Form.Controls, page);

            //ScriptManager.RegisterStartupScript(page, page.GetType(), System.Guid.NewGuid().ToString(), script, true);

        }

        //private void NDGridViewScriptFirst(ControlCollection ctls, Page page)
        //{

        //    foreach (Control ctl in ctls)
        //    {
        //        if (ctl is NDGridView)
        //        {
        //            NDGridView ndgv = (NDGridView)ctl;
        //            ScriptManager.RegisterStartupScript(page, page.GetType(), ndgv.ClientScriptKey, ndgv.ClientScriptName, true);
        //        }
        //        else
        //        {
        //            NDGridViewScriptFirst(ctl.Controls, page);
        //        }
        //    }
        //}

        /// <summary>
        /// ���ؿͻ���������汾
        /// �����IE���ͣ����ذ汾����
        /// �������IE���ͣ�����-1
        /// </summary>
        /// <returns>һλ���ְ汾��</returns>
        //public static int GetClientBrowserVersion()
        //{
        //    string USER_AGENT = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

        //    if (USER_AGENT.IndexOf("MSIE") < 0) return -1;

        //    string version = USER_AGENT.Substring(USER_AGENT.IndexOf("MSIE") + 5, 1);
        //    if (!Utility.IsInt(version)) return -1;

        //    return Convert.ToInt32(version);
        //}

        #endregion
    }
}
