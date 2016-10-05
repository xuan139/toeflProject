using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

public class HTMLHelper
{
    #region ˽���ֶ�
    private static CookieContainer cc = new CookieContainer();
    private static string contentType = "application/x-www-form-urlencoded";
    private static string accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
    private static string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
    private static Encoding encoding = Encoding.GetEncoding("utf-8");
    private static int delay = 1000;
    private static int maxTry = 300;
    private static int currentTry = 0;
    #endregion

    #region ��������
    /// <summary> 
    /// Cookie
    /// </summary> 
    public static CookieContainer CookieContainer
    {
        get
        {
            return cc;
        }
    }

    /// <summary> 
    /// ����
    /// </summary> 
    public static Encoding Encoding
    {
        get
        {
            return encoding;
        }
        set
        {
            encoding = value;
        }
    }

    public static int NetworkDelay
    {
        get
        {
            Random r = new Random();
            return (r.Next(delay, delay * 2));
        }
        set
        {
            delay = value;
        }
    }

    public static int MaxTry
    {
        get
        {
            return maxTry;
        }
        set
        {
            maxTry = value;
        }
    }
    #endregion

    #region ��ȡHTML
    /// <summary>
    /// ��ȡHTML
    /// </summary>
    /// <param name="url">��ַ</param>
    /// <param name="postData">post �ύ���ַ���</param>
    /// <param name="isPost">�Ƿ���post</param>
    /// <param name="cookieContainer">CookieContainer</param>
    public static string GetHtml(string url, string postData, bool isPost, CookieContainer cookieContainer)
    {
        if (string.IsNullOrEmpty(postData)) return GetHtml(url, cookieContainer);
        Thread.Sleep(NetworkDelay);
        currentTry++;
        HttpWebRequest httpWebRequest = null;
        HttpWebResponse httpWebResponse = null;
        try
        {
            byte[] byteRequest = Encoding.Default.GetBytes(postData);
            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
            httpWebRequest.Referer = url;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = isPost ? "POST" : "GET";
            httpWebRequest.ContentLength = byteRequest.Length;
            Stream stream = httpWebRequest.GetRequestStream();
            stream.Write(byteRequest, 0, byteRequest.Length);
            stream.Close();
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, encoding);
            string html = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            currentTry = 0;
            httpWebRequest.Abort();
            httpWebResponse.Close();
            return html;
        }
        catch (Exception e)
        {
            if (currentTry <= maxTry) GetHtml(url, postData, isPost, cookieContainer);
            currentTry--;
            if (httpWebRequest != null) httpWebRequest.Abort();
            if (httpWebResponse != null) httpWebResponse.Close();
            return string.Empty;
        }
    }

    /// <summary>
    /// ��ȡHTML
    /// </summary>
    /// <param name="url">��ַ</param>
    /// <param name="cookieContainer">CookieContainer</param>
    public static string GetHtml(string url, CookieContainer cookieContainer)
    {
        Thread.Sleep(NetworkDelay);
        currentTry++;
        HttpWebRequest httpWebRequest = null;
        HttpWebResponse httpWebResponse = null;
        try
        {
            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
            httpWebRequest.Referer = url;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = "GET";
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, encoding);
            string html = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            currentTry--;
            httpWebRequest.Abort();
            httpWebResponse.Close();
            return html;
        }
        catch (Exception e)
        {
            if (currentTry <= maxTry) GetHtml(url, cookieContainer);
            currentTry--;
            if (httpWebRequest != null) httpWebRequest.Abort();
            if (httpWebResponse != null) httpWebResponse.Close();
            return string.Empty;
        }
    }
    #endregion

    #region ��ȡ�ַ���
    /// <summary>
    /// ��ȡ�ַ���
    /// </summary>
    //---------------------------------------------------------------------------------------------------------------
    // ʾ��:
    // System.Net.CookieContainer cookie = new System.Net.CookieContainer(); 
    // Stream s = HttpHelper.GetStream("http://ptlogin2.qq.com/getimage?aid=15000102&0.43878429697395826", cookie);
    // picVerify.Image = Image.FromStream(s);
    //---------------------------------------------------------------------------------------------------------------
    /// <param name="url">��ַ</param>
    /// <param name="cookieContainer">cookieContainer</param>
    public static Stream GetStream(string url, CookieContainer cookieContainer)
    {
        currentTry++;

        HttpWebRequest httpWebRequest = null;
        HttpWebResponse httpWebResponse = null;

        try
        {
            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
            httpWebRequest.Referer = url;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = "GET";

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            currentTry--;
            return responseStream;
        }
        catch (Exception e)
        {
            if (currentTry <= maxTry)
            {
                GetHtml(url, cookieContainer);
            }

            currentTry--;

            if (httpWebRequest != null)
            {
                httpWebRequest.Abort();
            } if (httpWebResponse != null)
            {
                httpWebResponse.Close();
            }
            return null;
        }
    }
    #endregion

    #region ���HTML���
    ///<summary>   
    ///���HTML���   
    ///</summary>   
    ///<param name="NoHTML">����HTML��Դ��</param>   
    ///<returns>�Ѿ�ȥ���������</returns>   
    public static string NoHTML(string Htmlstring)
    {
        //ɾ���ű�   
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

        //ɾ��HTML   
        Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
        Htmlstring = regex.Replace(Htmlstring, "");
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");

        return Htmlstring;
    }
    #endregion

    #region ƥ��ҳ�������
    /// <summary>
    /// ��ȡҳ�����������
    /// </summary>
    public string GetHref(string HtmlCode)
    {
        string MatchVale = "";
        string Reg = @"(h|H)(r|R)(e|E)(f|F) *= *('|"")?((\w|\\|\/|\.|:|-|_)+)[\S]*";
        foreach (Match m in Regex.Matches(HtmlCode, Reg))
        {
            MatchVale += (m.Value).ToLower().Replace("href=", "").Trim() + "|";
        }
        return MatchVale;
    }
    #endregion

    #region ƥ��ҳ���ͼƬ��ַ
    /// <summary>
    /// ƥ��ҳ���ͼƬ��ַ
    /// </summary>
    /// <param name="imgHttp">Ҫ�����http://·����Ϣ</param>
    public string GetImgSrc(string HtmlCode, string imgHttp)
    {
        string MatchVale = "";
        string Reg = @"<img.+?>";
        foreach (Match m in Regex.Matches(HtmlCode.ToLower(), Reg))
        {
            MatchVale += GetImg((m.Value).ToLower().Trim(), imgHttp) + "|";
        }

        return MatchVale;
    }

    /// <summary>
    /// ƥ��<img src="" />�е�ͼƬ·��ʵ������
    /// </summary>
    /// <param name="ImgString"><img src="" />�ַ���</param>
    public string GetImg(string ImgString, string imgHttp)
    {
        string MatchVale = "";
        string Reg = @"src=.+\.(bmp|jpg|gif|png|)";
        foreach (Match m in Regex.Matches(ImgString.ToLower(), Reg))
        {
            MatchVale += (m.Value).ToLower().Trim().Replace("src=", "");
        }
        if (MatchVale.IndexOf(".net") != -1 || MatchVale.IndexOf(".com") != -1 || MatchVale.IndexOf(".org") != -1 || MatchVale.IndexOf(".cn") != -1 || MatchVale.IndexOf(".cc") != -1 || MatchVale.IndexOf(".info") != -1 || MatchVale.IndexOf(".biz") != -1 || MatchVale.IndexOf(".tv") != -1)
            return (MatchVale);
        else
            return (imgHttp + MatchVale);
    }
    #endregion

    #region ץȡԶ��ҳ������
    /// <summary>
    /// ��GET��ʽץȡԶ��ҳ������
    /// </summary>
    public static string Get_Http(string tUrl)
    {
        string strResult;
        try
        {
            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(tUrl);
            hwr.Timeout = 19600;
            HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();
            Stream myStream = hwrs.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.Default);
            StringBuilder sb = new StringBuilder();
            while (-1 != sr.Peek())
            {
                sb.Append(sr.ReadLine() + "\r\n");
            }
            strResult = sb.ToString();
            hwrs.Close();
        }
        catch (Exception ee)
        {
            strResult = ee.Message;
        }
        return strResult;
    }

    /// <summary>
    /// ��POST��ʽץȡԶ��ҳ������
    /// </summary>
    /// <param name="postData">�����б�</param>
    public static string Post_Http(string url, string postData, string encodeType)
    {
        string strResult = null;
        try
        {
            Encoding encoding = Encoding.GetEncoding(encodeType);
            byte[] POST = encoding.GetBytes(postData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = POST.Length;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(POST, 0, POST.Length); //����POST
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            strResult = reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            strResult = ex.Message;
        }
        return strResult;
    }
    #endregion

    #region ѹ��HTML���
    /// <summary>
    /// ѹ��HTML���
    /// </summary>
    public static string ZipHtml(string Html)
    {
        Html = Regex.Replace(Html, @">\s+?<", "><");//ȥ��HTML�еĿհ��ַ�
        Html = Regex.Replace(Html, @"\r\n\s*", "");
        Html = Regex.Replace(Html, @"<body([\s|\S]*?)>([\s|\S]*?)</body>", @"<body$1>$2</body>", RegexOptions.IgnoreCase);
        return Html;
    }
    #endregion

    #region ����ָ��HTML��ǩ
    /// <summary>
    /// ����ָ��HTML��ǩ
    /// </summary>
    /// <param name="s_TextStr">Ҫ���˵��ַ�</param>
    /// <param name="html_Str">a img p div</param>
    public static string DelHtml(string s_TextStr, string html_Str)
    {
        string rStr = "";
        if (!string.IsNullOrEmpty(s_TextStr))
        {
            rStr = Regex.Replace(s_TextStr, "<" + html_Str + "[^>]*>", "", RegexOptions.IgnoreCase);
            rStr = Regex.Replace(rStr, "</" + html_Str + ">", "", RegexOptions.IgnoreCase);
        }
        return rStr;
    }
    #endregion

    #region �����ļ���
    /// <summary>
    /// �����ļ���
    /// </summary>
    public static string File(string Path, System.Web.UI.Page p)
    {
        return @p.ResolveUrl(Path);
    }
    #endregion

    #region ����CSS��ʽ�ļ�
    /// <summary>
    /// ����CSS��ʽ�ļ�
    /// </summary>
    public static string CSS(string cssPath, System.Web.UI.Page p)
    {
        return @"<link href=""" + p.ResolveUrl(cssPath) + @""" rel=""stylesheet"" type=""text/css"" />" + "\r\n";
    }
    #endregion

    #region ����JavaScript�ű��ļ�
    /// <summary>
    /// ����javascript�ű��ļ�
    /// </summary>
    public static string JS(string jsPath, System.Web.UI.Page p)
    {
        return @"<script type=""text/javascript"" src=""" + p.ResolveUrl(jsPath) + @"""></script>" + "\r\n";
    }
    #endregion
}
