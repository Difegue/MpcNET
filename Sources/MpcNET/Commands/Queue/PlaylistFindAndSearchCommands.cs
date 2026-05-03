// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaylistFindAndSearchCommands.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Queue
{
    using System.Collections.Generic;
    using MpcNET.Commands.Database;
    using MpcNET.Tags;
    using MpcNET.Types;

    /// <summary>
    /// Searches the queue for songs matching FILTER (case sensitive).
    /// Supports optional sort and window parameters.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-queue
    /// </summary>
    public class PlaylistFindCommand : BaseFilterCommand
    {
        /// <summary>
        ///
        /// </summary>
        public override string CommandName => "playlistfind";

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistFindCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistFindCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(tag, searchText, windowStart, windowEnd, FilterOperator.Equal, sort) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistFindCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistFindCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(filters, windowStart, windowEnd, FilterOperator.Equal, sort) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistFindCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistFindCommand(IFilter filter, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(filter, windowStart, windowEnd, sort) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistFindCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistFindCommand(List<IFilter> filters, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(filters, windowStart, windowEnd, sort) { }
    }

    /// <summary>
    /// Searches the queue for songs matching FILTER (case-insensitive).
    /// Supports optional sort and window parameters.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-queue
    /// </summary>
    public class PlaylistSearchCommand : BaseFilterCommand
    {
        /// <summary>
        ///
        /// </summary>
        public override string CommandName => "playlistsearch";

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistSearchCommand"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistSearchCommand(ITag tag, string searchText, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(tag, searchText, windowStart, windowEnd, FilterOperator.Equal, sort) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistSearchCommand"/> class.
        /// </summary>
        /// <param name="filters">List of key/value filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistSearchCommand(List<KeyValuePair<ITag, string>> filters, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(filters, windowStart, windowEnd, FilterOperator.Equal, sort) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistSearchCommand"/> class.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistSearchCommand(IFilter filter, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(filter, windowStart, windowEnd, sort) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistSearchCommand"/> class.
        /// </summary>
        /// <param name="filters">List of filters</param>
        /// <param name="windowStart">Start of the portion of the results desired</param>
        /// <param name="windowEnd">End of the portion of the results desired</param>
        /// <param name="sort">Optional sort parameter (tag name; prefix with "-" for descending).</param>
        public PlaylistSearchCommand(List<IFilter> filters, int windowStart = -1, int windowEnd = -1, string sort = "")
            : base(filters, windowStart, windowEnd, sort) { }
    }
}
