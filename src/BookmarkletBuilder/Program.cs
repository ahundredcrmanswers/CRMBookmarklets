using Microsoft.Ajax.Utilities;
using System;//
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;

namespace BookmarkletBuilder
{
    public class Bookmarklet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string javascript { get; set; }

    }

    class Program
    {
        public static string InnerXml(XElement element)
        {
            StringBuilder innerXml = new StringBuilder();
            element.Nodes().ToList().ForEach(node => innerXml.Append(node.ToString()));
            return innerXml.ToString();
        }

        static void Main(string[] args)
        {
            Minifier minifier = new Microsoft.Ajax.Utilities.Minifier();
            CodeSettings cs = new CodeSettings();
            cs.QuoteObjectLiteralProperties = true;
            cs.PreserveImportantComments = false;
            
            

            string bookmarkletMainJS = System.IO.File.ReadAllText(Properties.Settings.Default.BookmarkletMain);

            string htmlTemplate = System.IO.File.ReadAllText(Properties.Settings.Default.BookmarkletHtmlTemplate);

            List<Bookmarklet> bookmarklets = new List<Bookmarklet>();

            foreach (string file in System.IO.Directory.GetFiles(Properties.Settings.Default.BookmarkletsFolder))
            {
                FileInfo fi = new FileInfo(file);

                if (fi.Extension.ToLower() != ".js")
                {
                    continue;
                }

                Bookmarklet bookmarklet = new Bookmarklet();

                bookmarklet.Name = fi.Name.Replace(".js", "");

                bookmarklet.javascript = System.IO.File.ReadAllText(file);

                int firstIndex = bookmarklet.javascript.IndexOf("<BookmarkletInfo>");
                int lastindex = bookmarklet.javascript.LastIndexOf("</BookmarkletInfo>") + ("</BookmarkletInfo>").Length;
                if (firstIndex > 0)
                {
                    string bookmarkletInfo = bookmarklet.javascript.Substring(firstIndex, lastindex - firstIndex);
                    XDocument doc = XDocument.Parse(bookmarkletInfo);
                    var nameNode = doc.XPathSelectElement("/BookmarkletInfo/Name");
                    if (nameNode != null)
                    {
                        bookmarklet.Name = nameNode.Value.ToString().Trim();
                    }

                    var descriptionNode = doc.XPathSelectElement("/BookmarkletInfo/Description");
                    if (descriptionNode != null)
                    {
                        bookmarklet.Description = InnerXml(descriptionNode).Trim();
                    }
                }
                // insert the bookmarklet javascript into the bookmarklet Main
                bookmarklet.javascript = bookmarkletMainJS.Replace("//[[Bookmarklet-Code-Inserted-Here]]", bookmarklet.javascript);

                bookmarklets.Add(bookmarklet);
            }

            string bookmarkletHtml = "";

            foreach (var bookmarklet in bookmarklets)
            {
                //bookmarkletHtml += "<a href='javascript:" + HttpUtility.JavaScriptStringEncode(jsMinifer.Compress(kvp.Value)) + "'>" + kvp.Key + "</a>" + Environment.NewLine;
                bookmarkletHtml += "<p>";
                bookmarkletHtml += "<a href=\"javascript:" + minifier.MinifyJavaScript(bookmarklet.javascript, cs).Replace("'", "\\'").Replace("\"", "'") + "\">" + bookmarklet.Name + "</a>" + Environment.NewLine;
                if (!string.IsNullOrEmpty(bookmarklet.Description))
                {
                    bookmarkletHtml += "<br/>";
                    bookmarkletHtml += bookmarklet.Description ;
                }
                bookmarkletHtml += "</p>";
            }

            string html = htmlTemplate.Replace("<!--Bookmarklets-->", bookmarkletHtml);

            File.WriteAllText(Properties.Settings.Default.BookmarkletHtmlOutput, html);
        }
    }
}
