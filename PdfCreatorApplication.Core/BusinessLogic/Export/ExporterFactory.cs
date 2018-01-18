using PdfCreatorApplication.Core.BusinessLogic.Export.Exporters;

namespace PdfCreatorApplication.Core.BusinessLogic.Export
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExporterFactory
    {
        /// <summary>
        /// Gets the exporter.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IExporter GetExporter(FileType type)
        {
            switch (type)
            {
                case FileType.Pdf:
                    return new PdfExporter();

                default:
                    return new PdfExporter();
            }
        }
    }
}
