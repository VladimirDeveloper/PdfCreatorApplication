using System;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;

namespace PdfCreatorApplication.Core.BusinessLogic.Export.Exporters
{
    /// <summary>
    /// 
    /// </summary>
    public class PdfExporter : IExporter
    {
        /// <summary>
        /// Gets or sets the document body.
        /// </summary>
        /// <value>
        /// The document body.
        /// </value>
        public string DocumentBody { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfExporter"/> class.
        /// </summary>
        public PdfExporter()
        {

        }

        /// <summary>
        /// Do exports
        /// </summary>
        /// <remarks>
        /// Code of this method was taken from this article
        /// https://rupertmaier.wordpress.com/2014/08/22/creating-a-pdf-with-an-image-in-itextsharp/
        /// Many thanks to Rupert Maier
        /// </remarks>
        /// <returns></returns>
        public byte[] Export()
        {
            using (var stream = new MemoryStream())
            {
                using (var doc = new Document(PageSize.A4))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {
                            writer.CloseStream = false;
                            doc.Open();

                            var tagProcessors = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
                            tagProcessors.RemoveProcessor(HTML.Tag.IMG);
                            tagProcessors.AddProcessor(HTML.Tag.IMG, new ImageTagProcessor());

                            var cssFiles = new CssFilesImpl();
                            cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());
                            var cssResolver = new StyleAttrCSSResolver(cssFiles);

                            var charset = Encoding.UTF8;
                            var context = new HtmlPipelineContext(new CssAppliersImpl(new XMLWorkerFontProvider()));
                            context.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(tagProcessors);
                            var htmlPipeline = new HtmlPipeline(context, new PdfWriterPipeline(doc, writer));
                            var cssPipeline = new CssResolverPipeline(cssResolver, htmlPipeline);
                            var worker = new XMLWorker(cssPipeline, true);
                            var xmlParser = new XMLParser(true, worker, charset);

                            using (var sr = new StringReader(DocumentBody))
                            {
                                xmlParser.Parse(sr);
                                doc.Close();
                                ms.Position = 0;
                                ms.CopyTo(stream);
                                stream.Position = 0;
                            }
                        }
                    }
                }
                
                return stream.ToArray();
            }
        }
    }
}
