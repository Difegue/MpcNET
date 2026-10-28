// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountAndSearchCountCommands.cs" company="MpcNET">
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
    /// Counts the songs in the database that match the given filter (case-sensitive).
    /// Returns the number of matching songs and their total playtime.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-music-database
    /// </summary>
    public class CountCommand : BaseCountCommand
    {
        /// <summary>
        ///
        /// </summary>
        public override string CommandName => "count";

        /// <summary>
        /// Initializes a new instance of the <see cref="CountCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        public CountCommand(ITag tag, string searchText) : base(tag, searchText) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        public CountCommand(List<KeyValuePair<ITag, string>> filters) : base(filters) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        public CountCommand(IFilter filter) : base(filter) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        public CountCommand(List<IFilter> filters) : base(filters) { }
    }

    /// <summary>
    /// Counts the songs in the database that match the given filter (case-insensitive).
    /// Returns the number of matching songs and their total playtime.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-music-database
    /// </summary>
    public class SearchCountCommand : BaseCountCommand
    {
        /// <summary>
        ///
        /// </summary>
        public override string CommandName => "searchcount";

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCountCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        public SearchCountCommand(ITag tag, string searchText) : base(tag, searchText) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCountCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        public SearchCountCommand(List<KeyValuePair<ITag, string>> filters) : base(filters) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCountCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        public SearchCountCommand(IFilter filter) : base(filter) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCountCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        public SearchCountCommand(List<IFilter> filters) : base(filters) { }
    }

    /// <summary>
    /// Base class for count-style commands. Returns an <see cref="MpdSongCount"/>.
    /// </summary>
    public abstract class BaseCountCommand : IMpcCommand<MpdSongCount>
    {
        /// <summary>The list of filters for the command.</summary>
        protected readonly List<IFilter> _filters;

        /// <summary>
        /// Name of the MPD command (e.g. "count" or "searchcount").
        /// </summary>
        public abstract string CommandName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCountCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="operand">Operator of the filter</param>
        public BaseCountCommand(ITag tag, string searchText, FilterOperator operand = FilterOperator.Equal)
        {
            _filters = new List<IFilter> { new FilterTag(tag, searchText, operand) };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCountCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="operand">Operator of the filter</param>
        public BaseCountCommand(List<KeyValuePair<ITag, string>> filters, FilterOperator operand = FilterOperator.Equal)
        {
            _filters = filters.Select(filter => (IFilter)new FilterTag(filter.Key, filter.Value, operand)).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCountCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        public BaseCountCommand(IFilter filter)
        {
            _filters = new List<IFilter> { filter };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCountCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        public BaseCountCommand(List<IFilter> filters)
        {
            _filters = filters;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>The serialized command.</returns>
        public string Serialize()
        {
            if (_filters == null)
            {
                return CommandName;
            }

            var serializedFilters = string.Join(" AND ",
                _filters.Select(x => $"{x.GetFormattedCommand()}")
            );
            return $@"{CommandName} ""({serializedFilters})""";
        }

        /// <summary>
        /// Deserializes the response into an <see cref="MpdSongCount"/>.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>An <see cref="MpdSongCount"/> with the matching song count and total playtime.</returns>
        public MpdSongCount Deserialize(SerializedResponse response)
        {
            int songs = 0;
            int playtime = 0;
            foreach (var line in response.ResponseValues)
            {
                if (line.Key == "songs")
                {
                    int.TryParse(line.Value, out songs);
                }
                else if (line.Key == "playtime")
                {
                    int.TryParse(line.Value, out playtime);
                }
            }
            return new MpdSongCount(songs, playtime);
        }
    }
}
