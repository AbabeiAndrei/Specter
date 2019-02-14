using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

using Specter.Api.Extensions;

namespace Specter.Api.Services.Filtering
{
    public interface IFilterKeywordDictionary : IDictionary<string, IFilterItemKeywordDictionary>
    {
        
    }

    public class FilterKeywordDictionary : ConcurrentDictionary<string, IFilterItemKeywordDictionary>, IFilterKeywordDictionary
    {
        internal static IFilterKeywordDictionary Default()
        {
            return new FilterKeywordDictionary
            {
                ["USER"] = new FilterItemKeywordDictionary(),
                ["DATE"] = new FilterItemKeywordDictionary
                {
                    ["Today"] = FilterItemDictionaryResult.FromResult(() => DateTime.Now.ToString("dd.MM.yyyy")),
                    ["Week"] = FilterItemDictionaryResult.FromResult(() => $"{DateTime.Now.StartOfWeek():dd.MM.yyy}-{DateTime.Now.EndOfWeek():dd.MM.yyy}")
                }
            };
        }
    }

    public interface IFilterItemKeywordDictionary : IDictionary<string, IFilterItemDictionaryResult>
    {
        
    }
    
    public class FilterItemKeywordDictionary : ConcurrentDictionary<string, IFilterItemDictionaryResult>, IFilterItemKeywordDictionary
    {

    }

    public interface IFilterItemDictionaryResult
    {
        string Value { get; }
    }

    public class FilterItemDictionaryResult : IFilterItemDictionaryResult
    {
        private string _value;

        public virtual string Value 
        { 
            get => _value; 
            set => _value = value; 
        }

        public static IFilterItemDictionaryResult FromResult(Func<string> source) => new LazyFilterItemDictionaryResult(source);
    }

    internal class LazyFilterItemDictionaryResult : IFilterItemDictionaryResult
    {
        private readonly Func<string> _retriever;

        public virtual string Value => _retriever?.Invoke() ?? null;

        internal LazyFilterItemDictionaryResult(Func<string> retriever) 
        {
            _retriever = retriever;
        }
    }
}