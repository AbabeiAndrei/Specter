using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

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
                ["USER"] = new FilterItemKeywordDictionary
                {
                    ["Me"] = () => "me"
                },
                ["DATE"] = new FilterItemKeywordDictionary
                {
                    ["Today"] = () => DateTime.Now.ToString("dd.MM.yyyy")
                }
            };
        }
    }

    public interface IFilterItemKeywordDictionary : IDictionary<string, Func<string>>
    {
        
    }
    
    public class FilterItemKeywordDictionary : ConcurrentDictionary<string, Func<string>>, IFilterItemKeywordDictionary
    {

    }
}