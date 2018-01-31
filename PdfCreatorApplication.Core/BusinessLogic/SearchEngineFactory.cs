namespace PdfCreatorApplication.Core.BusinessLogic
{
    public static class SearchEngineFactory
    {
        /// <summary>
        /// Gets the text search engine.
        /// </summary>
        /// <returns></returns>
        public static ISearchable GetTextSearchEngine()
        {
            return TextSearchEngine.Instance;
        }
    }
}
