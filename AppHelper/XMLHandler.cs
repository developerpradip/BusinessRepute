using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;

namespace AppHelper
{
    public class XMLHandler
    {
        string _xmldataFolder;
        XmlDocument xmldoc =null;
        public XMLHandler(string filePath)
        {
            try
            {
                _xmldataFolder = ConfigurationSettings.AppSettings["XMLDATAFOLDERNAME"];
            }
            catch (Exception ex)
            {
                _xmldataFolder = "XML";
                YelpTrace.Write(ex);
            }
            xmldoc = new XmlDocument();
            xmldoc.Load(_xmldataFolder + filePath);
        }

        public XmlNode GetXMLNode(string nodeType)
        {
            XmlNode node = xmldoc.SelectSingleNode("/emails/"+ nodeType);
            return node;
        }

        public string GetSubject(XmlNode node)
        {
            XmlNode xmlNode = node.SelectSingleNode("subject");
            string subject = xmlNode.InnerText;

            return subject;
        }

        public string GetMessageBody(XmlNode node)
        {
            XmlNode xmlNode = node.SelectSingleNode("messageBody");
            string messageBody = xmlNode.InnerText;

            return messageBody;
        }

    }
}