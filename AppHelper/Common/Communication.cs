using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace AppHelper.Common
{
    public class Communication
    {
        public static bool SendMail(string toEmailId, string filePath, string subject, string messagebody, string bussName, out Exception exOut)
        {
            string fromEmailId = string.Empty;
            string fromPass = string.Empty; 
            string smtpServer = string.Empty;
            int smtpPortNumber = 0;
            
            bool bReturn = true;
            exOut = null;
            try
            {
                try
                {
                    fromEmailId = ConfigurationSettings.AppSettings["FROM_EMAIL_ID"];
                }
                catch (Exception ex)
                {
                    fromEmailId = @"developerpradip@gmail.com";
                    YelpTrace.Write(ex);
                }

                try
                {
                    smtpServer = ConfigurationSettings.AppSettings["SMTPSERVER"];
                }
                catch (Exception ex)
                {
                    smtpServer = @"smtp.gmail.com";
                    YelpTrace.Write(ex);
                }

                try
                {
                    string strsmtpPortNumber = ConfigurationSettings.AppSettings["SMTPPORT"];
                    Int32.TryParse(strsmtpPortNumber, out smtpPortNumber);
                }
                catch (Exception ex)
                {
                    smtpPortNumber = 587;
                    YelpTrace.Write(ex);
                }

                try
                {
                    fromPass = ConfigurationSettings.AppSettings["EMAIL_PASSWORD"];
                }
                catch (Exception ex)
                {

                    YelpTrace.Write(ex);
                }
                



                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(fromEmailId, bussName);
                msg.To.Add(toEmailId);  // Add a new recipient to our msg.
                msg.Subject = subject;    // Assign the subject of our message.
                msg.Body = messagebody;
                msg.IsBodyHtml = true;
                
                //Add attachments to the mail
                if (File.Exists(filePath))
                {
                    Attachment attachment = new Attachment(filePath);
                    msg.Attachments.Add(attachment);
                }
                
                //create credentials and send mail
                //if (msg.Attachments.Count > 0)
                {
                    if (smtpPortNumber > 0)
                    {
                        using (SmtpClient client = new SmtpClient(smtpServer, smtpPortNumber))
                        {
                            NetworkCredential cred = new NetworkCredential(fromEmailId, fromPass);
#if DEBUG
                            //for gmail local
                            client.EnableSsl = true;
                            client.UseDefaultCredentials = false;
                            client.Host = smtpServer;
                            client.Credentials = cred; // Send our account login details to the client.
                            client.Port = 587;//smtpPortNumber;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
#else
                            //client.EnableSsl = true;
                            //client.UseDefaultCredentials = false; 
                            //client.Host = smtpServer;
                            client.Credentials = cred; // Send our account login details to the client.
                            //client.Port = 465;//smtpPortNumber;
                            client.Port = smtpPortNumber;
                            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
#endif
                            client.Send(msg);  
                        }
                    }
                    else
                    {
                        using (SmtpClient client = new SmtpClient(smtpServer))
                        {

                            NetworkCredential cred = new NetworkCredential(fromEmailId, fromPass);
                            //client.EnableSsl = true;
                            //client.UseDefaultCredentials = false; 
                            //client.Host = smtpServer;
                            client.Credentials = cred; // Send our account login details to the client.
                            //client.Port = 465;//smtpPortNumber;
                            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.Send(msg);          // Send our email.
                        }
                    }
                }
            }
            catch (System.Net.Mail.SmtpException smtpex)
            {
                exOut = smtpex;
                bReturn = false;
                YelpTrace.Write(smtpex);
            }
            catch (Exception ex)
            {
                exOut = ex;
                bReturn = false;
                //System.Windows.Forms.MessageBox.Show("in SendMail" + ex.Message + ex.InnerException);
                YelpTrace.Write(ex);
            }

            return bReturn;
        }

    }
}
