// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListPlaylistInfoCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playlist
{
    using System.Collections.Generic;
    using MpcNET.Types;

    /// <summary>
    /// Lists the songs with metadata in the playlist, optionally restricted to a START:END range (range since MPD 0.24).
    /// https://mpd.readthedocs.io/en/stable/protocol.html#stored-playlists
    /// </summary>
    public class ListPlaylistInfoCommand : IMpcCommand<IEnumerable<IMpdFile>>
    {
        private readonly string playlistName;
        private readonly int startpos;
        private readonly int endpos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPlaylistInfoCommand"/> class.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        public ListPlaylistInfoCommand(string playlistName)
        {
            this.playlistName = playlistName;
            this.startpos = -1;
            this.endpos = int.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPlaylistInfoCommand"/> class with a range.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        /// <param name="startpos">Inclusive start position.</param>
        /// <param name="endpos">Exclusive end position.</param>
        public ListPlaylistInfoCommand(string playlistName, int startpos, int endpos)
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
                return $"listplaylistinfo \"{playlistName}\"";
            }
            return $"listplaylistinfo \"{playlistName}\" {startpos}:{endpos}";
        }

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public IEnumerable<IMpdFile> Deserialize(SerializedResponse response)
        {
            return MpdFile.CreateList(response.ResponseValues);
        }
    }
}
