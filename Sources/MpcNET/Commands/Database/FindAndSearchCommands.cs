// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FindCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Database
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using MpcNET.Tags;
    using MpcNET.Types;

    /// <summary>
    /// Operators available for filters in the protocol
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Equal (==)
        /// </summary>
        [Description("==")]
        Equal,
        /// <summary>
        /// Different (!=)
        /// </summary>
        [Description("!=")]
        Different,
        /// <summary>
        /// Contains (contains)
        /// </summary>
        [Description("contains")]
        Contains,
        /// <summary>
        /// Mask (=~)
        /// </summary>
        [Description("=~")]
        Mask,
        /// <summary>
        /// None ("")
        /// </summary>
        [Description("")]
        None,
    }

    /// <summary>
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// <see cref="GetDescription"/> of the enum
        /// </summary>
        /// <param name="enumValue"></param>
        public static string GetDescription<T>(this T enumValue)
            where T : struct, System.IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            string description = enumValue.ToString();
            System.Reflection.FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes != null && attributes.Length > 0)
                    description = ((DescriptionAttribute)attributes[0]).Description;
            }

            return description;
        }
    }

    /// <summary>
    /// Filter interface specialized by filters in the protocol
    /// </summary>
    public abstract class IFilter
    {
        /// <summary>
        /// Name of the filter
        /// </summary>
        public string Name;
        /// <summary>
        /// Operator of the filter
        /// </summary>
        public FilterOperator Operator;
        /// <summary>
        /// Value to test
        /// </summary>
        public string Value;
        /// <summary>
        /// If the expression is negated
        /// </summary>
        public bool Negation;

        /// <summary>
        /// Initializes a new instance of the <see cref="IFilter"/> class.
        /// </summary>
        /// <param name="Name">Name of the filter</param>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public IFilter(string Name, FilterOperator Operator, string Value, bool Negation = false)
        {
            this.Name = Name;
            this.Operator = Operator;
            this.Value = Value;
            this.Negation = Negation;
        }

        /// <summary>
        /// Gets the formatted command
        /// </summary>
        public string GetFormattedCommand()
        {
            string make = "";

            if (Negation)
                make += "(!";

            make += "(" + Name + " ";
            make += Operator.GetDescription();
            make += " " + "\\\"" + Value + "\\\"" + ")";

            if (Negation)
                make += ")";

            return make;
        }
    }

    /// <summary>
    /// Filter "file"
    /// </summary>
    public class FilterFile : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterFile"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterFile(string Value, FilterOperator Operator, bool Negation = false) : base("file", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.Equal)
                throw new System.ArgumentException("Operator is not compatible: for \"File\" use FilterOperator.Equal.");

            this.Name = "file";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }

    /// <summary>
    /// Filter "TAG"
    /// </summary>
    public class FilterTag : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterTag"/> class.
        /// </summary>
        /// <param name="Tag">The tag.</param>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterTag(ITag Tag, string Value, FilterOperator Operator, bool Negation = false) : base(Tag.Value, Operator, Value, Negation)
        {
            if (Operator != FilterOperator.Equal && Operator != FilterOperator.Different && Operator != FilterOperator.Contains)
                throw new System.ArgumentException("Operator is not compatible: for \"TAG\" use FilterOperator.Equal, FilterOperator.Different or FilterOperator.Contains.");

            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }

    /// <summary>
    /// Filter "base" (not the interface base class, the base command: restrict search songs to a directory)
    /// </summary>
    public class FilterBase : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterBase"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterBase(string Value, FilterOperator Operator, bool Negation = false) : base("base", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.None)
                throw new System.ArgumentException("Operator is not compatible: for \"base\" use FilterOperator.None.");

            this.Name = "base";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }

    /// <summary>
    /// Filter "modified-since"
    /// </summary>
    public class FilterModifiedSince : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterModifiedSince"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterModifiedSince(string Value, FilterOperator Operator, bool Negation = false) : base("modified-since", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.None)
                throw new System.ArgumentException("Operator is not compatible: for \"ModifiedSince\" use FilterOperator.None.");

            this.Name = "modified-since";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }

    /// <summary>
    /// Filter "AudioFormat"
    /// </summary>
    public class FilterAudioFormat : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterAudioFormat"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterAudioFormat(string Value, FilterOperator Operator, bool Negation = false) : base("AudioFormat", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.Equal && Operator != FilterOperator.Mask)
                throw new System.ArgumentException("Operator is not compatible: for \"AudioFormat\" use FilterOperator.Equal or FilterOperator.Mask.");

            this.Name = "AudioFormat";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }

    /// <summary>
    /// Finds songs in the database that contain "searchText".
    /// Since MPD 0.21, search syntax is now (TAG == 'VALUE').
    /// https://www.musicpd.org/doc/html/protocol.html#filters
    /// </summary>
    public class SearchCommand : BaseFilterCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public override string CommandName => "search";
        /// <summary>
        /// 
        /// </summary>
        public override string Operand => "contains";

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>        
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1) : base(tag, searchText, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchCommand(IFilter filter, int windowStart = -1, int windowEnd = -1) : base(filter, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchCommand(List<IFilter> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }

    }

    /// <summary>
    /// Finds songs in the database that contain "searchText" and adds them to the queue.
    /// Since MPD 0.21, search syntax is now (TAG == 'VALUE').
    /// https://www.musicpd.org/doc/html/protocol.html#filters
    /// </summary>
    public class SearchAddCommand : BaseFilterCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public override string CommandName => "searchadd";
        /// <summary>
        /// 
        /// </summary>
        public override string Operand => "contains";

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>        
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1) : base(tag, searchText, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(IFilter filter, int windowStart = -1, int windowEnd = -1) : base(filter, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(List<IFilter> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }
    }

    /// <summary>
    /// Finds songs in the database that is exactly "searchText".
    /// Since MPD 0.21, search syntax is now (TAG == 'VALUE').
    /// https://www.musicpd.org/doc/html/protocol.html#filters
    /// </summary>
    public class FindCommand : BaseFilterCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public override string CommandName => "find";
        /// <summary>
        /// 
        /// </summary>
        public override string Operand => "==";

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public FindCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1) : base(tag, searchText, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public FindCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public FindCommand(IFilter filter, int windowStart = -1, int windowEnd = -1) : base(filter, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public FindCommand(List<IFilter> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }

    }

    /// <summary>
    /// Base class for find/search commands.
    /// </summary>
    public abstract class BaseFilterCommand : IMpcCommand<IEnumerable<IMpdFile>>
    {
        private readonly List<KeyValuePair<ITag, string>> tagFilters;
        private readonly List<IFilter> completeFilters;
        private readonly int _start;
        private readonly int _end;

        /// <summary>
        /// Name of the command to use when deserializing
        /// </summary>
        public abstract string CommandName { get; }
        /// <summary>
        /// Operand to use between tags and search text. Can be ==, !=, contains...
        /// </summary>
        public abstract string Operand { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public BaseFilterCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1)
        {
            tagFilters = new List<KeyValuePair<ITag, string>>();
            tagFilters.Add(new KeyValuePair<ITag, string>(tag, searchText));

            _start = windowStart;
            _end = windowEnd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public BaseFilterCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1)
        {
            tagFilters = filters;

            _start = windowStart;
            _end = windowEnd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterCommand"/> class.
        /// </summary>
        /// <param name="filters">Filter</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public BaseFilterCommand(IFilter filters, int windowStart = -1, int windowEnd = -1)
        {
            completeFilters = new List<IFilter>();
            completeFilters.Add(filters);

            _start = windowStart;
            _end = windowEnd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public BaseFilterCommand(List<IFilter> filters, int windowStart = -1, int windowEnd = -1)
        {
            completeFilters = filters;

            _start = windowStart;
            _end = windowEnd;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialized command.
        /// </returns>
        public string Serialize()
        {
            string cmd = "";

            if (tagFilters != null)
            {
                var serializedFilters = string.Join(" AND ",
                    tagFilters.Select(x => $"({x.Key.Value} {Operand} {escape(x.Value)})")
                );
                cmd = $@"{CommandName} ""({serializedFilters})""";
            }

            if (completeFilters != null)
            {
                var serializedFilters = string.Join(" AND ",
                    completeFilters.Select(x => $"{x.GetFormattedCommand()}")
                );
                cmd = $@"{CommandName} ""({serializedFilters})""";
            }

            if (_start > -1)
            {
                cmd += $" window {_start}:{_end}";
            }

            return cmd;
        }

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public IEnumerable<IMpdFile> Deserialize(SerializedResponse response)
        {
            return MpdFile.CreateList(response.ResponseValues);
        }

        /// <summary>
        /// String values are quoted with single or double quotes, 
        /// and special characters within those values must be escaped with the backslash (\). 
        /// Keep in mind that the backslash is also the escape character on the protocol level, 
        /// which means you may need to use double backslash. 
        /// 
        /// Example expression which matches an artist named foo'bar":
        /// (Artist == "foo\'bar\"")
        /// 
        /// At the protocol level, the command must look like this:
        /// find "(Artist == \"foo\\'bar\\\"\")"
        /// 
        /// (https://mpd.readthedocs.io/en/stable/protocol.html#filter-syntax)
        /// </summary>
        /// <param name="value">Value to escape</param>
        /// <returns></returns>
        private string escape(string value)
        {
            var escapedValue = value.Replace(@"\", @"\\\\")
                                    .Replace("'", @"\\'")
                                    .Replace(@"""", @"\\\""");

            return $@"\""{escapedValue}\""";
        }
    }
    // TODO: rescan
}
