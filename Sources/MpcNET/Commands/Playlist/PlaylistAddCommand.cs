// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaylistAddCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{

    /// <summary>
    /// Adds URI to the playlist NAME.m3u. NAME.m3u will be created if it does not exist.
    /// https://www.musicpd.org/doc/html/protocol.html#command-load
    /// </summary>
    public class PlaylistAddCommand : IMpcCommand<string>
    {
        private readonly string playlist;
        private readonly string pathUri;
        private readonly string position;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistAddCommand"/> class.
        /// </summary>
        /// <param name="playlistName">The playlistn name.</param>
        /// <param name="uri">The path to add.</param>
        public PlaylistAddCommand(string playlistName, string uri)
        {
            this.playlist = playlistName;
            this.pathUri = uri;
            this.position = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistAddCommand"/> class.
        /// </summary>
        /// <param name="playlistName">The playlistn name.</param>
        /// <param name="uri">The path to add.</param>
        /// <param name="position">Optional position in the playlist to place the path at.</param>
        public PlaylistAddCommand(string playlistName, string uri, int position)
        {
            this.playlist = playlistName;
            this.pathUri = uri;
            this.position = position.ToString();
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize() => string.Join(" ", "playlistadd", $"\"{playlist}\"", $"\"{pathUri}\"", position);

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