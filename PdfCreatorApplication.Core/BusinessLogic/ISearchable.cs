using System;

namespace PdfCreatorApplication.Core.BusinessLogic
{
    public interface ISearchable : IDisposable
    {
        void Search();
        void Refresh();
    }
}
