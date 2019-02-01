using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Specter.Api.Services.Filtering
{

    public interface IFilterParser
    {
        event FilterDictionaryItemNotFoundHandler FilterDictionaryNotFound;
        IEnumerable<IFilterItem> Parse();
    }
    public class FilterParser : IFilterParser
    {
        private readonly IFilterKeywordDictionary _dictionary;
        private readonly string _filter;
        
        private int _position;
        
        public int Position => _position;
        
        public char Char => Peek();
        
        public const char EndOfStatement = (char)0x2403;

        public event FilterDictionaryItemNotFoundHandler FilterDictionaryNotFound;

        public FilterParser(string filter)
        {
            if(string.IsNullOrWhiteSpace(filter))
                throw new ArgumentNullException(nameof(filter));
                
            filter = ReplaceAndOr(filter.Trim());
            
            _filter = filter;
                
            _dictionary = new FilterKeywordDictionary();
        }
        
        public FilterParser(string filter, IFilterKeywordDictionary dictionary)
            : this(filter)
        {
            _dictionary = dictionary;
        }

        public IEnumerable<IFilterItem> Parse()
        {	
            Step('=');
            
            SkipAll(' ');
            
            var filters = new List<IFilterItem>();
            bool finished;
            
            do
            {			
                Step('[');
            
                SkipAll(' ');
                
                var filter = new FilterItem();
                
                filter.Name = GetSteps(IsUpperAlpha, ':');
                
                Step(':');
                
                SkipAll(' ');
                
                filter.Values = GetFilterValuesStep(filter.Name, ']');
                
                Step(']');
                
                SkipAll(' ');
                
                filters.Add(filter);
                
                finished = !Peek(';') || Peek(EndOfStatement);
                
                if(!finished)
                    Step(';');
                    
                SkipAll(' ');
                
            } while(!finished);
            
            return filters;	
        }
        
        private void Step(char expected)
        {
            var peek = Peek();
            if(peek != expected)
                throw new ParseFilterException(_position, $"Expected '{expected}', instead got '{peek}'")
                {
                    Length = 1,
                    Text = peek.ToString()
                };
                
            _position++;
        }
        
        private IEnumerable<FilterValue> GetFilterValuesStep(string filter, char until)
        {
            var values = new List<FilterValue>();
            int order = 0;
            
            while(!Peek(until))
            {
                var value = new FilterValue
                {
                    Order = order++
                };
                
                if(Peek('('))
                {
                    Step('(');
                    value.Complex = GetFilterValuesStep(filter, ')');
                    Step(')');
                    
                    SkipAll(' ');
                }
                else
                {
                    string val;
                    if(Peek('"'))
                    {
                        Step('"');
                        
                        val = GetSteps(Any, '"').Trim();
                        
                        Step('"');
                    }
                    else
                    {
                        val = GetSteps(Any, '|', '&', '-', until).Trim();
                    }
                    
                    value.Value = EnsureValue(filter, val);
                    
                    SkipAll(' ');
                }
                    
                if(Peek('|')) 
                {
                    value.Operation = Operation.Or;
                    Step('|');
                }
                else if(Peek('&'))
                {
                    value.Operation = Operation.And;
                    Step('&');
                }
                else if(Peek('-'))
                {
                    value.Operation = Operation.Until;
                    Step('-');
                }
                
                values.Add(value);
            }
            
            return values;
        }
        
        private string GetSteps(Action<char> ensure, params char[] until)
        {
            var sb = new StringBuilder();
            do
            {
                char chr = _filter[_position];
                
                if(ensure != null)
                    ensure(chr);
                
                sb.Append(chr);
                
                _position++;
            } while(!Peek(until));
            
            return sb.ToString().Trim();
        }
        
        private void IsUpperAlpha(char chr)
        {
            if(!(char.IsLetter(chr) && char.IsUpper(chr)))
                throw new ParseFilterException(_position, $"Invalid identifier, expected upper letter but got {chr}")
                {
                    Length = 1,
                    Text = chr.ToString()
                }; 
        }
        
        private void Any(char chr) {}
        
        private void SkipAll(params char[] chars)
        {
            while(Peek(chars))
                _position++;
        }
        
        private void SkipUntil(params char[] until)
        {
            while(!Peek(until))
                _position++;
        }
        
        private bool Peek(params char[] chars)
        {
            var peek = Peek();
            return chars.Any(c => c == peek);
        }
        
        private char Peek()
        {
            if(_filter.Length <= _position)
                return EndOfStatement;
                
            return _filter[_position];
        }
        
        private string ReplaceAndOr(string filter)
        {
            string pattern = @"\bOR\b";
            string replace = "|";
            
            filter = Regex.Replace(filter, pattern, replace);
            
            pattern = @"\bAND\b";
            replace = "&";
            
            return Regex.Replace(filter, pattern, replace);
        }
        
        private string EnsureValue(string filterName, string value)
        {
            if(value.StartsWith("#"))
            {
                var keyword = value.TrimStart('#');
            
                if(!_dictionary.ContainsKey(filterName))
                {
                    var args = new FilterDictionaryItemNotFoundArgs(filterName);

                    if(FilterDictionaryNotFound != null)
                        FilterDictionaryNotFound(this, _dictionary, args);

                    if(args.Value == null)
                        throw new ParseFilterException(_position, $"Invalid keyword '{keyword}'")
                        {
                            Length = keyword.Length + 1,
                            Text = '#' + keyword
                        }; 
                }
                    
                var dictionary = _dictionary[filterName];
                
                if(!dictionary.ContainsKey(keyword))
                {
                    var args = new FilterDictionaryItemNotFoundArgs(filterName, keyword);

                    if(FilterDictionaryNotFound != null)
                        FilterDictionaryNotFound(this, _dictionary, args);
                        
                    if(args.Value == null)
                        throw new ParseFilterException(_position, $"Invalid keyword '{keyword}' for filter '{filterName}'")
                        {
                            Length = keyword.Length + 1,
                            Text = '#' + keyword
                        }; 
                    
                    return args.Value;
                }
                    
                return dictionary[keyword]();
            }
            
            return value.Trim('"');
        }
    }
}