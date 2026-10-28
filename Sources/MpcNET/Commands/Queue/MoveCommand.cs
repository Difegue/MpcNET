// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MoveCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Queue
{
    using MpcNET;
    using System.Collections.Generic;

    /// <summary>
    /// Moves a song or a range of songs in the queue to a new position.
    /// TO supports relative offsets ("+N" / "-N") relative to the current song.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-queue
    /// </summary>
    public class MoveCommand : IMpcCommand<string>
    {
        private readonly int from;
        private readonly int end;
        private readonly string to;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class for a single song.
        /// </summary>
        /// <param name="from">Source position in the queue.</param>
        /// <param name="to">Target position. Supports relative offsets like "+0" or "-0".</param>
        public MoveCommand(int from, string to)
        {
            this.from = from;
            this.end = int.MinValue;
            this.to = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class for a single song.
        /// </summary>
        /// <param name="from">Source position in the queue.</param>
        /// <param name="to">Target position. Negative values are relative to the current song.</param>
        public MoveCommand(int from, int to)
        {
            this.from = from;
            this.end = int.MinValue;
            this.to = to.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class for a range of songs.
        /// </summary>
        /// <param name="start">Inclusive start of the source range.</param>
        /// <param name="end">Exclusive end of the source range.</param>
        /// <param name="to">Target position. Supports relative offsets like "+0" or "-0".</param>
        public MoveCommand(int start, int end, string to)
        {
            this.from = start;
            this.end = end;
            this.to = to;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize()
        {
            string source = end != int.MinValue ? $"{from}:{end}" : $"{from}";
            return $"move {source} {to}";
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
