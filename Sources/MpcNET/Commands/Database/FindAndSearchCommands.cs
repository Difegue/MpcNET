// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FindCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using MpcNET.Tags;
    using MpcNET.Types;
    using MpcNET.Types.Filters;

    /// <summary>
    /// Finds songs in the database that contain "searchText".
    /// Since MPD 0.21, search syntax is now (TAG == 'VALUE').
    /// https://mpd.readthedocs.io/en/stable/protocol.html#filters
    /// </summary>
    public class SearchCommand : BaseFilterCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public override string CommandName => "search";

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
    /// https://mpd.readthedocs.io/en/stable/protocol.html#filters
    /// </summary>
    public class SearchAddCommand : BaseFilterCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public override string CommandName => "searchadd";

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAddCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>        
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1) : base(tag, searchText, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAddCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAddCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(IFilter filter, int windowStart = -1, int windowEnd = -1) : base(filter, windowStart, windowEnd) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAddCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        public SearchAddCommand(List<IFilter> filters, int windowStart = -1, int windowEnd = -1) : base(filters, windowStart, windowEnd) { }
    }

    /// <summary>
    /// Finds songs in the database that is exactly "searchText".
    /// Since MPD 0.21, search syntax is now (TAG == 'VALUE').
    /// https://mpd.readthedocs.io/en/stable/protocol.html#filters
    /// </summary>
    public class FindCommand : BaseFilterCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public override string CommandName => "find";

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
        private readonly List<IFilter> _filters;
        private readonly int _start;
        private readonly int _end;

        /// <summary>
        /// Name of the command to use when deserializing
        /// </summary>
        public abstract string CommandName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="operand">Operator of the filter</param>
        public BaseFilterCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1, FilterOperator operand = FilterOperator.Equal)
        {
            _filters = new List<IFilter>();
            FilterTag Tag = new FilterTag(tag, searchText, operand);
            _filters.Add(Tag);

            _start = windowStart;
            _end = windowEnd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="operand">Operator of the filter</param>
        public BaseFilterCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1, FilterOperator operand = FilterOperator.Equal)
        {
            _filters = new List<IFilter>();
            _filters.AddRange(filters.Select(filter => new FilterTag(filter.Key, filter.Value, operand)).ToList());

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
            _filters = new List<IFilter>();
            _filters.Add(filters);

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
            _filters = filters;

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

            if (_filters != null)
            {
                var serializedFilters = string.Join(" AND ",
                    _filters.Select(x => $"{x.GetFormattedCommand()}")
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
    }
    // TODO: rescan
}
