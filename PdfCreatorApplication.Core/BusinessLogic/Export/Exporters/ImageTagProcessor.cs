using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharpImage = iTextSharp.tool.xml.html.Image;
using Image = iTextSharp.text.Image;

namespace PdfCreatorApplication.Core.BusinessLogic.Export.Exporters
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageTagProcessor : iTextSharpImage
    {
        /// <summary>
        /// The image stream tag pattern
        /// </summary>
        private const string ImageStreamTagPattern = "data:imagestream/";

        /// <summary>
        /// The images filename pattern
        /// </summary>
        private const string ImagesFilenamePattern = "{0}Images\\PhotoBank\\{1}.png";

        /// <summary>
        /// Ends the specified context.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="currentContent">Content of the current.</param>
        /// <returns></returns>
        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
        {
            string src;

            if (!tag.Attributes.TryGetValue(HTML.Attribute.SRC, out src))
            {
                return new List<IElement>(1);
            }

            if (String.IsNullOrWhiteSpace(src))
            {
                return new List<IElement>(1);
            }

            if (!string.IsNullOrEmpty(src) && src.StartsWith(ImageStreamTagPattern, StringComparison.InvariantCultureIgnoreCase))
            {
                var imageName = string.Format(ImagesFilenamePattern, 
                                                AppDomain.CurrentDomain.BaseDirectory,
                                                src.Substring(src.IndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1));

                using (var stream = new FileStream(imageName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var image = Image.GetInstance(stream);
                    image.ScalePercent(80f);

                    return CreateElements(ctx, tag, image);
                }
            }

            return base.End(ctx, tag, currentContent);
        }

        /// <summary>
        /// Creates the elements.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        private IList<IElement> CreateElements(IWorkerContext ctx, Tag tag, Image image)
        {
            var htmlPipelineContext = GetHtmlPipelineContext(ctx);
            var result = new List<IElement>();
            var element = GetCssAppliers().Apply(new Chunk((Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext);
            result.Add(element);

            return result;
        }
    }
}
