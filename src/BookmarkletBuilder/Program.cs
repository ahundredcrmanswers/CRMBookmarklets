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
    /// <summary>
    /// Bookmarklet Information class
    /// </summary>
    public class Bookmarklet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string javascript { get; set; }

    }

    /// <summary>
    /// Class for the main program execution.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Extract the inner xml of the xml element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string InnerXml(XElement element)
        {
            StringBuilder innerXml = new StringBuilder();
            element.Nodes().ToList().ForEach(node => innerXml.Append(node.ToString()));
            return innerXml.ToString();
        }


        /// <summary>
        /// Main Method (Entry point to the program).
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            /// instantiate a code settings object.
            CodeSettings cs = new CodeSettings();
            cs.QuoteObjectLiteralProperties = true;
            cs.PreserveImportantComments = false;

            /// Extract the bookmarklet wrapper javascript from the js wrapper file.  
            /// This file is the defines the bookmarklet javascript entry point and wraps each bookmarklet within this javascript.
            string bookmarkletMainJSWrapper = System.IO.File.ReadAllText(Properties.Settings.Default.BookmarkletMainJSWrapper);

            /// Read all the HTML Template file for adding the bookmarklets as links into.
            string htmlTemplate = System.IO.File.ReadAllText(Properties.Settings.Default.BookmarkletHtmlTemplate);

            string importableTemplate = System.IO.File.ReadAllText(Properties.Settings.Default.BookmarkletHtmlImportableTemplate);

            /// A list of all the bookarklets from the bookmarklets folder
            List<Bookmarklet> bookmarklets = new List<Bookmarklet>();

            #region Load all bookmarklets
            /// For each bookmarklet js in the bookmarklets folder
            foreach (string file in System.IO.Directory.GetFiles(Properties.Settings.Default.BookmarkletsFolder))
            {
                /// get the file info
                FileInfo fi = new FileInfo(file);

                // if the file is not a js file skip
                if (fi.Extension.ToLower() != ".js")
                {
                    continue;
                }

                /// Instantite the bookmarklet object populating all the properties
                Bookmarklet bookmarklet = new Bookmarklet();

                bookmarklet.Name = fi.Name.Replace(".js", "");

                bookmarklet.javascript = System.IO.File.ReadAllText(file);


                /// Extract the bookmarklet info from the xml documentation notation
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
                /// update the bookmarklet javascript by inserting it into the main wrapper js
                /// witha  replace of "//[[Bookmarklet-Code-Inserted-Here]]" with the bookmarklet javascript
                bookmarklet.javascript = bookmarkletMainJSWrapper.Replace("//[[Bookmarklet-Code-Inserted-Here]]", bookmarklet.javascript);

                // add it to the list of bookmarklets.
                bookmarklets.Add(bookmarklet);
            }

            #endregion Load all bookmarklets


            #region output all bookmarklets into the html output file based on the template.
            string bookmarkletHtml = "";
            string bookmarkletImport = "";
            ///Instantiate a minifer instance
            Minifier minifier = new Microsoft.Ajax.Utilities.Minifier();

            foreach (var bookmarklet in bookmarklets)
            {
                //bookmarkletHtml += "<a href='javascript:" + HttpUtility.JavaScriptStringEncode(jsMinifer.Compress(kvp.Value)) + "'>" + kvp.Key + "</a>" + Environment.NewLine;
                bookmarkletHtml += "<p>";
                /// append the bookmarklet js minified.  Replace all ' with \\' to escape any js quotes.
                bookmarkletHtml += "<a href=\"javascript:" + minifier.MinifyJavaScript(bookmarklet.javascript, cs).Replace("'", "\\'").Replace("\"", "'") + "\">" + bookmarklet.Name + "</a>" + Environment.NewLine;
                if (!string.IsNullOrEmpty(bookmarklet.Description))
                {
                    bookmarkletHtml += "<br/>";
                    bookmarkletHtml += bookmarklet.Description;
                }
                bookmarkletHtml += "</p>";

                /// append the bookmarklet js minified.  Replace all ' with \\' to escape any js quotes.
                bookmarkletImport += "\t\t<DT><A HREF=\"javascript:" + minifier.MinifyJavaScript(bookmarklet.javascript, cs).Replace("'", "\\'").Replace("\"", "'") + "\">" + bookmarklet.Name + "</A>" + Environment.NewLine;
                
            }

            string html = htmlTemplate.Replace("<!--Bookmarklets-->", bookmarkletHtml);
            /// write the results to the html output file
            File.WriteAllText(Properties.Settings.Default.BookmarkletHtmlOutput, html);

            html = importableTemplate.Replace("<!--Bookmarklets-->", bookmarkletImport);
            /// write the results to the html output file
            File.WriteAllText(Properties.Settings.Default.BookmarkletImportOutput, html);
            
            #endregion output all bookmarklets into the html output file based on the template.
        }
    }
}
