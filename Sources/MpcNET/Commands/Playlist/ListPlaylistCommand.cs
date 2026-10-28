// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListPlaylistCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{
    using System.Collections.Generic;
    using System.Linq;
    using MpcNET.Types;

    /// <summary>
    /// Lists the songs in the playlist, optionally restricted to a START:END range (range since MPD 0.24).
    /// https://mpd.readthedocs.io/en/stable/protocol.html#stored-playlists
    /// </summary>
    public class ListPlaylistCommand : IMpcCommand<IEnumerable<IMpdFilePath>>
    {
        private readonly string playlistName;
        private readonly int startpos;
        private readonly int endpos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPlaylistCommand"/> class.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        public ListPlaylistCommand(string playlistName)
        {
            this.playlistName = playlistName;
            this.startpos = -1;
            this.endpos = int.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPlaylistCommand"/> class with a range.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        /// <param name="startpos">Inclusive start position.</param>
        /// <param name="endpos">Exclusive end position.</param>
        public ListPlaylistCommand(string playlistName, int startpos, int endpos)
        {
            this.playlistName = playlistName;
            this.startpos = startpos;
            this.endpos = endpos;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize()
        {
            if (startpos < 0)
            {
                return $"listplaylist \"{playlistName}\"";
            }
            return $"listplaylist \"{playlistName}\" {startpos}:{endpos}";
        }

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public IEnumerable<IMpdFilePath> Deserialize(SerializedResponse response)
        {
            var results = response.ResponseValues.Where(line => line.Key.Equals("file")).Select(line => new MpdFile(line.Value));

            return results;
        }
    }
}
