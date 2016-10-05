using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using UnileverCAS.UnileverFun;
using System.Collections.Generic;



namespace UnileverCAS.UnileverFun
{
    public static class CMConst
    {
        public const int iPageSize = 25;  //Ò³Êý¾Ý
        public const string IVRPath = "Ftp://tech:1234@156.5.173.10:21";
        public const int userIllegalUser = -1;
        public const int userGroupFrontEnd = 1;
        public const int userGroupCommon = 2;
        public const int userGroupVisitor = 3;
        public const int userGroupQA = 4;
        public const int userGroupManager = 5;
        public const int userGroupInfor = 6;
        public const int userGroupQuestionnaire=7;
        public const int userGroupInvestigate= 8;
        public const int userGroupAdmin = 10;
        public const int userGroupCustomer=11;

        public const string userGroupFrontEndString = "Hotline Operator";
        public const string userGroupCommonString = "Common User";
        public const string userGroupVisitorString = "Consumer Home Visitor";
        public const string userGroupQAString = "CQA";
        public const string userGroupManagerString = "Management Team";
        public const string userGroupInforString = "Information Manager";
        public const string userGroupAdminString = "Administrator";
        public const string userGroupExpiredString = "Expired User";
        public const string userGroupQuestionnaireString = "Questionnaire Manager";
        public const string userGroupInvestigateString = "Investigate User";
        public const string userGroupUnknownString = "Unknown User";
        public const string userAdminLoginName = "superadmin";
        public const string userGroupCustomerString="Customer Sqler";


        public static List<string> advway = new List<string>();
        public static List<string> Recvia = new List<string>(); 
            


        public static DropDownList ddl = new DropDownList();

        public static DataTable dtRecvia = new DataTable();

        
    }
}
