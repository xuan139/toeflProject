using System;
using System.IO;
using System.Threading;
using System.Web;

/// <summary>
/// �ļ�������
/// </summary>
public class FileDown
{
    public FileDown()
    { }

    /// <summary>
    /// ����Ϊ����·��
    /// </summary>
    public static string FileNameExtension(string FileName)
    {
        return Path.GetExtension(MapPathFile(FileName));
    }

    /// <summary>
    /// ��ȡ������ַ
    /// </summary>
    public static string MapPathFile(string FileName)
    {
        return HttpContext.Current.Server.MapPath(FileName);
    }

    /// <summary>
    /// ��ͨ����
    /// </summary>
    /// <param name="FileName">�ļ�����·��</param>
    public static void DownLoadold(string FileName)
    {
        string destFileName = MapPathFile(FileName);
        if (File.Exists(destFileName))
        {
            FileInfo fi = new FileInfo(destFileName);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(destFileName), System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(destFileName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

    /// <summary>
    /// �ֿ�����
    /// </summary>
    /// <param name="FileName">�ļ�����·��</param>
    public static void DownLoad(string FileName)
    {
        string filePath = MapPathFile(FileName);
        long chunkSize = 204800;             //ָ�����С 
        byte[] buffer = new byte[chunkSize]; //����һ��200K�Ļ����� 
        long dataToRead = 0;                 //�Ѷ����ֽ���   
        FileStream stream = null;
        try
        {
            //���ļ�   
            stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            dataToRead = stream.Length;

            //����Httpͷ   
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
            HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

            while (dataToRead > 0)
            {
                if (HttpContext.Current.Response.IsClientConnected)
                {
                    int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.Clear();
                    dataToRead -= length;
                }
                else
                {
                    dataToRead = -1; //��ֹclientʧȥ���� 
                }
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write("Error:" + ex.Message);
        }
        finally
        {
            if (stream != null) stream.Close();
            HttpContext.Current.Response.Close();
        }
    }

    /// <summary>
    ///  ���Ӳ���ļ����ṩ���� ֧�ִ��ļ����������ٶ����ơ���Դռ��С
    /// </summary>
    /// <param name="_Request">Page.Request����</param>
    /// <param name="_Response">Page.Response����</param>
    /// <param name="_fileName">�����ļ���</param>
    /// <param name="_fullPath">���ļ�������·��</param>
    /// <param name="_speed">ÿ���������ص��ֽ���</param>
    /// <returns>�����Ƿ�ɹ�</returns>
    //---------------------------------------------------------------------
    //���ã�
    // string FullPath=Server.MapPath("count.txt");
    // ResponseFile(this.Request,this.Response,"count.txt",FullPath,100);
    //---------------------------------------------------------------------
    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;

                long fileLength = myFile.Length;
                long startBytes = 0;
                int pack = 10240;  //10K bytes
                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;

                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }

                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
}
