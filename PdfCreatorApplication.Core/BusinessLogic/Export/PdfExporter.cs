using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfCreatorApplication.Core.BusinessLogic.Export
{
    public class PdfExporter : IExporter
    {
        public PdfExporter()
        {

        }

        public byte[] Export()
        {
            return File.ReadAllBytes("C:\\temp\\example.pdf");
        }
    }
}
