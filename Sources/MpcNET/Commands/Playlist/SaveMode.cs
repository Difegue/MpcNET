// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveMode.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{
    /// <summary>
    /// Mode for the <see cref="SaveCommand"/>. Added in MPD 0.24.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#stored-playlists
    /// </summary>
    public enum SaveMode
    {
        /// <summary>
        /// Creates a new playlist; fails if a playlist of the same name already exists.
        /// </summary>
        Create,

        /// <summary>
        /// Appends the queue to an existing playlist.
        /// </summary>
        Append,

        /// <summary>
        /// Replaces the contents of an existing playlist with the queue.
        /// </summary>
        Replace,
    }
}
