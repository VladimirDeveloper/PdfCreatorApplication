using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PdfCreatorApplication.Core.BusinessLogic
{
    public class BaseIndexer
    {
        private ISearchable _searcher;

        public BaseIndexer()
        {

        }

        private ISearchable Searcher
        {
            get
            {
                return _searcher ?? (_searcher = SearchEngineFactory.GetTextSearchEngine());
            }
        }

        public void BuildIndex()
        {
            Thread.Sleep(3000);
            Debug.WriteLine("BaseIndexer: Build index, ask TextEngine to refresh...");
            Searcher.Refresh();
        }
    }
}
