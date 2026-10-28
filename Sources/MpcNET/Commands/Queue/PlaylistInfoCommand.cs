// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaylistInfoCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MpcNET.Commands.Queue
{
    using System.Collections.Generic;
    using MpcNET.Types;

    /// <summary>
    /// Displays a list of all songs in the queue, or only the song at SONGPOS, or songs in the START:END range.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-queue
    /// </summary>
    public class PlaylistInfoCommand : IMpcCommand<IEnumerable<IMpdFile>>
    {
        private readonly int startpos;
        private readonly int endpos;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistInfoCommand"/> class for the full queue.
        /// </summary>
        public PlaylistInfoCommand()
        {
            this.startpos = -1;
            this.endpos = int.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistInfoCommand"/> class for a single song.
        /// </summary>
        /// <param name="songpos">The position of the song to fetch.</param>
        public PlaylistInfoCommand(int songpos)
        {
            this.startpos = songpos;
            this.endpos = int.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistInfoCommand"/> class for a range of songs.
        /// </summary>
        /// <param name="startpos">Inclusive start position.</param>
        /// <param name="endpos">Exclusive end position.</param>
        public PlaylistInfoCommand(int startpos, int endpos)
        {
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
                return "playlistinfo";
            }
            if (endpos != int.MinValue)
            {
                return $"playlistinfo {startpos}:{endpos}";
            }
            return $"playlistinfo {startpos}";
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
