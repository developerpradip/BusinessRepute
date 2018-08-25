using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using log4net;

namespace AppHelper
{
    public class YelpTrace
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Write(string message)
        {
            if(log != null)
            {
                log.Info(message);
            }
        }
        public static void Write(Exception ex)
        {
            
            string formattedMessage = string.Empty;
            if (ex == null || string.IsNullOrEmpty(ex.StackTrace))
            {
                formattedMessage = string.Empty;
            }
            else
            {
                formattedMessage = string.Format("\nMessage {0} \n {1} \n StackTrace{2}", ex.Message + Environment.NewLine, ex.Message, ex.StackTrace);
            }

            if (log != null)
            {
                log.Info(formattedMessage, ex);
            }
        }
    }


}
