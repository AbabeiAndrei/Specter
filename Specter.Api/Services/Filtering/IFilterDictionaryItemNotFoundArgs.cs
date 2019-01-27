namespace Specter.Api.Services.Filtering
{
    public delegate void FilterDictionaryItemNotFoundHandler(IFilterParser parser, IFilterKeywordDictionary dictionary, IFilterDictionaryItemNotFoundArgs args);

    public interface IFilterDictionaryItemNotFoundArgs
    {
        string Value { get; set; }
        string Dictionary { get; }
        string Keyword { get; }
    }

    public class FilterDictionaryItemNotFoundArgs : IFilterDictionaryItemNotFoundArgs
    {
        private readonly string _dictionary;
        private readonly string _keyword;

        public virtual string Value { get; set; }

        public virtual string Dictionary => _dictionary;

        public virtual string Keyword => _keyword;

        public FilterDictionaryItemNotFoundArgs(string dictionary)
        {
            _dictionary = dictionary;
        }
        public FilterDictionaryItemNotFoundArgs(string dictionary, string keyword)
        {
            _dictionary = dictionary;
            _keyword = keyword;
        }
    }
}