// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{
    using System.Collections.Generic;

    /// <summary>
    /// Loads the playlist into the current queue.
    /// Optional range (since MPD 0.15) and position (since MPD 0.23.1) limit which songs are loaded and where they go.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#stored-playlists
    /// </summary>
    public class LoadCommand : IMpcCommand<string>
    {
        private readonly string playlistName;
        private readonly int startpos;
        private readonly int endpos;
        private readonly string position;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCommand"/> class.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        public LoadCommand(string playlistName)
        {
            this.playlistName = playlistName;
            this.startpos = -1;
            this.endpos = int.MinValue;
            this.position = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCommand"/> class with a range and queue insert position.
        /// Pass <paramref name="startpos"/>=0 and <paramref name="endpos"/>=<see cref="int.MaxValue"/> to load the whole playlist when also specifying a position.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        /// <param name="startpos">Inclusive start position within the playlist.</param>
        /// <param name="endpos">Exclusive end position within the playlist. Use <see cref="int.MaxValue"/> for "to end".</param>
        /// <param name="position">Optional queue position to insert at. Supports relative offsets like "+0" or "-0".</param>
        public LoadCommand(string playlistName, int startpos, int endpos, string position = "")
        {
            this.playlistName = playlistName;
            this.startpos = startpos;
            this.endpos = endpos;
            this.position = position ?? "";
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize()
        {
            string cmd = $"load \"{playlistName}\"";
            if (startpos >= 0)
            {
                string end = endpos == int.MaxValue ? string.Empty : endpos.ToString();
                cmd += $" {startpos}:{end}";
            }
            if (!string.IsNullOrEmpty(position))
            {
                cmd += $" {position}";
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
        public string Deserialize(SerializedResponse response)
        {
            return string.Join(", ", response.ResponseValues);
        }
    }
}
