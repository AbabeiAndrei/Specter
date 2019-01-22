using Specter.Api.Extensions;

namespace Specter.Api.Services.Filtering
{
    public enum Comparation : short
    {
        None = 0,
        And = 1,
        Or = 2
    }

    public interface IFilterCollection
    {
        IFilterItem Item { get; }

        Comparation Next { get; }

        bool MoveNext();
    }

    public class FilterCollection : IFilterCollection
    {
        private const string OR = "OR";
        private const string AND = "AND";

        private IFilterItem _item;
        private readonly string _filter;
        private int _index;

        public virtual IFilterItem Item 
        { 
            get => _item; 
            protected set => _item = value; 
        }

        public virtual Comparation Next { get; protected set; }

        protected FilterCollection()
        {
            _index = 0;
        }

        public FilterCollection(string filter)
            : this()
        {
            _filter = filter;
        }

        public virtual bool MoveNext()
        {
            if(_index >= _filter.Length)
                return false;

            var filter = _filter.Substring(_index).GetFirstUntil(true, "\"" + OR, "\"" + AND);

            if(filter == null)
            {
                var value = _filter.Substring(_index);

                Item = new FilterItem(value);
                Next = Comparation.None;

                return true;
            }

            return false;
        }
    }
}