using System;
namespace UnileverCAS.UnileverFun
{
    interface IChartHelp1
    {
        void Bindinfo(Dundas.Charting.WebControl.Chart ch);
        void draw_Chart_column(Dundas.Charting.WebControl.Chart ch, string strQuestion, string strQ_id, string datestart, string dateend);
        void draw_Chart_line(Dundas.Charting.WebControl.Chart ch, string strQuestion, string strQ_id, string datestart, string dateend);
        void draw_Chart_pie(Dundas.Charting.WebControl.Chart ch, string strQuestion, string strQ_id, string datestart, string dateend);
        void draw_Chart_pie_jiedai(Dundas.Charting.WebControl.Chart ch, string strQuestion, string strOid, string datestart, string dateend);
        void draw_chart_visit(Dundas.Charting.WebControl.Chart ch, string strQuestion, string strQ_id);
        void ini_ch_chartarea(Dundas.Charting.WebControl.Chart ch, string strArea);
        void ini_ch_line(Dundas.Charting.WebControl.Chart ch, string seriesName);
        void ini_ch_series(Dundas.Charting.WebControl.Chart ch, string str_series);
        void initial_chart(Dundas.Charting.WebControl.Chart ch);
    }
}
