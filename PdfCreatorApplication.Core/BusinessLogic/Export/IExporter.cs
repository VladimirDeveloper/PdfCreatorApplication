using System;

namespace PdfCreatorApplication.Core.BusinessLogic.Export
{
    public interface IExporter
    {
        byte[] Export();
    }
}
