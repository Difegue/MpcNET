// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaylistMoveCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{

    /// <summary>
    /// Moves the song at position FROM in the playlist NAME.m3u to the position TO.
    /// https://www.musicpd.org/doc/html/protocol.html#stored-playlists
    /// </summary>
    public class PlaylistMoveCommand : IMpcCommand<string>
    {
        private readonly string playlist;
        private readonly int from;
        private readonly int endpos;
        private readonly int to;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistMoveCommand"/> class.
        /// </summary>
        /// <param name="playlistName">The playlist name.</param>
        /// <param name="from">Position of the song to move</param>
        /// <param name="to">New position of the song</param>
        public PlaylistMoveCommand(string playlistName, int from, int to)
        {
            this.playlist = playlistName;
            this.from = from;
            this.endpos = int.MinValue;
            this.to = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistMoveCommand"/> class with a range.
        /// Moves the songs at positions [start..end) in the playlist to position TO.
        /// </summary>
        /// <param name="playlistName">The playlist name.</param>
        /// <param name="start">Start position of the range to move</param>
        /// <param name="end">End position (exclusive) of the range to move</param>
        /// <param name="to">New position for the moved songs</param>
        public PlaylistMoveCommand(string playlistName, int start, int end, int to)
        {
            this.playlist = playlistName;
            this.from = start;
            this.endpos = end;
            this.to = to;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize() => string.Join(" ", "playlistmove", $"\"{playlist}\"",
            endpos != int.MinValue ? $"{from}:{endpos}" : $"{from}", to);

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public string Deserialize(SerializedResponse response)
        {
            return string.Join(", ", response.ResponseValues);
        }
    }
}