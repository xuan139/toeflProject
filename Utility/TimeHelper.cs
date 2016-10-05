/// <summary>
/// ��˵����ʱ����SecondToMinute(int Second) ����ת���ɷ���
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��[url=http://www.sufeinet.com/thread-655-1-1.html]http://www.sufeinet.com/thread-655-1-1.html[/url]
/// </summary>
using System;

namespace UnileverCAS.UnileverFun
{
    /// <summary>
    /// ʱ����
    /// 1��SecondToMinute(int Second) ����ת���ɷ���
    /// </summary>
    public class TimeHelper
    {
        /// <summary>
        /// ��ʱ���ʽ���� ������ ����ʽ,���ʱ��Ϊnull�����ص�ǰϵͳʱ��
        /// </summary>
        /// <param name="dt">�����շָ���</param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatDate(DateTime dt, char Separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string tem = string.Format("yyyy{0}MM{1}dd", Separator, Separator);
                return dt.ToString(tem);
            }
            else
            {
                return GetFormatDate(DateTime.Now, Separator);
            }
        }
        /// <summary>
        /// ��ʱ���ʽ���� ʱ���� ����ʽ,���ʱ��Ϊnull�����ص�ǰϵͳʱ��
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatTime(DateTime dt, char Separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string tem = string.Format("hh{0}mm{1}ss", Separator, Separator);
                return dt.ToString(tem);
            }
            else
            {
                return GetFormatDate(DateTime.Now, Separator);
            }
        }
        /// <summary>
        /// ����ת���ɷ���
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }

        #region ����ĳ��ĳ�����һ��
        /// <summary>
        /// ����ĳ��ĳ�����һ��
        /// </summary>
        /// <param name="year">���</param>
        /// <param name="month">�·�</param>
        /// <returns>��</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region ����ʱ���
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "��" + DateTime1.Day.ToString() + "��";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "Сʱǰ";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "����ǰ";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region ����������ڵļ��
        /// <summary>
        /// ����������ڵļ��
        /// </summary>
        /// <param name="DateTime1">����һ��</param>
        /// <param name="DateTime2">���ڶ���</param>
        /// <returns>���ڼ��TimeSpan��</returns>
        public static TimeSpan DateDiff2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region ��ʽ������ʱ��
        /// <summary>
        /// ��ʽ������ʱ��
        /// </summary>
        /// <param name="dateTime1">����ʱ��</param>
        /// <param name="dateMode">��ʾģʽ</param>
        /// <returns>0-9��ģʽ������</returns>
        public static string FormatDate(DateTime dateTime1, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime1.ToString("yyyy-MM-dd");
                case "1":
                    return dateTime1.ToString("yyyy-MM-dd HH:mm:ss");
                case "2":
                    return dateTime1.ToString("yyyy/MM/dd");
                case "3":
                    return dateTime1.ToString("yyyy��MM��dd��");
                case "4":
                    return dateTime1.ToString("MM-dd");
                case "5":
                    return dateTime1.ToString("MM/dd");
                case "6":
                    return dateTime1.ToString("MM��dd��");
                case "7":
                    return dateTime1.ToString("yyyy-MM");
                case "8":
                    return dateTime1.ToString("yyyy/MM");
                case "9":
                    return dateTime1.ToString("yyyy��MM��");
                default:
                    return dateTime1.ToString();
            }
        }
        #endregion

        #region �õ��������
        /// <summary>
        /// �õ��������
        /// </summary>
        /// <param name="time1">��ʼ����</param>
        /// <param name="time2">��������</param>
        /// <returns>�������֮��� �������</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);

            // ��ȡ����ʱ�����������
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion
    }
}