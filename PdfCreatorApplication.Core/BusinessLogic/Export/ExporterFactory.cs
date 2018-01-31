using PdfCreatorApplication.Core.BusinessLogic.Export.Exporters;

namespace PdfCreatorApplication.Core.BusinessLogic.Export
{
    public static class ExporterFactory
    {
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
