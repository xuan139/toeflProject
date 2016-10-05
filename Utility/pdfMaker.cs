using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Web;
using System.Threading;
using UnileverCAS.UnileverFun;
using System.Data.SqlClient;
using System.Data.OleDb;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Drawing;
using iTextSharp.text;

namespace PdfMakerHelper
{
    class PdfMakerTool
    {

        /// <summary>
        /// Print a table to a Pdf file in tabular format,
        /// with header image and text on every page
        /// </summary>
        /// <param name="PdfFileName">This is pyhsical file name
        /// of Pdf document that you wanto write to</param>
        /// <param name="dt">Datatable that contain the data to print</param>
        /// <param name="DocTitle">This is the title of the
        /// document to be printed once on first page</param>
        /// <param name="PageHeader">Header text to go
        ///    with header image on every page</param>
        /// <param name="HeaderImgPath">the physical file path of a image
        /// that you want to print out on every page header</param>
        /// <returns></returns>     
        /// 
        //使用 iTextSharp 生成中文 PDF 文档
        public void ExportPdfTable(RichTextBox rtf, string PdfFileName)
        {
            Document doc = new Document(); //iTextSharp document

            try
            {
                string physicalFile = PdfFileName;

                //利用iText五步创建一个PDF文件：helloword。 

                //第一步，创建一个 iTextSharp.text.Document对象的实例： 

                //Document document = new Document();

                Document document = new Document(PageSize.A4, 36, 72, 108, 180);

                //第二步，为该Document创建一个Writer实例： 
                PdfWriter.GetInstance(document, new FileStream(physicalFile, FileMode.Create));

                //第三步，打开当前Document 

                document.Open();

                //第四步，为当前Document添加内容： 
                int i = 0;
                foreach (string line in rtf.Lines)
                {

                    document.Add(new Paragraph(line));
                }

                //document.Add(new Paragraph("Hello World"));

                document.AddHeader("Bible", "God");
                document.AddTitle("MyTitle");

                document.AddSubject("MySubject");

                document.AddCreationDate();



                //第五步，关闭Document 

                document.Close();




            }
            catch (Exception ex)
            {
                ex.ToString();

            }


        }


        //TextSharp.text.Document-object共有三个构造函数： 

        //public Document(); 

        //public Document(Rectangle pageSize); 

        //public Document(Rectangle pageSize, 

        //int marginLeft, 

        //int marginRight, 

        //int marginTop, 

        //int marginBottom); 




    }
}
