
[assembly: WebActivator.PostApplicationStartMethod(typeof(PdfCreatorApplication.WebSite.SearchEngine), "OnApplicationStart")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(PdfCreatorApplication.WebSite.SearchEngine), "OnApplicationShutdown")]

namespace PdfCreatorApplication.WebSite
{
    using System;
    using Core.BusinessLogic;
    using System.Diagnostics;
    using System.Threading;
    using Timer = System.Timers.Timer;

    public class SearchEngine
    {
        private static ISearchable _searcher;
        private static Timer _indexerTimer;

        /// <summary>
        /// Prevents a default instance of the <see cref="SearchEngine"/> class from being created.
        /// </summary>
        private SearchEngine()
        {
        }

        /// <summary>
        /// Called when application starts.
        /// </summary>
        public static void OnApplicationStart()
        {
            // Initializes indexer timer
            _indexerTimer = new Timer { Interval = 45000 };
            _indexerTimer.Elapsed += IndexerBuildIndex;
            _indexerTimer.Enabled = true;

            Debug.WriteLine("SearchEngine: init()");
        }

        private static void IndexerBuildIndex(object sender, System.Timers.ElapsedEventArgs e)
        {
            Debug.WriteLine("Time to create index!");
            var threadIndexer = new Thread(BuildIndex);
            threadIndexer.Start();
        }

        private static void BuildIndex(object obj)
        {
            try
            {
                var baseIndexer = new BaseIndexer();
                baseIndexer.BuildIndex();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Call BuildIndex() error - ", ex);
            }
        }

        /// <summary>
        /// Called when application shutdown.
        /// </summary>
        public static void OnApplicationShutdown()
        {
            try
            {
                Debug.WriteLine("Stop indexer timer!");

                _indexerTimer.Enabled = false;
                _indexerTimer.Stop();
                _indexerTimer.Dispose();
                _indexerTimer = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Stop indexer timer error - ", ex);
            }

            Debug.WriteLine("SearchEngine: bye()");
        }

        /// <summary>
        /// Gets the searcher.
        /// </summary>
        /// <value>
        /// The searcher.
        /// </value>
        private static ISearchable Searcher
        {
            get
            {
                return _searcher ?? (_searcher = SearchEngineFactory.GetTextSearchEngine());
            }
        }

        public static void Search()
        {
            Searcher.Search();
        }

    }
}