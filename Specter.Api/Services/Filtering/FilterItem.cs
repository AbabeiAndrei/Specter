namespace Specter.Api.Services.Filtering
{
    public interface IFilterItem
    {
        string Value { get; }
        IFilterCollection Item { get; }
    }

    internal class FilterItem : IFilterItem
    {
        private readonly string _value;
        private readonly IFilterCollection _item;

        public string Value => _value;

        public IFilterCollection Item => _item;

        public FilterItem(string value)
        {
            _value = value;
        }

        public FilterItem(IFilterCollection item)
        {
            _item = item;
        }
    }
}