// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Queue
{
    using MpcNET;
    using System.Collections.Generic;

    /// <summary>
    /// Deletes a song or a range of songs from the queue.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-queue
    /// </summary>
    public class DeleteCommand : IMpcCommand<string>
    {
        private readonly int startpos;
        private readonly int endpos;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public DeleteCommand(int position)
        {
            this.startpos = position;
            this.endpos = int.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand"/> class for a range.
        /// </summary>
        /// <param name="startpos">Inclusive start position.</param>
        /// <param name="endpos">Exclusive end position.</param>
        public DeleteCommand(int startpos, int endpos)
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
        public string Serialize() => string.Join(" ", "delete", endpos != int.MinValue ? $"{startpos}:{endpos}" : $"{startpos}");

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
