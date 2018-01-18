using System;

namespace PdfCreatorApplication.Core.BusinessLogic.Export
{
    public interface IExporter
    {
        /// <summary>
        /// Exports.
        /// </summary>
        /// <returns></returns>
        byte[] Export();
    }
}
