// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{
    using System.Collections.Generic;

    /// <summary>
    /// Saves the queue to NAME.m3u in the playlist directory.
    /// https://www.musicpd.org/doc/html/protocol.html#stored-playlists
    /// </summary>
    public class SaveCommand : IMpcCommand<string>
    {
        private readonly string playlistName;
        private readonly SaveMode? mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCommand"/> class.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        public SaveCommand(string playlistName)
        {
            this.playlistName = playlistName;
            this.mode = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCommand"/> class with an explicit mode.
        /// Mode parameter requires MPD 0.24 or later.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        /// <param name="mode">Mode controlling whether to create, append to, or replace the playlist.</param>
        public SaveCommand(string playlistName, SaveMode mode)
        {
            this.playlistName = playlistName;
            this.mode = mode;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize() => mode.HasValue
            ? string.Join(" ", "save", $"\"{this.playlistName}\"", mode.Value.ToString().ToLowerInvariant())
            : string.Join(" ", "save", $"\"{this.playlistName}\"");

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