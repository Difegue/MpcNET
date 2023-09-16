// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaylistDeleteCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{

    /// <summary>
    /// Deletes SONGPOS from the playlist NAME.m3u.
    /// https://www.musicpd.org/doc/html/protocol.html#stored-playlists
    /// </summary>
    public class PlaylistDeleteCommand : IMpcCommand<string>
    {
        private readonly string playlist;
        private readonly int startpos;
        private readonly int endpos;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistDeleteCommand"/> class.
        /// </summary>
        /// <param name="playlistName">The playlist name.</param>
        /// <param name="startpos">Position of the song to remove</param>
        /// <param name="endpos">Position of the song to remove</param>
        public PlaylistDeleteCommand(string playlistName, int startpos, int endpos = int.MinValue)
        {
            this.playlist = playlistName;
            this.startpos = startpos;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize() => string.Join(" ", "playlistdelete", $"\"{playlist}\"", endpos != int.MinValue ? $"{startpos}:{endpos}" : $"{startpos}");

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