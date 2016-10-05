using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Text;
using UnileverCAS.UnileverFun;




namespace UnileverCAS.UnileverFun
{
    public static class AdvisorySheet
    {
        #region 显示Advisory Sheet内容
        public static string ShowAdvisorySheet(string strAdvId, string strErrorMsg)
        {
            bool bflag = true;
            string strSqlAdvSheet = "";
            string strDetail = "";
            string strHtm="";
            strAdvId=CMFun.GetDelCharString(strAdvId);
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = CMFun.GetConnectString();
            Conn.Open();
            if(strAdvId!="")
            { 
                //读取Advsheet
                strSqlAdvSheet = "select * from AdvSheet where ADV_ID="+strAdvId;
                DataTable dtAdvSheet =CMFun.GetSqlDataTable(Conn, strSqlAdvSheet);;
                int iAdvSheetNum = dtAdvSheet.Rows.Count;
                if (iAdvSheetNum > 0)
                {
                    strDetail= GetAdvSheetDetails(dtAdvSheet.Rows[0]["DETAILS"].ToString());
                }
                else
                {
                    bflag = false;
                    strErrorMsg = "Can not find the Advisory Sheet";
                }
                //
                StringBuilder sbAdvSheet = new StringBuilder();
                strHtm = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\" >\r\n";                sbAdvSheet.Append("");
                sbAdvSheet.Append(strHtm);
                sbAdvSheet.Append("<html>\r\n");
	            sbAdvSheet.Append("<head>\r\n");
	            sbAdvSheet.Append("<title>Advisory Sheet Information</title>\r\n");
                strHtm = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n";
                sbAdvSheet.Append(strHtm);
                strHtm = "<style type=\"text/css\">\r\n";
                sbAdvSheet.Append(strHtm);
	            sbAdvSheet.Append("a:link       { color: #3333FF; text-decoration: none }\r\n");
	            sbAdvSheet.Append("a:visited    { text-decoration: none; color: #3333FF }\r\n");
	            sbAdvSheet.Append("a:active     { color: #3366FF; text-decoration: none }\r\n");
	            sbAdvSheet.Append("a:hover      { color: #3366FF; text-decoration: none}\r\n");
	            sbAdvSheet.Append(".p9 {font-size: 9pt; font-family:Arial; }\r\n");
	            sbAdvSheet.Append(".p9_spec {font-size: 9pt; margin-left:5px; font-family:Arial; }\r\n");
	            sbAdvSheet.Append(".p12 {font-size: 12pt; font-family:Arial; }\r\n");
	            sbAdvSheet.Append("TD.title {FONT-SIZE: 12pt; FONT-FAMILY: Arial; FONT-WEIGHT: bold; }\r\n");
                sbAdvSheet.Append("TD.spec { BACKGROUND-COLOR: #cadefc; FONT-SIZE: 9pt; FONT-FAMILY: Arial; FONT-WEIGHT: 500; }\r\n");
	            sbAdvSheet.Append("TD.common { BACKGROUND-COLOR: #fff4df; FONT-SIZE: 9pt; FONT-FAMILY: Arial; }\r\n");
	            sbAdvSheet.Append("TD.msg { BACKGROUND-COLOR: #ffffff; FONT-SIZE: 9pt; FONT-FAMILY: Arial; }\r\n");
	            sbAdvSheet.Append("TABLE.hide { display: none; margin-left: 10px; BACKGROUND-COLOR: #ffffff; FONT-SIZE: 9pt; FONT-FAMILY: Arial; }\r\n");
	            sbAdvSheet.Append("TABLE.show {margin-left: 10px; BACKGROUND-COLOR: #ffffff; FONT-SIZE: 9pt; FONT-FAMILY: Arial; }\r\n");
	            sbAdvSheet.Append("</style>\r\n");
                strHtm = "<script language=\"javascript\">\r\n";
	            sbAdvSheet.Append(strHtm);
	            sbAdvSheet.Append("<!--\r\n");
	            sbAdvSheet.Append("function openWindow(theURL,winName)\r\n");
	            sbAdvSheet.Append("{\r\n");
                strHtm = " features = \"toolbar=yes,scrollbars=yes,width=700,height=400,Top=50,Left=150\";\r\n";
                sbAdvSheet.Append(strHtm);
	            sbAdvSheet.Append("	var newWin = window.open(theURL,winName,features);\r\n");
	            sbAdvSheet.Append("	newWin.opener = this;\r\n");
	            sbAdvSheet.Append("}\r\n");
	            sbAdvSheet.Append("//-->\r\n");
	            sbAdvSheet.Append("</script>\r\n");
	            sbAdvSheet.Append("</head>\r\n");
	            sbAdvSheet.Append("<body>\r\n");
                strHtm = "<script language=\"javascript\">\r\n";
                sbAdvSheet.Append(strHtm);
	            sbAdvSheet.Append("<!--\r\n");
	            sbAdvSheet.Append("function showTable(obj)\r\n");
	            sbAdvSheet.Append("{\r\n");
	            sbAdvSheet.Append("	if( obj.className == 'hide')\r\n");
	            sbAdvSheet.Append("	{\r\n");
	            sbAdvSheet.Append("		obj.className = 'show';\r\n");
	            sbAdvSheet.Append("	}\r\n");
	            sbAdvSheet.Append("	else\r\n");
	            sbAdvSheet.Append("	{\r\n");
	            sbAdvSheet.Append("		obj.className = 'hide';\r\n");
	            sbAdvSheet.Append("	}\r\n");
	            sbAdvSheet.Append("}\r\n");
	            sbAdvSheet.Append("-->\r\n");
	            sbAdvSheet.Append("</script>\r\n");
                strHtm = "<table width=\"100%\" height=\"0%\" border=\"0\">\r\n";
                sbAdvSheet.Append(strHtm);

                if (bflag == false)
                {
                    strHtm = "	<tr><td class=\"title\">Error!</td><tr>\r\n";
                    sbAdvSheet.Append(strHtm);
                    strHtm = "	<tr><td class=\"msg\">" + strErrorMsg + "</td></tr>\r\n";
                    sbAdvSheet.Append(strHtm);
                }
                else
                {
                    strHtm = "	<tr><td class=\"title\">Advisory Sheet(" + GetSheetStatus(dtAdvSheet.Rows[0]["STATUS"].ToString()) + ")</td></tr>\r\n";
                    sbAdvSheet.Append(strHtm);
                    strHtm = "	<tr><td class=\"msg\" align=\"right\">\r\n";
                    sbAdvSheet.Append(strHtm);

                    string strEmpName = GetEmpName(Conn, dtAdvSheet.Rows[0]["EMP_ID"].ToString());

                    sbAdvSheet.Append("<b>Advisory Sheet ID:</b>" + strAdvId + "<br>\r\n");
			        sbAdvSheet.Append("<b>No.:</b>"+dtAdvSheet.Rows[0]["ADV_SN"].ToString()+"<br>\r\n");
                    sbAdvSheet.Append("<b>Date:</b>" +  GetDateTimeToDate(dtAdvSheet.Rows[0]["CRE_TIME"].ToString()) + "<br>\r\n");
                    sbAdvSheet.Append("<b>Call Receiver:</b>" + strEmpName + "<br>\r\n");
			        sbAdvSheet.Append("	</td></tr>\r\n");
                    strHtm = "	<tr><td class=\"spec\">&nbsp;Sheet Information&nbsp;&nbsp;<b>(<a href=\"#\" onclick=\"print();\">Print</a>)</b></td></tr>\r\n";
                    sbAdvSheet.Append(strHtm);
                    sbAdvSheet.Append("	<tr><td>\r\n"); 
                    strHtm="	<table width=\"99%\" style=\"margin-left: 5px;\" border=\"0\">\r\n";
                    sbAdvSheet.Append(strHtm);

                    //Consumer
                    string strSqlCosInfo = "select * from COSINFO where ID=" + dtAdvSheet.Rows[0]["COS_ID"].ToString();

                    DataTable dtCosInfo = CMFun.GetSqlDataTable(Conn,strSqlCosInfo);
                    int iCosNum = dtCosInfo.Rows.Count;
                    string strCosName = "";
                    string strCosCountry = "";
                    string strCosProvince = "";
                    string strCosCity = "";
                    string strCosAddress = "N/A";
                    string strCosHtel = "N/A (Home)";
                    string strCosOfftel = "N/A (Office)";
                    string strCosMobile = "NA(Mobile)";
                    string strGroupID = "";
                    if (HttpContext.Current.Session["EMPINFO.GROUP_ID"] != null)
                    {
                         strGroupID = HttpContext.Current.Session["EMPINFO.GROUP_ID"].ToString();
                    }

                    if (iCosNum > 0 && strGroupID != "4" && strGroupID != "19" && strGroupID != "14")
                    {
                        strCosName = dtCosInfo.Rows[0]["NAME"].ToString();
                        strCosCountry = dtCosInfo.Rows[0]["COUNTRY"].ToString();
                        strCosProvince = dtCosInfo.Rows[0]["PROVINCE"].ToString();
                        strCosProvince = strCosCountry + " " + strCosProvince;
                        strCosCity = dtCosInfo.Rows[0]["CITY"].ToString();
                        if (dtCosInfo.Rows[0]["ADDRESS"].ToString() != "")
                        {
                            strCosAddress = dtCosInfo.Rows[0]["ADDRESS"].ToString();
                        }
                        if (dtCosInfo.Rows[0]["HTEL"].ToString() != "")
                        {
                            strCosHtel = dtCosInfo.Rows[0]["HTEL"].ToString() + "(Home)";
                        }
                        if (dtCosInfo.Rows[0]["OFFTEL"].ToString() != "")
                        {
                            strCosOfftel = dtCosInfo.Rows[0]["OFFTEL"].ToString() + "(Office)";
                        }

                        if (dtCosInfo.Rows[0]["Mobile"].ToString() != "")
                        {
                            strCosMobile = dtCosInfo.Rows[0]["Mobile"].ToString() + "(Mobile)";
                        }

                    }
                    else
                    {
                        strCosName = "******";
                        strCosAddress = "******";
                        strCosProvince = "******";
                        strCosHtel = "******";
                        strCosOfftel = "******";
                        strCosMobile = "******";

                    }
                    dtCosInfo.Dispose();

                    //Product
                    string strProName = "No Relation with Product";
                    string strProType = "N/A";
                    string strProBU = "N/A";
                    string strProPDT = "N/A";
                    string strProSeries = "N/A";
                    string strProUnit = "N/A";
                    string strProSize = "N/A";
                    string strProBpxid = "N/A";
                    string strProBarCode = "N/A";
                    string strSqlPackage = "";
                    if (dtAdvSheet.Rows[0]["PRO_ID"].ToString() != "" && dtAdvSheet.Rows[0]["PRO_ID"].ToString() != "0")
                    {

                        string strSqlPro = "select Pro.PRO_NAME,Pro.PRO_EN_NAME,Pro.UNIT,Pro.SIZE,Pro.BARCODE,PT.TYPENAME,BU.BU_NAME,PDT.PDT_NAME,PS.Series_Name from  PROINFO AS Pro LEFT OUTER JOIN  PROTYPE AS PT ON Pro.Type_ID = PT.TYPE_ID LEFT OUTER JOIN PROBU AS BU ON Pro.BU_ID = BU.BU_ID LEFT OUTER JOIN  ProductDetailType AS PDT ON Pro.Detail_Type_ID = PDT.PDT_ID LEFT OUTER JOIN  ProductSeries AS PS ON Pro.Series_ID = PS.Series_ID where Pro.PRO_ID=" + dtAdvSheet.Rows[0]["PRO_ID"].ToString();
                        //string strPro=GetStringName(Conn,strSqlPro,dtAdvSheet.Rows[0]["PRO_ID"]);
                        //if (strPro!="")
                        //{
                        //    strProName = strPro;
                        //}
                        DataTable dtProduct = CMFun.GetSqlDataTable(Conn, strSqlPro);
                        int iProNum = dtProduct.Rows.Count;
                        if (iProNum > 0)
                        {
                            strProName = dtProduct.Rows[0]["PRO_NAME"].ToString() + "(" + dtProduct.Rows[0]["PRO_EN_NAME"].ToString() + ")";
                            if (dtProduct.Rows[0]["UNIT"].ToString() != "" && dtProduct.Rows[0]["UNIT"] != null)
                            {
                                strProUnit = dtProduct.Rows[0]["UNIT"].ToString();
                            }
                            if (dtProduct.Rows[0]["SIZE"].ToString() != "" && dtProduct.Rows[0]["SIZE"] != null)
                            {
                                strProSize = dtProduct.Rows[0]["SIZE"].ToString();
                            }
                            if (dtProduct.Rows[0]["BARCODE"].ToString() != "" && dtProduct.Rows[0]["BARCODE"] != null)
                            {
                                strProBarCode = dtProduct.Rows[0]["BARCODE"].ToString();
                            }
                            if (dtProduct.Rows[0]["TYPENAME"].ToString() != "" && dtProduct.Rows[0]["TYPENAME"] != null)
                            {
                                strProType = dtProduct.Rows[0]["TYPENAME"].ToString();
                            }
                            if (dtProduct.Rows[0]["BU_NAME"].ToString() != "" && dtProduct.Rows[0]["BU_NAME"] != null)
                            {
                                strProBU = dtProduct.Rows[0]["BU_NAME"].ToString();
                            }
                            if (dtProduct.Rows[0]["PDT_NAME"].ToString() != "" && dtProduct.Rows[0]["PDT_NAME"] != null)
                            {
                                strProPDT = dtProduct.Rows[0]["PDT_NAME"].ToString();
                            }
                            if (dtProduct.Rows[0]["Series_Name"].ToString() != "" && dtProduct.Rows[0]["Series_Name"] != null)
                            {
                                strProSeries = dtProduct.Rows[0]["Series_Name"].ToString();
                            }
                        }
                        dtProduct.Dispose();
                    }
                    //ProPackage
                    if (dtAdvSheet.Rows[0]["P_ID"].ToString() != "" && dtAdvSheet.Rows[0]["P_ID"] != null)
                    {
                        strSqlPackage = "select P_ID,UNIT,SIZE,BPXID,BARCODE from PROPACKAGE where P_ID=" + dtAdvSheet.Rows[0]["P_ID"].ToString();
                        DataTable dtPackage = CMFun.GetSqlDataTable(Conn, strSqlPackage);
                        int iPackageNum = dtPackage.Rows.Count;
                        if (iPackageNum > 0)
                        {
                            if (dtPackage.Rows[0]["UNIT"].ToString() != "" && dtPackage.Rows[0]["UNIT"] != null)
                            {
                                strProUnit = dtPackage.Rows[0]["UNIT"].ToString();
                            }
                            if (dtPackage.Rows[0]["SIZE"].ToString() != "" && dtPackage.Rows[0]["SIZE"] != null)
                            {
                                strProSize = dtPackage.Rows[0]["SIZE"].ToString();
                            }
                            if (dtPackage.Rows[0]["BPXID"].ToString() != "" && dtPackage.Rows[0]["BPXID"] != null)
                            {
                                strProBpxid = dtPackage.Rows[0]["BPXID"].ToString();
                            }
                            if (dtPackage.Rows[0]["BARCODE"].ToString() != "" && dtPackage.Rows[0]["BARCODE"] != null)
                            {
                                strProBarCode = dtPackage.Rows[0]["BARCODE"].ToString();
                            }

                        }
                        dtPackage.Dispose();
                    }
                   
                    //Brand
                    string strSqlBrand = "select BRANDNAME,BRAND_EN_NAME from BRAND where BRAND_ID=";
                    string strBrandName =GetStringName(Conn,strSqlBrand,dtAdvSheet.Rows[0]["BRAND_ID"]);
                    //Category
                    string strSqlCategory = "select CATEGORY_NAME,CATEGORY_EN_NAME from CATEGORY where CATEGORY_ID=";
                    string strCategoryName = GetStringName(Conn, strSqlCategory, dtAdvSheet.Rows[0]["CATEGORY_ID"]);
                    //Recvia
                    string strSqlRecvia = "select NAME from RECVIA where RECVIA_ID=";
                    string strRecviaName = GetStringNameOne(Conn, strSqlRecvia, dtAdvSheet.Rows[0]["RECVIA_ID"]);
                    //Advway
                    string strSqlAdvway = "select NAME from ADVWAY where ADV_WAY_ID=";
                    string strReceiveFrom = GetStringNameOne(Conn, strSqlAdvway, dtAdvSheet.Rows[0]["ADV_WAY_ID"]);
                    //Proway
                    string strSqlProway = "select WAY_NAME from PROWAY where PRO_WAY_ID=";
                    string strProWay = GetStringNameOne(Conn, strSqlProway, dtAdvSheet.Rows[0]["PRO_WAY_ID"]);
                    //Complaint_Class
                    string strComplaintClass = "NA";
                    string strSqlComplaint = "select CLASS,CLASS_TIPS from COMPLAINT_CLASS where COMPLAINT_ID=";
                    string strComplaint = GetStringName(Conn, strSqlComplaint, dtAdvSheet.Rows[0]["COMPLAINT_ID"], true);
                    if (strComplaint!="")
                    {
                        strComplaintClass = strComplaint;
                    }
                    //Symptom
                    string strSqlSymptom = "select DETAIL from SYMPTOM where SYMPTOM_ID=";
                    string strSymptom = GetStringNameOne(Conn, strSqlSymptom, dtAdvSheet.Rows[0]["SYMPTOM_ID"]);
                    //AdvSheet
                    string strPurchDate = "N/A";
                    string strPurchPlace = "N/A";
                    string strPurchQuantity = "N/A";
                    string strProDate = "N/A";
                    string strProPlace = "N/A";
                    string strProBatchNo = "N/A";
                    if (dtAdvSheet.Rows[0]["PURCH_DATE"] != null && dtAdvSheet.Rows[0]["PURCH_DATE"].ToString()!="")
                    {
                        try
                        {
                            DateTime dtime = Convert.ToDateTime(dtAdvSheet.Rows[0]["PURCH_DATE"]);

                            if (dtime.Year != 1900)
                                strPurchDate = dtAdvSheet.Rows[0]["PURCH_DATE"].ToString();
                            else
                                strPurchDate = "N/A";
                        }
                        catch (Exception e) { }
                        
                    }
                    if (dtAdvSheet.Rows[0]["PURCH_PLACE"] != null && dtAdvSheet.Rows[0]["PURCH_PLACE"].ToString() != "")
                    {
                        strPurchPlace = dtAdvSheet.Rows[0]["PURCH_PLACE"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["PURCH_QUANTITY"] != null && dtAdvSheet.Rows[0]["PURCH_QUANTITY"].ToString() != "")
                    {
                        strPurchQuantity = dtAdvSheet.Rows[0]["PURCH_QUANTITY"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["PRO_DATE"] != null && dtAdvSheet.Rows[0]["PRO_DATE"].ToString() != "")
                    {
                        strProDate = dtAdvSheet.Rows[0]["PRO_DATE"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["PRO_PLACE"] != null && dtAdvSheet.Rows[0]["PRO_PLACE"].ToString() != "")
                    {
                        strProPlace = dtAdvSheet.Rows[0]["PRO_PLACE"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["BATCH_NO"] != null && dtAdvSheet.Rows[0]["BATCH_NO"].ToString() != "")
                    {
                        strProBatchNo = dtAdvSheet.Rows[0]["BATCH_NO"].ToString();
                    }
                    //Is Food Solution
                    string strIsFoodSolution = "NA";
                    string strProductPurpose = "NA";
                    string strStorageCondition = "NA";
                    string strIsAirproof = "NA";
                    string strStartUsedDate = "NA";
                    string strFoundPoblemDate = "NA";
                    string strFoundPlace = "NA";
                    if (dtAdvSheet.Rows[0]["IsFoodSolution"] != null && dtAdvSheet.Rows[0]["IsFoodSolution"].ToString() != "")
                    {
                        strIsFoodSolution = dtAdvSheet.Rows[0]["IsFoodSolution"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["ProductPurpose"] != null && dtAdvSheet.Rows[0]["ProductPurpose"].ToString() != "")
                    {
                        strProductPurpose = dtAdvSheet.Rows[0]["ProductPurpose"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["StorageCondition"] != null && dtAdvSheet.Rows[0]["StorageCondition"].ToString() != "")
                    {
                        strStorageCondition = dtAdvSheet.Rows[0]["StorageCondition"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["IsAirproof"] != null && dtAdvSheet.Rows[0]["IsAirproof"].ToString() != "")
                    {
                        strIsAirproof = dtAdvSheet.Rows[0]["IsAirproof"].ToString();
                    }
                    if (dtAdvSheet.Rows[0]["StartUsedDate"] != null && dtAdvSheet.Rows[0]["StartUsedDate"].ToString() != "")
                    {
                        strStartUsedDate = GetDateTimeToDate(dtAdvSheet.Rows[0]["StartUsedDate"].ToString());
                    }
                    if (dtAdvSheet.Rows[0]["FoundPoblemDate"] != null && dtAdvSheet.Rows[0]["FoundPoblemDate"].ToString() != "")
                    {
                        strFoundPoblemDate = GetDateTimeToDate(dtAdvSheet.Rows[0]["FoundPoblemDate"].ToString());
                    }
                    if (dtAdvSheet.Rows[0]["FoundPlace"] != null && dtAdvSheet.Rows[0]["FoundPlace"].ToString() != "")
                    {
                        strFoundPlace = dtAdvSheet.Rows[0]["FoundPlace"].ToString();
                    }
                    strHtm = "<tr><td class=\"msg\">\r\n";
                    sbAdvSheet.Append(strHtm);
                    strHtm = "<b>Receive Via:</b> &nbsp;" + strRecviaName + " &nbsp;&nbsp; <b>Receive From:</b> &nbsp;" + strReceiveFrom + " &nbsp;&nbsp; <font color=\"red\"><b>Should Finish Date:</b> &nbsp; " + dtAdvSheet.Rows[0]["SH_FINISH_DATE"].ToString() + "</font>\r\n";
                    sbAdvSheet.Append(strHtm);
                    strHtm = "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
                    sbAdvSheet.Append(strHtm);
                    //strHtm = "<b>Consumer Information:</b> &nbsp;<a href=\"#\" onclick=\"javascript:openWindow('cusinfo.asp?id=" + dtAdvSheet.Rows[0]["COS_ID"].ToString() + "','cosinfo')\">" + strCosName + "</a>\r\n";
                    strHtm = "<b>Consumer Information:</b> &nbsp;" + strCosName + "\r\n";
                    sbAdvSheet.Append(strHtm);
                    strHtm = "<img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(cus);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                    sbAdvSheet.Append(strHtm);
				
					strHtm ="<table width=\"100%\" border=\"0\" id=\"cus\" class=\"show\">\r\n"; 
                    sbAdvSheet.Append(strHtm);
					strHtm ="<tr><td class=\"msg\" width=\"25%\"><b>Country/Province/City</b></td>\r\n"; 
                    sbAdvSheet.Append(strHtm);
					strHtm ="<td class=\"msg\" width=\"75%\">"+strCosProvince+" "+strCosCity+"</td></tr>\r\n"; 
                    sbAdvSheet.Append(strHtm);
					strHtm ="<tr><td class=\"msg\" width=\"25%\"><b>Address/Zip</b></td>\r\n"; 
                    sbAdvSheet.Append(strHtm);
					strHtm ="<td class=\"msg\" width=\"75%\">"+strCosAddress+"</td></tr>\r\n"; 
                    sbAdvSheet.Append(strHtm);
					strHtm ="<tr><td class=\"msg\" width=\"25%\"><b>Communication</b></td>\r\n";  
                    sbAdvSheet.Append(strHtm);
                    strHtm = "<td class=\"msg\" width=\"75%\">" + strCosMobile + "&nbsp;&nbsp;" + strCosHtel + " &nbsp;&nbsp; " + strCosOfftel + "</td></tr>\r\n";  
                    sbAdvSheet.Append(strHtm);
					sbAdvSheet.Append(  "</table>\r\n");
			        if(dtAdvSheet.Rows[0]["PRO_ID"]!=null&&dtAdvSheet.Rows[0]["PRO_ID"].ToString()!="")
                    {
                        if (int.Parse(dtAdvSheet.Rows[0]["PRO_ID"].ToString()) != 0)
                        {
                            strHtm = "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
                            sbAdvSheet.Append(strHtm);
                            //strHtm = "<b>Prodoct Information:</b> &nbsp;<a href=\"#\" onclick=\"javascript:openWindow('proinfo.asp?pro_id=" + dtAdvSheet.Rows[0]["PRO_ID"].ToString() + "', 'proinfo')\">" + strProName + "</a>\r\n";
                            strHtm = "<b>Product Information:</b> &nbsp;" + strProName + "\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(prod);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<table width=\"100%\" border=\"0\" id=\"prod\" class=\"show\">\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Barcode/BPXID</b></td>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<td class=\"msg\" width=\"75%\">" + strProBarCode + " / " + strProBpxid + "</td></tr>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Product Type/BU</b></td>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<td class=\"msg\" width=\"75%\">" + strProType + " &nbsp;/&nbsp; " + strProBU + "</td></tr>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Category/Brand</b></td>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<td class=\"msg\" width=\"75%\">" + strCategoryName + " &nbsp;/&nbsp; " + strBrandName + "</td></tr>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Product Detail Type/Series</b></td>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<td class=\"msg\" width=\"75%\">" + strProPDT + " &nbsp;/&nbsp; " + strProSeries + "</td></tr>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Size/Unit</b></td>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<td class=\"msg\" width=\"75%\">" + strProSize + " / " + strProUnit + "</td></tr>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Batch No.</b></td>\r\n";
                            sbAdvSheet.Append(strHtm);
                            strHtm = "<td class=\"msg\" width=\"75%\">" + strProBatchNo + "</td></tr>\r\n";
                            sbAdvSheet.Append(strHtm);
                            sbAdvSheet.Append("</table>\r\n");
                            //if (strIsFoodSolution=="1")
                            //{
                            //    strHtm = "<hr size=\"1\" width=\"100%\" visable=\"false\" color=\"#0033CC\">\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<b visable=\"false\">是否Food Solution:</b> &nbsp;是\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(food);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<table width=\"100%\" border=\"0\" id=\"food\" visable=\"false\" class=\"show\">\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>产品使用方法及用途</b></td>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<td class=\"msg\" width=\"75%\">" +strProductPurpose+ "</td></tr>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>产品储藏条件</b></td>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<td class=\"msg\" width=\"75%\">" +strStorageCondition+ "</td></tr>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>产品开始使用时间</b></td>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<td class=\"msg\" width=\"75%\">" +strStartUsedDate+ "</td></tr>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>开封后,密封保存</b></td>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    if (strIsAirproof == "0")
                            //    {
                            //        strHtm = "<td class=\"msg\" width=\"75%\">否</td></tr>\r\n";
                            //        sbAdvSheet.Append(strHtm);
                            //    }
                            //    else if (strIsAirproof == "1")
                            //    {
                            //        strHtm = "<td class=\"msg\" width=\"75%\">是</td></tr>\r\n";
                            //        sbAdvSheet.Append(strHtm);
                            //    }

                            //    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>发现产品问题时间</b></td>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<td class=\"msg\" width=\"75%\">" +strFoundPoblemDate+ "</td></tr>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>发现产品问题地点</b></td>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    strHtm = "<td class=\"msg\" width=\"75%\">" +strFoundPlace + "</td></tr>\r\n";
                            //    sbAdvSheet.Append(strHtm);
                            //    sbAdvSheet.Append("</table>\r\n");
                            //}
                        }
                    
                    }
			        else
                    {
                        strHtm = "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
				        sbAdvSheet.Append(strHtm);
				        sbAdvSheet.Append( 	"<b>Prodoct Information:</b> &nbsp;"+strProName);
				    }
                    strHtm = "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
					sbAdvSheet.Append(strHtm);
                    sbAdvSheet.Append("<b>Case Information:</b>\r\n"); 
                    strHtm = "<img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(caseinfo);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                    sbAdvSheet.Append(strHtm); 
                    strHtm = "<table width=\"100%\" border=\"0\" id=\"caseinfo\" class=\"show\">\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Source From</b></td>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<td class=\"msg\" width=\"75%\">" + strProWay + "</td></tr>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Manu. Date/Place</b></td>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<td class=\"msg\" width=\"75%\">" + strProDate + " / " + strProPlace + "</td></tr>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Purchase Date/Place/Quan.</b></td>\r\n";
                    sbAdvSheet.Append(strHtm); 
                    strHtm = "<td class=\"msg\" width=\"75%\">" + strPurchDate + " / " + strPurchPlace + " / " + strPurchQuantity + "</td></tr>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Priority</b></td>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<td class=\"msg\" width=\"75%\">" + strComplaintClass + "</td></tr>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<tr><td class=\"msg\" width=\"25%\"><b>Symptom</b></td>\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm = "<td class=\"msg\" width=\"75%\">" + strSymptom + "</td></tr>\r\n";
					sbAdvSheet.Append(strHtm);
					sbAdvSheet.Append(  "</table>\r\n");

                    //AdvSheet Attach
                    string strSqlAttach="select Title,afile_name from ADV_IMAGE where ADV_ID="+strAdvId+" order by ID ";
                    DataTable dtAdvAttach=CMFun.GetSqlDataTable(Conn,strSqlAttach);
                    int iAttachNum=dtAdvAttach.Rows.Count;
                    if (iAttachNum>0)
                    {
                        int n = 1;
                        for (int i = 0; i < iAttachNum; i++)
                        {
                            string strTitle = "Advisory Sheet Attach" + n;
                            if (i == 0)
                            {
                                sbAdvSheet.Append("<b>Advisory Sheet Attachments: ");
                            }
                            if (dtAdvAttach.Rows[i]["Title"] != null && dtAdvAttach.Rows[i]["Title"].ToString() != "")
                            {
                                strTitle = dtAdvAttach.Rows[i]["Title"].ToString();
                            }
                            else
                            {
                                strTitle = dtAdvAttach.Rows[i]["afile_name"].ToString();
                            }
                            //bool bfog= CMFun.GetPicFileExtension(dtAdvAttach.Rows[i]["afile_name"].ToString());
                            //if (bfog == true)
                            //{
                            //    strHtm = "<br><img src=\"../FileUpload/AdvFile/" + dtAdvAttach.Rows[i]["afile_name"].ToString() + "\" alt="+ strTitle +">&nbsp;";
                            //}
                            //else
                            //{
                                strHtm = "<a href=\"#\" onclick=\"openWindow('../FileUpload/AdvFile/" + dtAdvAttach.Rows[i]["afile_name"].ToString() + "', 'advimg');\">[" + strTitle + "]</a>&nbsp;";
                            //}
                            sbAdvSheet.Append(strHtm);
                            n++;
                        }
                        if (n > 1)
                        {
                            strHtm = "<b><br><hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
                            sbAdvSheet.Append(strHtm);
                        }
                    }
                    dtAdvAttach.Dispose();
                    //
                    sbAdvSheet.Append(  "</td></tr>\r\n");
					sbAdvSheet.Append( "		</table>\r\n");
					sbAdvSheet.Append( "	</td></tr>\r\n");
                    strHtm= "	<tr><td class=\"spec\">&nbsp;Details</td></tr>\r\n";
					sbAdvSheet.Append(strHtm);
					sbAdvSheet.Append( "	<tr><td>\r\n");
                    strHtm= "		<table width=\"99%\" style=\"margin-left: 5px;\" border=\"0\">\r\n";
					sbAdvSheet.Append(strHtm);
                    strHtm= "<tr><td class=\"msg\"><b>Problem</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(problem);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
					sbAdvSheet.Append(strHtm); 
                    strHtm=  "<table width=\"100%\" border=\"0\" id=\"problem\" class=\"show\">\r\n";
					sbAdvSheet.Append(strHtm);

                    //Problem 
                    GetProblemComplaint(sbAdvSheet,Conn,strAdvId);

                    sbAdvSheet.Append(  "</table>\r\n");
                    strHtm=  "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
					sbAdvSheet.Append(strHtm);
                    //
                    sbAdvSheet.Append( "</td></tr>\r\n");

                    strHtm = "<tr><td class=\"msg\"><b>Details of Complaints or Inquiries</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(Details);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                    sbAdvSheet.Append(strHtm);
                    strHtm = "<table width=\"100%\" border=\"0\" id=\"Details\" class=\"show\">\r\n";
                    sbAdvSheet.Append(strHtm);

                    //Details of Complaints or Inquiries
                    strHtm = "<tr><td class=\"msg\">"+strDetail+"</td></tr>\r\n";
                    sbAdvSheet.Append(strHtm);
                    sbAdvSheet.Append("</table>\r\n");
                    //
                    sbAdvSheet.Append("</td></tr>\r\n");
				    sbAdvSheet.Append( "	</table>\r\n");
				    sbAdvSheet.Append( "	</td></tr>\r\n");
                    //DealProcess
                    string strSqlDeal="select * from DEALPROCESS where ADV_ID="+strAdvId;
                    DataTable dtDeal=CMFun.GetSqlDataTable(Conn,strSqlDeal);
                    int iDealNum=dtDeal.Rows.Count;
                    if(iDealNum>0)
                    {
                        string strResuId = dtDeal.Rows[0]["RESU_ID"].ToString();
                        strHtm= "	<tr><td class=\"spec\">&nbsp;Solution</td></tr>\r\n";
                    	sbAdvSheet.Append(strHtm);
			            sbAdvSheet.Append( "	<tr><td>\r\n");
                        strHtm= "	<table width=\"99%\" style=\"margin-left: 5px;\" border=\"0\">\r\n";
			            sbAdvSheet.Append(strHtm);
                        string strRepTime="";
                        if(dtDeal.Rows[0]["REP_TIME"]!=null&&dtDeal.Rows[0]["REP_TIME"].ToString()!="")
                        {
                            strRepTime= GetDateTimeToDate(dtDeal.Rows[0]["REP_TIME"].ToString());
                        }
                        strHtm= "<tr><td class=\"msg\">\r\n";
                        sbAdvSheet.Append(strHtm);
                        strHtm= "<b>Front</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(casfront);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
					    sbAdvSheet.Append(strHtm); 
                        strHtm= "<table width=\"100%\" border=\"0\" id=\"casfront\" class=\"show\">\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<tr><td class=\"msg\">Answer question and suggestion on " +strRepTime+ "</td></tr>\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<tr><td class=\"msg\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;By " +dtDeal.Rows[0]["REP_METHOD"]+ "</td></tr>\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<tr><td class=\"msg\">Find out solutions and contact to consumer before <font color=\"red\">" +  GetDateTimeToDate(dtAdvSheet.Rows[0]["SH_FINISH_DATE"].ToString())+ "</font></td></tr>\r\n";
					    sbAdvSheet.Append(strHtm);
					    sbAdvSheet.Append( "</table>\r\n");
                        strHtm=  "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
					    sbAdvSheet.Append(strHtm);
                        string strQaBrief="N/A";
                        string strQaReason = "N/A";
                        if(dtDeal.Rows[0]["QA_BRIEF"]!=null&&dtDeal.Rows[0]["QA_BRIEF"].ToString()!="")
                        {
                            strQaBrief=dtDeal.Rows[0]["QA_BRIEF"].ToString();
                        }
                        if (dtDeal.Rows[0]["QA_Reason"] != null && dtDeal.Rows[0]["QA_Reason"].ToString() != "")
                        {
                            strQaReason = dtDeal.Rows[0]["QA_Reason"].ToString();
                        }
                        string strQaName=GetEmpName(Conn,dtDeal.Rows[0]["QA_ID"].ToString());
                        string strCQAttach = "N/A";
                        strCQAttach = CMFun.GetCQAttach(Conn, strResuId);
                        strHtm= "<b>Send to QA and Follow up</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(casqa);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                        sbAdvSheet.Append(strHtm); 
                        strHtm= "<table width=\"100%\" border=\"0\" id=\"casqa\" class=\"show\">\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<tr><td class=\"msg\"><b>Inform QA(" +strQaName+ ") on " + GetDateTimeToDate(dtDeal.Rows[0]["INFOR_TIME_QA"].ToString())+"</b><br>\r\n";
					    sbAdvSheet.Append(strHtm);
					    sbAdvSheet.Append( "<b>Reply from QA(" +strQaName+ ") on " + GetDateTimeToDate(dtDeal.Rows[0]["REPLY_TIME_QA"].ToString())+"</b><br>\r\n");
                        sbAdvSheet.Append("<b>Investigate Result:</b><br>\r\n");
                        sbAdvSheet.Append(strQaReason + "<br>\r\n");

                        sbAdvSheet.Append( "<b>Corrective Action:</b><br>\r\n");
					    sbAdvSheet.Append( strQaBrief + "<br>\r\n");
                        sbAdvSheet.Append("<b>QA/CQA Attach:</b><br>\r\n");
                        sbAdvSheet.Append(strCQAttach + "<br>\r\n");
					    sbAdvSheet.Append( "</td></tr>\r\n");
					    sbAdvSheet.Append( "</table>\r\n");
                        strHtm=  "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
					    sbAdvSheet.Append(strHtm);

                        string strVbrief="N/A";
                        if(dtDeal.Rows[0]["V_BRIEF"]!=null&&dtDeal.Rows[0]["V_BRIEF"].ToString()!="")
                        {
                            strVbrief=dtDeal.Rows[0]["V_BRIEF"].ToString();
                        }
                        string strVisitor=GetEmpName(Conn,dtDeal.Rows[0]["VISITOR_ID"].ToString());
                        strHtm= "<b>Send to CRM Home Visitor and Follow up</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(casvisitor);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                        sbAdvSheet.Append(strHtm); 
                        strHtm= "<table width=\"100%\" border=\"0\" id=\"casvisitor\" class=\"show\">\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<tr><td class=\"msg\"><b>Inform CRM Home Visitor(" +strVisitor+ ") on " + GetDateTimeToDate(dtDeal.Rows[0]["INFOR_TIME_VISITOR"].ToString())+ "</b><br>\r\n";
					    sbAdvSheet.Append(strHtm);
					    sbAdvSheet.Append( "<b>Reply from CRM Home Visitor(" +strVisitor+ ") on " + GetDateTimeToDate(dtDeal.Rows[0]["REPLY_TIME_VISITOR"].ToString())+ "</b><br>\r\n");
					    sbAdvSheet.Append( "<b>Result:</b><br>\r\n");
					    sbAdvSheet.Append( strVbrief + "<br>\r\n");
					    sbAdvSheet.Append( "</td></tr>\r\n");
					    sbAdvSheet.Append( "</table>\r\n");
                        strHtm=  "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
					    sbAdvSheet.Append(strHtm);

                        string strOtherBrief="NA";
                        if(dtDeal.Rows[0]["OTHER_BRIEF"]!=null&&dtDeal.Rows[0]["OTHER_BRIEF"].ToString()!="")
                        {
                            strOtherBrief=dtDeal.Rows[0]["OTHER_BRIEF"].ToString();
                        }
                        string strOtherName=GetEmpName(Conn,dtDeal.Rows[0]["OTHER_ID"].ToString());
                        strHtm = "<b>Send to Other and Follow up</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(casother);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                        sbAdvSheet.Append(strHtm); 
                        strHtm= "<table width=\"100%\" border=\"0\" id=\"casother\" class=\"show\">\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<tr><td class=\"msg\"><b>Inform CRM Other(" +strOtherName+ ") on " + GetDateTimeToDate(dtDeal.Rows[0]["INFOR_TIME_OTHER"].ToString())+ "</b><br>\r\n";
					    sbAdvSheet.Append(strHtm);
					    sbAdvSheet.Append( "<b>Reply from CRM Others(" +strOtherName+ ") on " + GetDateTimeToDate(dtDeal.Rows[0]["REPLY_TIME_OTHER"].ToString())+ "</b><br>\r\n");
					    sbAdvSheet.Append( "<b>Result:</b><br>\r\n");
					    sbAdvSheet.Append( strOtherBrief + "<br>\r\n");
					    sbAdvSheet.Append( "</td></tr>\r\n");
					    sbAdvSheet.Append( "</table>\r\n");
                        strHtm=  "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
					    sbAdvSheet.Append(strHtm);

                        //Diagnosis Attach
                        //string strResuId=dtDeal.Rows[0]["RESU_ID"].ToString();
                        string strSqlDiagnosisAttach="select Title,afile_name from DIAGNOSIS_IMAGE where RESU_ID="+strResuId+" order by DIAGNOSIS_ID desc";
                        DataTable dtDiagnosisAttach=CMFun.GetSqlDataTable(Conn,strSqlDiagnosisAttach);
                        int iDiagnosisNum=dtDiagnosisAttach.Rows.Count;
                        int m=1;
                        for(int i=0;i<iDiagnosisNum;i++)
                        {
                            string strTitle="Disgnosis Attach "+m;
                            if(i==0)
                            {
                                sbAdvSheet.Append(  "<b>Disgnosis: ");
                            }
                            if(dtDiagnosisAttach.Rows[i]["Title"]!=null&&dtDiagnosisAttach.Rows[i]["Title"].ToString()!="")
                            {
                                strTitle=dtDiagnosisAttach.Rows[i]["Title"].ToString();
                            }
                            strHtm= "<a href=\"#\" onclick=\"openWindow('../FileUpload/DiagnosisFile/"+dtDiagnosisAttach.Rows[i]["afile_name"].ToString()+"', 'advimg');\">[" +strTitle+ "]</a>&nbsp;";
                            sbAdvSheet.Append(strHtm);
                            m++;
                        }
                        if(m>1)
                        {   
                            strHtm="<b><br><hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
                            sbAdvSheet.Append(strHtm);
                        }
                        dtDiagnosisAttach.Dispose();

                        //
                        strHtm= "<b>Solution Details:</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(solution);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
                        sbAdvSheet.Append(strHtm); 
                        strHtm= "<table width=\"100%\" border=\"0\" id=\"solution\" class=\"show\">\r\n";
					    sbAdvSheet.Append(strHtm);
					    sbAdvSheet.Append( dtDeal.Rows[0]["SOLUTION"].ToString()+"<br>\r\n");
					    sbAdvSheet.Append( "<b>Product Exchange Fee:</b>  "+dtDeal.Rows[0]["COST_FEE"].ToString()+"元（人民币）<br>\r\n");
                        sbAdvSheet.Append("<b>&nbsp;&nbsp; Refund:</b>  " + dtDeal.Rows[0]["RETURN_FEE"].ToString() + "元（人民币）<br>\r\n");
                        sbAdvSheet.Append("<b>&nbsp;&nbsp; Medical  Fee:</b>  " + dtDeal.Rows[0]["MEDICAL_FEE"].ToString() + "元（人民币）<br>\r\n");
                        sbAdvSheet.Append("<b>&nbsp;&nbsp; Delay work  Fee:</b>  " + dtDeal.Rows[0]["DELAYWORK_FEE"].ToString() + "元（人民币）<br>\r\n");
                        sbAdvSheet.Append("<b>&nbsp;&nbsp; Traffic  Fee:</b>  " + dtDeal.Rows[0]["TRAFFIC_FEE"].ToString() + "元（人民币）<br>\r\n");
                        sbAdvSheet.Append("<b>&nbsp;&nbsp; Other  Fee:</b>  " + dtDeal.Rows[0]["OTHERS_FEE"].ToString() + "元（人民币）<br>\r\n");
					    sbAdvSheet.Append( "<b>Compensation Fee:</b>  "+dtDeal.Rows[0]["TREAT_FEE"].ToString()+"元（人民币）\r\n");
					    sbAdvSheet.Append( "</table>\r\n");
                        strHtm=  "<hr size=\"1\" width=\"100%\" color=\"#0033CC\">\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<b>Notes/Suggestion</b><img src=\"../images/tri.gif\" border=\"0\" onclick=\"showTable(notes);\" width=\"14\" height=\"12\" style=\"cursor: hand; \"><br>\r\n";
					    sbAdvSheet.Append(strHtm);
                        strHtm= "<table width=\"100%\" border=\"0\" id=\"notes\" class=\"show\">\r\n";
					    sbAdvSheet.Append(strHtm);
                        
                        string strNote="N/A";
                       
                        sbAdvSheet.Append(strNote+"\r\n");
                        sbAdvSheet.Append( "</table>\r\n");
                    }
                    dtDeal.Dispose();
                    sbAdvSheet.Append( "</td></tr>\r\n");
				    sbAdvSheet.Append( "</table>\r\n");
				    sbAdvSheet.Append( "</td></tr>\r\n");
                }
                sbAdvSheet.Append( "</table>\r\n");
                dtAdvSheet.Dispose();
                sbAdvSheet.Append( "</body></html>\r\n");
                strHtm = sbAdvSheet.ToString();
            }
            FileHelper.CreateFile("e:\\sheets\\" + strAdvId + ".htm", Encoding.UTF8.GetBytes(strHtm));
            
            return strHtm;
        }
        #endregion 
        #region 获取Sql两个字段衔接的单一字符串
        private static string GetStringName(SqlConnection Conn,string strSql,object objName)
        {
            string strName = "";
            if (objName.ToString() != "" && objName != null)
            {
                string strSqlName = strSql + objName.ToString();
                DataTable dtName = CMFun.GetSqlDataTable(Conn, strSqlName);
                int iNum = dtName.Rows.Count;
                if (iNum > 0)
                {
                    strName = dtName.Rows[0][0].ToString() + "(" + dtName.Rows[0][1].ToString() + ")";
                }
                dtName.Dispose();
            }
            return strName;
        }
        private static string GetStringName(SqlConnection Conn, string strSql, object objName,bool bIsRedFont)
        {
            string strName = "";
            if (objName.ToString() != "" && objName != null)
            {
                string strSqlName = strSql + objName.ToString();
                DataTable dtName = CMFun.GetSqlDataTable(Conn, strSqlName);
                int iNum = dtName.Rows.Count;
                if (iNum > 0)
                {
                    string strRed="";
                    if (bIsRedFont == true)
                    {
                        strRed = "<font color=\"red\">["+dtName.Rows[0][0].ToString()+"]</font>";
                    }
                    else
                    {
                        strRed = dtName.Rows[0][0].ToString();
                    }
                    
                    strName = strRed + dtName.Rows[0][1].ToString();
                }
                dtName.Dispose();
            }
            return strName;
        }
        #endregion
        #region 获取Sql单一字段返回的单一字符串
        private static string GetStringNameOne(SqlConnection Conn, string strSql, object objName)
        {
            string strName = "";
            if (objName.ToString() != "" && objName != null)
            {
                string strSqlName = strSql + objName.ToString();
                DataTable dtName = CMFun.GetSqlDataTable(Conn, strSqlName);
                int iNum = dtName.Rows.Count;
                if (iNum > 0)
                {
                    strName = dtName.Rows[0][0].ToString();
                }
                dtName.Dispose();
            }
            return strName;
        }
        #endregion
        #region 获取Problem投诉类型并返回输出字符串
        private static void GetProblemComplaint(StringBuilder sbName, SqlConnection Conn, string strAdvId)
        {
            string strSqlProblem = "select ADV_KIND_ID from PROBLEM_DETAILS where ADV_ID="+strAdvId;
            DataTable dtProblem = CMFun.GetSqlDataTable(Conn, strSqlProblem);
            int iProblemNum = dtProblem.Rows.Count;
            string strAdvkindId = "";
            for (int i = 0; i < iProblemNum;i++)
            {
                strAdvkindId = dtProblem.Rows[i][0].ToString();
                string strAdvKindName = "";
                GetAdvKindName(Conn, strAdvkindId, ref strAdvKindName);
                string strkind="<tr><td class=\"msg\" >" + strAdvKindName + "</td></tr>\r\n";
                sbName.Append(strkind);
            }
            dtProblem.Dispose();
        }
        #endregion
        #region  获取AdvKindName
        private static void GetAdvKindName(SqlConnection Conn, string strAdvKindId, ref string strAdvKindName)
        {
            int iLayout = 0;
            string strPID="";
            string strSqlAdvKind = "select P_ID,LAYOUT_ID,KIND_NAME,EN_KIND_NAME from ADVKIND where ADV_KIND_ID="+strAdvKindId;
            DataTable dtAdvKind = CMFun.GetSqlDataTable(Conn, strSqlAdvKind);
            int iAdvKindNum = dtAdvKind.Rows.Count;
            for (int i = 0; i < iAdvKindNum;i++)
            {
                strPID = dtAdvKind.Rows[i][0].ToString();
                iLayout = int.Parse(dtAdvKind.Rows[i][1].ToString()); 
                if (iLayout>1)
                {
                    if (strAdvKindName!="")
                    {
                        strAdvKindName = " / " + strAdvKindName;
                    }
                    if(iLayout==2)
                    {
                        strAdvKindName = "<b>" + dtAdvKind.Rows[i][2].ToString() + "(" + dtAdvKind.Rows[i][3].ToString() + ")</b>" + strAdvKindName;
                    }
                    else
                    {
                        strAdvKindName = dtAdvKind.Rows[i][2].ToString() + "(" + dtAdvKind.Rows[i][3].ToString() + ")" + strAdvKindName;
                    }
                   iLayout--;
                   GetAdvKindName(Conn,strPID,ref strAdvKindName);
                } 
                
            }
            dtAdvKind.Dispose();

        }
        #endregion
        #region  日期格式化
        private static string GetDateTimeToDate(string strDateTime)
        {
            string strDate = "NA";
            strDateTime = strDateTime.Trim();
            if (strDateTime != "" && strDateTime.Contains("1900/1/1 0:00:00") !=true )
            {  
                DateTime dtDate = DateTime.Parse(strDateTime);
                strDate = dtDate.ToLongDateString();
            }
            return strDate;
        }
        #endregion
        #region 获取投诉单的状态标示
        private static string GetSheetStatus(string strStatus)
        {
            string strSign = "";
            switch (strStatus)
            {
                case "U":
                    strSign = "Unfinished";
                    break;
                case "C":
                    strSign = "Finished";
                    break;
                default:
                    strSign = strStatus;
                    break;
            }
            return strSign;
        }
         #endregion
        #region 获取登录者或投诉单记录者
        public static string GetEmpName(SqlConnection Conn, string strEmpId)
        {
            string strEmpName = "";
            string strSqlEmp = "";
            strEmpId = CMFun.GetDelCharString(strEmpId);
            if (strEmpId != "" && strEmpId!="0")
            {
                strSqlEmp = "select EMP_NAME from EMPINFO where EMP_ID=" + strEmpId;
                strEmpName = CMFun.GetSqlObject(Conn, strSqlEmp).ToString();
            }
            return strEmpName;
        }
        #endregion
        #region 获取AdvSheet的Details
        public static string GetAdvSheetDetails(string strDetail)
        {
            string strNewDetail = "";
            strDetail = strDetail.Trim();
            if (strDetail != "")
            {
                bool bflag = strDetail.Contains("<sheetdetails");
                bool bflog = strDetail.Contains("<details>");
                if (bflag == true)
                {
                    if (bflog == true)
                    {
                        int iNum = strDetail.IndexOf("<details>")+9;
                        int iLastNum = strDetail.IndexOf("</details>");
                        int iLen = iLastNum - iNum;
                        strNewDetail = strDetail.Substring(iNum, iLen);
                    }
                }
                else
                {
                    strNewDetail = strDetail;
                }

            }
            return strNewDetail;
        }
        #endregion
             
    }



}
