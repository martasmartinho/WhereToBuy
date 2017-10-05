using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace WhereToBuy.utils.Helpers
{
    public class XSLTManagement
    {
        private XPathDocument xPathDocument;
        private XslCompiledTransform xslt;

        public XSLTManagement(string xml, string styleSheetFile)
        {
            this.xPathDocument = new XPathDocument((TextReader)new StringReader(xml));
            this.xslt = new XslCompiledTransform(true);
            this.xslt.Load(styleSheetFile);
        }

        public XSLTManagement(XmlReader readerXml, XmlReader readerStyleSheet)
        {
            this.xPathDocument = new XPathDocument(readerXml);
            this.xslt = new XslCompiledTransform(true);
            this.xslt.Load(readerStyleSheet);
        }

        public string GetOuput()
        {
            string @string;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter results = XmlWriter.Create((Stream)memoryStream, this.xslt.OutputSettings))
                    this.xslt.Transform((IXPathNavigable)this.xPathDocument, results);
                @string = this.xslt.OutputSettings.Encoding.GetString(memoryStream.ToArray());
            }
            return @string;
        }

        public string GetOuput(Dictionary<string, string> args)
        {
            XsltArgumentList arguments = new XsltArgumentList();
            foreach (KeyValuePair<string, string> keyValuePair in args)
                arguments.AddParam(keyValuePair.Key, "", (object)keyValuePair.Value);
            string @string;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter results = XmlWriter.Create((Stream)memoryStream, this.xslt.OutputSettings))
                    this.xslt.Transform((IXPathNavigable)this.xPathDocument, arguments, results);
                @string = this.xslt.OutputSettings.Encoding.GetString(memoryStream.ToArray());
            }
            return @string;
        }
    }
}
