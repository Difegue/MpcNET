// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MpdSongCount.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Types
{
    /// <summary>
    /// Result of an MPD "count" or "searchcount" command.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-music-database
    /// </summary>
    public class MpdSongCount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MpdSongCount"/> class.
        /// </summary>
        /// <param name="songs">Number of matching songs.</param>
        /// <param name="playtime">Total playtime of matching songs, in seconds.</param>
        public MpdSongCount(int songs, int playtime)
            : this(songs, playtime, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MpdSongCount"/> class for a grouped result row.
        /// </summary>
        /// <param name="songs">Number of matching songs in this group.</param>
        /// <param name="playtime">Total playtime in this group, in seconds.</param>
        /// <param name="groupTag">Tag name used for grouping (e.g. "Artist"); null when not grouped.</param>
        /// <param name="groupValue">Value of the group tag for this row; null when not grouped, empty when songs lack the tag.</param>
        public MpdSongCount(int songs, int playtime, string groupTag, string groupValue)
        {
            this.Songs = songs;
            this.Playtime = playtime;
            this.GroupTag = groupTag;
            this.GroupValue = groupValue;
        }

        /// <summary>
        /// Gets the number of matching songs.
        /// </summary>
        public int Songs { get; }

        /// <summary>
        /// Gets the total playtime of matching songs, in seconds.
        /// </summary>
        public int Playtime { get; }

        /// <summary>
        /// Gets the tag name used for grouping, or null when not grouped.
        /// </summary>
        public string GroupTag { get; }

        /// <summary>
        /// Gets the group tag value for this row, or null when not grouped.
        /// </summary>
        public string GroupValue { get; }
    }
}
