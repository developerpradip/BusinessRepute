using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppHelper
{
    public class Constants
    {

        public static string BLANK_BUSINESS_NAME = "Business name is empty";
        public static string BLANK_BUSS_FIRST_NAME = "Business owner first name is empty";
        public static string BLANK_SECURITY_QUE_1= "Security Question 1 is empty";
        public static string BLANK_SECURITY_ANS_1 = "Answer for security question 1 is empty";
        public static string BLANK_SECURITY_QUE_2 = "Security Question 2 is empty";
        public static string BLANK_SECURITY_ANS_2 = "Answer for security question 2 is empty";

        public static string BLANK_USER_NAME = "User name is empty";
        public static string BLANK_PASSWORD = "Password is empty";
        public static string BLANK_CONFIRM_PASSWORD = "Confirm password is empty";
        public static string PASSWORDS_NOT_SAME = "Password and confirm password are not same";
        public static string BLANK_EMAIL = "Email address is empty";
        public static string INVALID_EMAIL = "Email address is invalid";

        public static string BLANK_SECURITY_QUESTION = "Security question is blank";
        public static string BLANK_SECURITY_ANSWER = "Security answer is blank";
        public static string DUPLICATE_USERNAME = "This username is already taken, please use another username";

        public static string BLANK_PATIENT_NAME = "Patient name is empty";
        public static string BLANK_PATIENT_EMAIL_PHONE = "Please provide patient contact information";

        public static string SP_USER_INSERT_UPDATE = "user_insert_update";
        public static string SP_USERDETAILS_SELECT = "USERDETAILS_SELECT";
        public static string SP_HouzzMainSearch_SELECT = "HouzzMainSearch_SELECT";
        //public static string SP_MainSearch_INSERT = "MainSearch_INSERT";
        public static string SP_HouzzMainSearch_INSERT = "HouzzMainSearch_INSERT";
        public static string SP_HouzzBusinessData_DELETE = "HouzzBusinessData_DELETE";
        public static string SP_HouzzMAINSEARCH_SEARCHFINISHED_UPDATE = "HouzzMAINSEARCH_SEARCHFINISHED_UPDATE";
        public static string SP_HouzzMainSearch_BYUSERID_SELECT = "HouzzMainSearch_BYUSERID_SELECT";
        public static string SP_HouzzMainSearch_BYSEARCHID_SELECT = "HouzzMainSearch_BYSEARCHID_SELECT";

        public static string SP_HouzzBUSINESSDATA_SELECT = "HouzzBUSINESSDATA_SELECT";
        public static string SP_DailyCounter_SELECT = "DailyCounter_SELECT";

        public static string SP_DailyCounter_INSERT_UPDATE = "DailyCounter_INSERT_UPDATE";
        public static string SP_HouzzMainSearch_SEARCH_SELECT = "HouzzMainSearch_SEARCH_SELECT";
        public static string SP_GetCurrentOffsetValue = "GetCurrentOffsetValue";

        public static string SP_HouzzMainSearch_Update = "HouzzMainSearch_Update";
        public static string SP_HouzzMAINSEARCH_Results_UPDATE = "HouzzMAINSEARCH_Results_UPDATE";
        public static string SP_HouzzBusinessData_INSERT = "HouzzBusinessData_INSERT";
        public static string SP_HouzzBusinessData_EMAIL_S = "HouzzBusinessData_EMAIL_S";
        public static string SP_HouzzBusinessData_EMAIL_U = "HouzzBusinessData_EMAIL_U";
        public static string SP_HouzzBusinessData_PendingRecord_S = "HouzzBusinessData_PendingRecord_S";
        public static string SP_HouzzBusinessData_U = "HouzzBusinessData_U";
        
        public static string SP_USEREMAIL_SELECT = "USEREMAIL_SELECT";
        public static string SP_USER_SELECT = "USER_SELECT";
        public static string SP_CHECK_DUPLICATE_USERNAME = "CHECK_DUPLICATE_USERNAME";
        public static string SP_USERDETAILS_USER_ACTIONS = "USERDETAILS_USER_ACTIONS";
        public static string SP_USERDETAILS_CHANGE_PASSWORD = "USERDETAILS_CHANGE_PASSWORD";
        public static string SP_USERDETAILS_ALL_INFO = "USERDETAILS_ALL_INFO";
        public static string SP_USERDETAILS_ACTIVATION_INSERT_RESET = "USERDETAILS_ACTIVATION_INSERT_RESET";
        
        public static string SP_ACCESSTOKEN_SELECT = "ACCESSTOKEN_SELECT";
        public static string SP_ACCESSTOKEN_INSERT = "ACCESSTOKEN_INSERT";
        public static string SP_ACCESSTOKEN_DELETE = "ACCESSTOKEN_DELETE";
        public static string SP_PATIENT_INSERT_UPDATE = "SP_PATIENT_INSERT_UPDATE";
        public static string SP_TREATMENTT_INSERT = "SP_TREATMENTT_INSERT";
        public static string SP_PATIENT_LIST = "SP_PATIENT_LIST";
        public static string SP_BUSINESS_DETAILS_S = "BUSINESS_DETAILS_S";
        public static string SP_FUTURE_MONTHLY_INCOME = "SP_FUTURE_MONTHLY_INCOME";
        public static string SP_CONSULTATIONDETALS_I = "SP_CONSULTATIONDETALS_I";
        public static string SP_Status_S = "SP_Status_S";
        public static string TREATMENT_Status_U = "TREATMENT_Status_U";
        public static string SP_FeedbackDetails_I = "SP_FeedbackDetails_I";
        public static string SP_BusinessRole_S = "SP_BusinessRole_S";
        public static string SP_USER_INFO_IU = "USER_IU";
        public static string SP_USER_INFO_S = "USER_S";
        public static string SP_BUSINESS_INSERT_UPDATE = "SP_BUSINESS_INSERT_UPDATE";
        public static string SP_BUSINESSID_FROM_USERID_S = "SP_BUSINESSID_FROM_USERID_S";
        public static string SP_BusinessUsers_S = "SP_BusinessUsers_S";
        public static string SP_USER_INFO_D = "SP_USER_INFO_D";
        public static string SP_BusinessSettings_IU = "SP_BusinessSettings_IU";
        public static string SP_BusinessSettings_S = "SP_BusinessSettings_S";
        public static string SP_BusinessSettings_D = "SP_BusinessSettings_D";
        public static string SP_PATIENT_TREATMENT_S = "SP_PATIENT_TREATMENT_S";
        public static string SP_CONSULTATIONDETALS_S = "SP_CONSULTATIONDETALS_S";
        public static string SP_GET_PATIENT_USERID = "SP_GET_PATIENT_USERID";
        public static string SP_GET_TREATMENTTYPEID = "SP_GET_TREATMENTTYPEID";
        public static string SP_GET_TREATMENTTYPE_NAME = "SP_GET_TREATMENTTYPE_NAME";
        public static string SP_RPT_CONVERSION_REPORT = "SP_RPT_CONVERSION_REPORT";
        public static string SP_BusinessType_S = "SP_BusinessType_S";
        public static string SP_PATIENT_TREATMENT_D = "SP_PATIENT_TREATMENT_D";

        public static string YELP_SITE_NANE = "YELP";
        public static string FACEBOOK_SITE_NAME = "FACEBOOK";
        public static string HOUZZ_SITE_NANE = "HOUZZ";
        
        //userCodeType user activation code in table user_details
        //0=> activated   activation
        //1=> user registration completed but not activated
        //2-> reset password

        //userstatus in table user_details
        //0=> Deleted/not Active
        //1 => User is successfully created and successfully activated/ user successfully reset the password 
        //2 => User is successfully created but waiting for actiavtion
        //3 => Reset password request is successfully sent, User needs to reset the password
        //


    }
}